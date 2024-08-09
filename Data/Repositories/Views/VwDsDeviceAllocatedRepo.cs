using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwDsDeviceAllocatedRepo : BaseRepository<VwDsDeviceAllocated>, IVwDsDeviceAllocatedRepo
    {
        public VwDsDeviceAllocatedRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
