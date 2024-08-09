using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwDsDeviceYoYRepo : BaseRepository<VwDsDeviceYoY>, IVwDsDeviceYoYRepo
    {
        public VwDsDeviceYoYRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
