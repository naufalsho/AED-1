using Core.Models.Entities.Views;
using FluentResults;

namespace Domain.UnitSpec
{
    public interface IUnitSpecService
    {
        Task<Result<IEnumerable<UnitSpecDto>>> GetAllByParam(UnitSpecFilterDto param);
    }
}
