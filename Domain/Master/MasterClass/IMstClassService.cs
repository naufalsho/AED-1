using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master.Class
{
    public interface IMstClassService
    {
        Task<Result<IEnumerable<TMstClassDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<TMstClassDto>> GetListByParam(TMstClassDto param);
        Task<Result<TMstClassDto>> GetById(string id);
        Task<Result<TMstClassDto>> GetByParam(TMstClassDto param);
        Task<Result<TMstClassDto>> Create(UserClaimModel user, TMstClassCreatedDto data);
        Task<Result<TMstClassDto>> Update(UserClaimModel user, TMstClassCreatedDto data);
        Task<Result<TMstClassDto>> Delete(UserClaimModel user, string id);
    }
}
