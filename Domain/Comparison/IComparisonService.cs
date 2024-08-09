using Core.Models.Entities.Views;
using FluentResults;

namespace Domain.Comparison
{
    public interface IComparisonService
    {
        Task<Result<IEnumerable<ComparisonDto>>> Generate(ComparisonFilterDto param);
    }
}
