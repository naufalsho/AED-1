using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwAccUserMenuRepo : BaseRepository<VwAccUserMenu>, IVwAccUserMenuRepo
    {
        public VwAccUserMenuRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
