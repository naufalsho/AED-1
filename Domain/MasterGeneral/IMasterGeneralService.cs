using Core.Models;
using FluentResults;

namespace Domain.MasterGeneral
{
    public interface IMasterGeneralService
    {
        Task<Result<IEnumerable<MstGeneralDto>>> GetList(string mdType);
        Task<Result<MstGeneralDto>> GetById(int id);
        Task<Result<MstGeneralDto>> Create(UserClaimModel user, string mdType, MstGeneralDto data);
        Task<Result<MstGeneralDto>> Update(UserClaimModel user, MstGeneralUpdateDto data);
        Task<Result<MstGeneralDto>> Delete(UserClaimModel user, int id);
    }
}
