using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwDsDeviceStockByCategoryRepo : BaseRepository<VwDsDeviceStockByCategory>, IVwDsDeviceStockByCategoryRepo
    {
        public VwDsDeviceStockByCategoryRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
