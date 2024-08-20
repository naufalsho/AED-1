using AutoMapper;
using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Domain.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AccountService(
            IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<UserSessionDto>> Authenticate(AuthLoginDto data)
        {
            try
            {
                var repoResult = await _uow.AccUser.Set()
                    .Include(m => m.Role)
                    .FirstOrDefaultAsync(m => m.UserId == data.UserId && !m.IsDelete);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":User not found!");

                if (!repoResult.IsActive)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":User not active. Please contact your administrator!");

                if (!BCryptNet.Verify(data.Password, repoResult.Password))
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Password not match!");

                var result = _mapper.Map<UserSessionDto>(repoResult);

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result<IEnumerable<UserMenuGroupDto>>> GetUserMenu(int roleId)
        {
            try
            {
                var repoResult = await _uow.VwAccUserMenu.Set()
                    .Where(m => m.RoleId == roleId)
                    .ToListAsync();

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.NoContent);

                var groupMenus = repoResult
                    .Select(m => new UserMenuGroupDto()
                    {
                        Order = m.GroupOrder,
                        Name = m.GroupName,
                        Icon = m.GroupIcon,
                        IsDirectMenu = m.IsDirectMenu,
                        IsActive = m.MG_IsActive
                    })
                    .OrderBy(m => m.Order)
                    .GroupBy(m => m.Order);

                var result = new List<UserMenuGroupDto>();

                foreach (var groupMenu in groupMenus)
                {
                    var dataToAdd = groupMenus.Single(m => m.Key == groupMenu.Key).FirstOrDefault();

                    dataToAdd.Menus = repoResult.Where(m => m.GroupOrder == groupMenu.Key)
                        .Select(m => new UserMenuDto()
                        {
                            Order = m.MenuOrder,
                            Name = m.MenuName,
                            Controller = m.MenuController,
                            AllowView = m.AllowView,
                            AllowCreate = m.AllowCreate,
                            AllowEdit = m.AllowEdit,
                            AllowDelete = m.AllowDelete,
                            IsActive = m.M_IsActive
                        })
                        .OrderBy(m => m.Order);

                    result.Add(dataToAdd);
                }

                return Result.Ok(result.AsEnumerable());
            }
            catch (Exception ex)
            {
                return Result.Fail(ResponseStatusCode.InternalServerError + ":" + ex.GetMessage());
            }
        }

        public async Task<Result> ChangePassword(ChangePasswordDto data)
        {
            try
            {
                if (data.OldPassword == data.NewPassword)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Please use different password!");

                if (data.NewPassword != data.NewPasswordConfirm)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":New password and confirm new password not match!");

                var repoResult = await _uow.AccUser.Set().FirstOrDefaultAsync(m => m.Id == data.Id);

                if (repoResult == null)
                    return Result.Fail(ResponseStatusCode.BadRequest + ":User not found!");

                if (!BCryptNet.Verify(data.OldPassword, repoResult.Password))
                    return Result.Fail(ResponseStatusCode.BadRequest + ":Old password not match!");

                repoResult.Password = BCryptNet.HashPassword(data.NewPassword);
                repoResult.UpdatedBy = repoResult.UserId;
                repoResult.UpdatedDate = DateTime.Now;

                _uow.AccUser.Update(repoResult);
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
