using Core.Models;
using Domain.Account;
using Domain.Master.MasterModel;
using FluentResults;

namespace Domain.Master.MasterSpecItem
{
    public interface ITMstSpecItemsService
    {
        Task<Result<IEnumerable<TMstSpecItemsDto>>> GetAll();
        Task<Result<string>> GetLastCode();
        Task<Result<TMstSpecItemsDto>> GetListByParam(TMstSpecItemsDto param);
        Task<Result<TMstSpecItemsDto>> GetById(string id);
        Task<Result<TMstSpecItemsDto>> GetByParam(TMstSpecItemsDto param);
        Task<Result<TMstSpecItemsDto>> Create(UserClaimModel user, TMstSpecItemsCreatedDto data);
        Task<Result<TMstSpecItemsDto>> Update(UserClaimModel user, TMstSpecItemsCreatedDto data);
        Task<Result<TMstSpecItemsDto>> Delete(UserClaimModel user, string id);
    }
}
