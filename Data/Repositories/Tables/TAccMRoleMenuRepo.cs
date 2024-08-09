using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TAccMRoleMenuRepo : BaseRepository<TAccMRoleMenu>, ITAccMRoleMenuRepo
    {
        public TAccMRoleMenuRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
