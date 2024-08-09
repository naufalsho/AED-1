using Core.Models;
using FluentResults;

namespace Domain.AccessUser
{
    public interface IAccessUserService
    {
        Task<Result<IEnumerable<UserDto>>> GetList();
        Task<Result<UserDto>> GetById(int id);
        Task<Result> Create(UserClaimModel user, UserDto data);
        Task<Result> Update(UserClaimModel user, UserUpdateDto data);
        Task<Result> Delete(UserClaimModel user, int data);
    }
}
