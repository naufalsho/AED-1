using Core.Models.Entities.Views;
using FluentResults;

namespace Domain.Comparison
{
    public interface IImplementService
    {
        Task<Result<ImplementDto>> GetImplement(ImplementDto param);
        Task<Result<ImplementDto>> GetProductModel(ImplementDto param);
    }
}
