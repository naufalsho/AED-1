using Core.Interfaces.IRepositories.Tables;
using Core.Interfaces.IRepositories.Tables.Master;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;

namespace Data.Repositories.Tables.Master
{
    public class TTrnSpecValuesRepo : BaseRepository<TTrnSpecValues>, ITrnSpecValuesRepo
    {
        public TTrnSpecValuesRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
