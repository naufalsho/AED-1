using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TAccRoleRepo : BaseRepository<TAccRole>, ITAccRoleRepo
    {
        public TAccRoleRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
