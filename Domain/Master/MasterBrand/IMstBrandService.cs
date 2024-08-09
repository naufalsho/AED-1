using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Master
{
    public interface IMstBrandService
    {
        Task<Result<IEnumerable<TMstBrandDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<IEnumerable<TMstBrandDto>>> GetByCategory(string categoryCode);
        Task<Result<IEnumerable<TMstBrandDto>>> GetByClass(string classCode);
        Task<Result<TMstBrandDto>> GetListByParam(TMstBrandDto param);
        Task<Result<TMstBrandDto>> GetById(string id);
        Task<Result<TMstBrandDto>> GetByParam(TMstBrandDto param);
        Task<Result<TMstBrandDto>> Create(UserClaimModel user, TMstBrandDto data);
        Task<Result<TMstBrandDto>> Update(UserClaimModel user, TMstBrandCreateDto data);
        Task<Result<TMstBrandDto>> Delete(UserClaimModel user, string id);
    }
}
