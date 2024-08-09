using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master.MasterModel
{
    public interface IMstModelService
    {
        Task<Result<IEnumerable<TMstModelDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<IEnumerable<TMstModelDto>>> GetByCategory(string categoryCode);
        Task<Result<IEnumerable<TMstModelDto>>> GetByBrand(string brandCode);
        Task<Result<TMstModelDto>> GetById(string id);
        Task<Result<TMstModelDto>> GetByParam(TMstModelDto param);
        Task<Result<TMstModelDto>> Create(UserClaimModel user, TMstModelCreatedDto data);
        Task<Result<TMstModelDto>> Update(UserClaimModel user, TMstModelCreatedDto data);
        Task<Result<TMstModelDto>> Delete(UserClaimModel user, string id);
    }
}
