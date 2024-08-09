using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.MasterYardArea
{
    public interface IYardAreaService
    {
        Task<Result<IEnumerable<YardAreaDto>>> GetList();
        Task<Result<IEnumerable<YardAreaDto>>> GetAll();
        Task<Result<YardAreaDto>> GetById(int id);
        Task<Result<YardAreaDto>> GetByParam(YardAreaFilterDto param);
        Task<Result<YardAreaDto>> Create(UserClaimModel user, YardAreaDto data);
        Task<Result<YardAreaDto>> Update(UserClaimModel user, YardAreaDto data);
        Task<Result<YardAreaDto>> Delete(UserClaimModel user, int id);
    }
}
