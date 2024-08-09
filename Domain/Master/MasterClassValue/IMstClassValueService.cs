using Core.Models;
using Domain.Account;
using Domain.Master.Class;
using FluentResults;

namespace Domain.Master.ClassValue
{
    public interface IMstClassValueService
    {
        Task<Result<IEnumerable<TMstClassValueDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<TMstClassValueDto>> GetListByParam(TMstClassValueDto param);
        Task<Result<TMstClassValueDto>> GetById(string id);
        Task<Result<TMstClassValueDto>> GetByParam(TMstClassValueDto param);
        Task<Result<TMstClassValueDto>> Create(UserClaimModel user, TMstClassCreatedDto data);
        Task<Result<TMstClassValueDto>> Update(UserClaimModel user, TMstClassCreatedDto data);
        Task<Result<TMstClassValueDto>> Delete(UserClaimModel user, string id);
    }
}
