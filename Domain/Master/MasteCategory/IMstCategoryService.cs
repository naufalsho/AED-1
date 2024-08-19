using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master.MasterCategory
{
    public interface IMstCategoryService
    {
        Task<Result<IEnumerable<TMstCategoryDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<IEnumerable<TMstCategoryDto>>> GetListByParam(TMstCategoryDto param);
        Task<Result<TMstCategoryDto>> GetById(string id);
        Task<Result<TMstCategoryDto>> GetByParam(TMstCategoryDto param);
        Task<Result<TMstCategoryDto>> Create(UserClaimModel user, TMstCategoryCreatedDto data);
        Task<Result<TMstCategoryDto>> Update(UserClaimModel user, TMstCategoryUpdatedDto data);
        Task<Result<TMstCategoryDto>> Delete(UserClaimModel user, string id);
        //Task<Result<IEnumerable<DescriptionGroupDto>>> GetDescriptionGroups();
    }
}
