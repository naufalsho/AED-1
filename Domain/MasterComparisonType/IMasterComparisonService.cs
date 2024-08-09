using Core.Models;
using Domain.Account;
using FluentResults;

namespace Domain.MasterComparisonType
{
    public interface IMasterComparisonService
    {
        Task<Result<IEnumerable<MasterComparisonTypeDto>>> GetList();
        //Task<Result<IEnumerable<MasterComparisonTypeDto>>> GetAll();
        //Task<Result<MasterComparisonTypeDto>> GetById(int id);
        //Task<Result<MasterComparisonTypeDto>> GetByParam(MasterComparisonTypeDto param);
        //Task<Result<MasterComparisonTypeDto>> Create(UserClaimModel user, MasterComparisonTypeDto data);
        //Task<Result<MasterComparisonTypeDto>> Update(UserClaimModel user, MasterComparisonTypeDto data);
        //Task<Result<MasterComparisonTypeDto>> Delete(UserClaimModel user, int id);
    }
}
