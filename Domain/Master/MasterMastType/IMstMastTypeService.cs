using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master.MastType
{
    public interface IMstMastTypeService
    {
        Task<Result<IEnumerable<TMstMastTypeDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<TMstMastTypeDto>> GetListByParam(TMstMastTypeDto param);
        Task<Result<TMstMastTypeDto>> GetById(string id);
        Task<Result<TMstMastTypeDto>> GetByParam(TMstMastTypeDto param);
        Task<Result<TMstMastTypeDto>> Create(UserClaimModel user, TMstMastTypeCreatedDto data);
        Task<Result<TMstMastTypeDto>> Update(UserClaimModel user, TMstMastTypeCreatedDto data);
        Task<Result<TMstMastTypeDto>> Delete(UserClaimModel user, string id);
    }
}
