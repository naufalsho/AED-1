using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master.LiftingHeight
{
    public interface IMstLiftingHeightService
    {
        Task<Result<IEnumerable<TMstLiftingHeightDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<TMstLiftingHeightDto>> GetListByParam(TMstLiftingHeightDto param);
        Task<Result<TMstLiftingHeightDto>> GetById(string id);
        Task<Result<TMstLiftingHeightDto>> GetByParam(TMstLiftingHeightDto param);
        Task<Result<TMstLiftingHeightDto>> Create(UserClaimModel user, TMstLiftingHeightCreatedDto data);
        Task<Result<TMstLiftingHeightDto>> Update(UserClaimModel user, TMstLiftingHeightCreatedDto data);
        Task<Result<TMstLiftingHeightDto>> Delete(UserClaimModel user, string id);
    }
}
