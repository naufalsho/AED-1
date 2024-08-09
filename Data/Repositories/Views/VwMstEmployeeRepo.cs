using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwMstEmployeeRepo : BaseRepository<VwMstEmployee>, IVwMstEmployeeRepo
    {
        public VwMstEmployeeRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
