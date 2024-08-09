using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwDsDeviceStockByStatusRepo : BaseRepository<VwDsDeviceStockByStatus>, IVwDsDeviceStockByStatusRepo
    {
        public VwDsDeviceStockByStatusRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
