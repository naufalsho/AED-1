using Core.Interfaces.IRepositories.Tables;
using Core.Interfaces.IRepositories.Tables.Master;
using Core.Interfaces.IRepositories.Tables.Transaction;
using Core.Models.Entities.Tables;
using Core.Models.Entities.Tables.Master;
using Core.Models.Entities.Tables.Transaction;

namespace Data.Repositories.Tables.Master
{
    public class TTrnImplementRepo : BaseRepository<TTrnImplement>, ITrnImplementRepo
    {
        public TTrnImplementRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
