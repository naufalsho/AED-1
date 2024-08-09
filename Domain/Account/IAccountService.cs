using FluentResults;

namespace Domain.Account
{
    public interface IAccountService
    {
        Task<Result<UserSessionDto>> Authenticate(AuthLoginDto data);
        Task<Result<IEnumerable<UserMenuGroupDto>>> GetUserMenu(int roleId);
        Task<Result> ChangePassword(ChangePasswordDto data);
    }
}
