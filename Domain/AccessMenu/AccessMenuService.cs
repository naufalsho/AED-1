using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Domain.AccessMenu
{
    public class AccessMenuService : IAccessMenuService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AccessMenuService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        #region Menu Group
        public async Task<Result<IEnumerable<MenuGroupDto>>> GroupGetList()
        {
            try
            {
                var repoResult = await _uow.AccMenuGroup.Set().ToListAsync();

                var result = _mapper.Map<IEnumerable<MenuGroupDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MenuGroupDto>> GroupGetById(int id)
        {
            try
            {
                var repoResult = await _uow.AccMenuGroup.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<MenuGroupDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> GroupCreate(UserClaimModel user, MenuGroupDto data)
        {
            try
            {
                var checkOrder = await _uow.AccMenuGroup.Set().FirstOrDefaultAsync(m => m.Order == data.Order);

                if (checkOrder != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Group order not available. Please change the order data!");

                var dataToAdd = _mapper.Map<TAccMenuGroup>(data);
                dataToAdd.CreatedBy = user.NameIdentifier;
                dataToAdd.CreatedDate = DateTime.Now;

                await _uow.AccMenuGroup.Add(dataToAdd);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> GroupUpdate(UserClaimModel user, MenuGroupDto data)
        {
            try
            {
                var repoResult = await _uow.AccMenuGroup.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var checkOrder = await _uow.AccMenuGroup.Set().FirstOrDefaultAsync(m => m.Id != data.Id && m.Order == data.Order);

                if (checkOrder != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Group order not available. Please change the order data!");

                _mapper.Map(data, repoResult);
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.AccMenuGroup.Update(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> GroupDelete(UserClaimModel user, int data)
        {
            try
            {
                var repoResult = await _uow.AccMenuGroup.Set().Include(m => m.Menus).FirstOrDefaultAsync(m => m.Id == data);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                if (repoResult.Menus.Any())
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Group data used on menu. Please delete the menu first!");

                _uow.AccMenuGroup.Remove(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        #endregion

        #region Menu
        public async Task<Result<IEnumerable<MenuDto>>> MenuGetList()
        {
            try
            {
                var repoResult = await _uow.AccMenu.Set().Include(m => m.MenuGroup).OrderBy(m => m.MenuGroup.Order).ThenBy(m => m.Order).ToListAsync();

                var result = _mapper.Map<IEnumerable<MenuDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<MenuDto>> MenuGetById(int id)
        {
            try
            {
                var repoResult = await _uow.AccMenu.Set().Include(m => m.MenuGroup).FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<MenuDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> MenuCreate(UserClaimModel user, MenuDto data)
        {
            try
            {
                var checkOrder = await _uow.AccMenu.Set().FirstOrDefaultAsync(m => m.MenuGroupId == data.MenuGroupId && m.Order == data.Order);

                if (checkOrder != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Menu order not available. Please change the order data!");

                var dataToAdd = _mapper.Map<TAccMenu>(data);
                dataToAdd.CreatedBy = user.NameIdentifier;
                dataToAdd.CreatedDate = DateTime.Now;

                await _uow.AccMenu.Add(dataToAdd);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> MenuUpdate(UserClaimModel user, MenuUpdateDto data)
        {
            try
            {
                var repoResult = await _uow.AccMenu.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var checkOrder = await _uow.AccMenu.Set().FirstOrDefaultAsync(m => m.Id != data.Id && m.MenuGroupId == data.MenuGroupId && m.Order == data.Order);

                if (checkOrder != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Menu order not available. Please change the order data!");

                _mapper.Map(data, repoResult);
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.AccMenu.Update(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> MenuDelete(UserClaimModel user, int data)
        {
            try
            {
                var repoResult = await _uow.AccMenu.Set().Include(m => m.RoleMenus).FirstOrDefaultAsync(m => m.Id == data);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                if (repoResult.RoleMenus.Any())
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Menu data used on role. Please delete the role first!");

                _uow.AccMenu.Remove(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
        #endregion

    }
}
