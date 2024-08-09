using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TtMstYardAreaRepo : BaseRepository<TtMstYardArea>, ITtMstYardAreaRepo
    {
        public TtMstYardAreaRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
