using Core.Models;
using FluentResults;

namespace Domain.AccessMenu
{
    public interface IAccessMenuService
    {
        Task<Result<IEnumerable<MenuGroupDto>>> GroupGetList();
        Task<Result<MenuGroupDto>> GroupGetById(int id);
        Task<Result> GroupCreate(UserClaimModel user, MenuGroupDto data);
        Task<Result> GroupUpdate(UserClaimModel user, MenuGroupDto data);
        Task<Result> GroupDelete(UserClaimModel user, int data);

        Task<Result<IEnumerable<MenuDto>>> MenuGetList();
        Task<Result<MenuDto>> MenuGetById(int id);
        Task<Result> MenuCreate(UserClaimModel user, MenuDto data);
        Task<Result> MenuUpdate(UserClaimModel user, MenuUpdateDto data);
        Task<Result> MenuDelete(UserClaimModel user, int data);
    }
}
