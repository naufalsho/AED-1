using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwComparisonRepo : BaseRepository<VwComparison>, IVwComparisonRepo
    {
        public VwComparisonRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
