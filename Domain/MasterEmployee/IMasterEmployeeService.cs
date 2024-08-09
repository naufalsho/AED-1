using Core.Models;
using FluentResults;

namespace Domain.MasterEmployee
{
    public interface IMasterEmployeeService
    {
        Task<Result<IEnumerable<MstEmployeeDto>>> GetList();
        Task<Result<MstEmployeeDto>> GetById(int id);
        Task<Result<MstEmployeeDto>> GetByNRP(string NRP);
        Task<Result> Create(UserClaimModel user, MstEmployeeDto data);
        Task<Result> Update(UserClaimModel user, MstEmployeeUpdateDto data);
        Task<Result> Delete(UserClaimModel user, int id);
    }
}
