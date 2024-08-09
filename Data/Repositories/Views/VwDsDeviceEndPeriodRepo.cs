using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwDsDeviceEndPeriodRepo : BaseRepository<VwDsDeviceEndPeriod>, IVwDsDeviceEndPeriodRepo
    {
        public VwDsDeviceEndPeriodRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
