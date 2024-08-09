using Core.Models;
using FluentResults;

namespace Domain.AccessRole
{
    public interface IAccessRoleService
    {
        Task<Result<IEnumerable<RoleDto>>> GetList();
        Task<List<RoleMenuDto>> GetRoleMenu();
        Task<Result<RoleDto>> GetById(int id);
        Task<Result> Create(UserClaimModel user, RoleDto data);
        Task<Result> Update(UserClaimModel user, RoleUpdateDto data);
        Task<Result> Delete(UserClaimModel user, int data);
    }
}
