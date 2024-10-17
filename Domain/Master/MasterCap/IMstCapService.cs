using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master.Cap
{
    public interface IMstCapService
    {
        Task<Result<IEnumerable<TMstCapDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<TMstCapDto>> GetListByParam(TMstCapDto param);
        Task<Result<TMstCapDto>> GetById(string id);
        Task<Result<TMstCapDto>> GetByParam(TMstCapDto param);
        Task<Result<TMstCapDto>> Create(UserClaimModel user, TMstCapCreatedDto data);
        Task<Result<TMstCapDto>> Update(UserClaimModel user, TMstCapCreatedDto data);
        Task<Result<TMstCapDto>> Delete(UserClaimModel user, string id);
        Task<Result<IEnumerable<TMstCapDto>>> GetByBrand(string brandCode, string classCode, string distributor);
    }
}
