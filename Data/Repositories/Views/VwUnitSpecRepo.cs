using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwUnitSpecRepo : BaseRepository<VwUnitSpec>, IVwUnitSpecRepo
    {
        public VwUnitSpecRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
