using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.Transaction.Implement
{
    public interface ITrnImplementService
    {
        Task<Result<IEnumerable<TTrnImplementDto>>> GetListByParam(TTrnImplementFilterDto param);
        Task<Result<TTrnImplementDto>> GetById(int id);

        Task<Result<TTrnImplementDto>> Create(UserClaimModel user, TTrnImplementDto data);
        Task<Result<TTrnImplementDto>> Update(UserClaimModel user, TTrnImplementDto data);
        Task<Result<TTrnImplementDto>> Delete(UserClaimModel user, int id);
    }
}
