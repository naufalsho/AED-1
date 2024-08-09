using Core.Interfaces.IRepositories.Views;
using Core.Models.Entities.Views;
using Core.Models.Entities.Views.Transaction;

namespace Data.Repositories.Views
{
    public class VwSpecValueMatriksRepo : BaseRepository<VwSpecValueMatriks>, IVwSpecValueMatriksRepo
    {
        public VwSpecValueMatriksRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
