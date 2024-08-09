using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities.Tables;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Domain.AccessRole
{
    public class AccessRoleService : IAccessRoleService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AccessRoleService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<RoleDto>>> GetList()
        {
            try
            {
                var repoResult = await _uow.AccRole.Set().Where(m => !m.IsDelete).ToListAsync();

                var result = _mapper.Map<IEnumerable<RoleDto>>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<List<RoleMenuDto>> GetRoleMenu()
        {
            var repoResult = await _uow.AccMenu.Set()
                .Include(m => m.MenuGroup)
                .Where(m => m.IsActive)
                .OrderBy(m => m.MenuGroup.Order).ThenBy(m => m.Order)
                .ToListAsync();

            var result = repoResult.Select(m => new RoleMenuDto()
            {
                MenuName = m.Name,
                GroupName = m.MenuGroup.Name,
                MenuId = m.Id,
                AllowView = false,
                AllowCreate = false,
                AllowEdit = false,
                AllowDelete = false
            });

            return result.ToList();
        }

        public async Task<Result<RoleDto>> GetById(int id)
        {
            try
            {
                var repoResult = await _uow.AccRole.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                var result = _mapper.Map<RoleDto>(repoResult);

                var roleMenus = await _uow.AccMRoleMenu.Set()
                    .Where(m => m.RoleId == id)
                    .ToListAsync();

                var menus = await GetRoleMenu();

                foreach (var menu in menus)
                {
                    var roleMenu = roleMenus.FirstOrDefault(m => m.MenuId == menu.MenuId);
                    if (roleMenu != null)
                    {
                        menu.AllowView = roleMenu.AllowView;
                        menu.AllowCreate = roleMenu.AllowCreate;
                        menu.AllowEdit = roleMenu.AllowEdit;
                        menu.AllowDelete = roleMenu.AllowDelete;
                    }
                }

                result.RoleMenus = menus;

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Create(UserClaimModel user, RoleDto data)
        {
            try
            {
                var checkOrder = await _uow.AccRole.Set().FirstOrDefaultAsync(m => m.Name == data.Name && !m.IsDelete);

                if (checkOrder != null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Role name used. Please change the role name!");

                var dataToAdd = _mapper.Map<TAccRole>(data);
                dataToAdd.CreatedBy = user.NameIdentifier;
                dataToAdd.CreatedDate = DateTime.Now;

                dataToAdd.RoleMenus = data.RoleMenus
                    .Where(m => m.AllowView || m.AllowCreate || m.AllowEdit || m.AllowDelete)
                    .Select(m => new TAccMRoleMenu()
                    {
                        MenuId = m.MenuId,
                        AllowView = m.AllowView,
                        AllowCreate = m.AllowCreate,
                        AllowEdit = m.AllowEdit,
                        AllowDelete = m.AllowDelete,
                        CreatedBy = user.NameIdentifier,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                    })
                    .ToList();

                await _uow.AccRole.Add(dataToAdd);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Update(UserClaimModel user, RoleUpdateDto data)
        {
            try
            {
                var repoResult = await _uow.AccRole.Set().Include(m => m.RoleMenus).FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                _mapper.Map(data, repoResult);
                repoResult.UpdatedBy = user.NameIdentifier;
                repoResult.UpdatedDate = DateTime.Now;
                repoResult.RoleMenus = null;

                repoResult.RoleMenus = data.RoleMenus
                    .Where(m => m.AllowView || m.AllowCreate || m.AllowEdit || m.AllowDelete)
                    .Select(m => new TAccMRoleMenu()
                    {
                        RoleId = data.Id,
                        MenuId = m.MenuId,
                        AllowView = m.AllowView,
                        AllowCreate = m.AllowCreate,
                        AllowEdit = m.AllowEdit,
                        AllowDelete = m.AllowDelete,
                        CreatedBy = user.NameIdentifier,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                    })
                    .ToList();

                _uow.AccRole.Update(repoResult);
                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> Delete(UserClaimModel user, int id)
        {
            try
            {
                var repoResult = await _uow.AccRole.Set().FirstOrDefaultAsync(m => m.Id == id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Data not found!");

                repoResult.IsActive = false;
                repoResult.IsDelete = true;
                repoResult.DeletedBy = user.NameIdentifier;
                repoResult.DeletedDate = DateTime.Now;

                _uow.AccRole.Update(repoResult);

                var roleMenus = await _uow.AccMRoleMenu.Set().Where(m => m.RoleId == id).ToListAsync();
                _uow.AccMRoleMenu.RemoveRange(roleMenus);

                await _uow.CompleteAsync();

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }
    }
}
