using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TAccUserRepo : BaseRepository<TAccUser>, ITAccUserRepo
    {
        public TAccUserRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
