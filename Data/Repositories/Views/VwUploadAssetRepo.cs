using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;

namespace Data.Repositories.Views
{
    public class VwUploadAssetRepo : BaseRepository<VwUploadAsset>, IVwUploadAssetRepo
    {
        public VwUploadAssetRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
