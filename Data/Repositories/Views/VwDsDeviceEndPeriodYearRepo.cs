using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwDsDeviceEndPeriodYearRepo : BaseRepository<VwDsDeviceEndPeriodYear>, IVwDsDeviceEndPeriodYearRepo
    {
        public VwDsDeviceEndPeriodYearRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
