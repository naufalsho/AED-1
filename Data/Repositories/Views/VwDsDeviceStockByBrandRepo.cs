using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwDsDeviceStockByBrandRepo : BaseRepository<VwDsDeviceStockByBrand>, IVwDsDeviceStockByBrandRepo
    {
        public VwDsDeviceStockByBrandRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
