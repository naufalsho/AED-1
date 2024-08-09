using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Transaction.SpecValues
{
    public interface ITrnSpecValuesService
    {
        Task<Result<IEnumerable<TTrnSpecValuesDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<IEnumerable<TTrnSpecValuesDto>>> GetListByParam(TTrnSpecValuesFilterDto param);
        Task<Result<TTrnSpecValuesDto>> GetById(int id);
        Task<Result<TTrnSpecValuesDto>> GetByParam(TTrnSpecValuesFilterDto param);

        Task<Result<TTrnSpecValuesDto>> Create(UserClaimModel user, TTrnSpecValuesCreatedDto data);
        Task<Result<TTrnSpecValuesDto>> Update(UserClaimModel user, TTrnSpecValuesCreatedDto data);
        Task<Result<TTrnSpecValuesDto>> Delete(UserClaimModel user, int id);
    }
}
