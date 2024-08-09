using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwTransDeployDeviceRepo : BaseRepository<VwTransDeployDevice>, IVwTransDeployDeviceRepo
    {
        public VwTransDeployDeviceRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
