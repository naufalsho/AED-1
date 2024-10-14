using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master.Tire
{
    public interface IMstTireService
    {
        Task<Result<IEnumerable<TMstTireDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<TMstTireDto>> GetListByParam(TMstTireDto param);
        Task<Result<TMstTireDto>> GetById(string id);
        Task<Result<TMstTireDto>> GetByParam(TMstTireDto param);
        Task<Result<TMstTireDto>> Create(UserClaimModel user, TMstTireCreatedDto data);
        Task<Result<TMstTireDto>> Update(UserClaimModel user, TMstTireCreatedDto data);
        Task<Result<TMstTireDto>> Delete(UserClaimModel user, string id);
    }
}
