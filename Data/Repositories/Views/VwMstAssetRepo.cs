using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwMstAssetRepo : BaseRepository<VwMstAsset>, IVwMstAssetRepo
    {
        public VwMstAssetRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
