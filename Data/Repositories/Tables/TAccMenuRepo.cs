using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TAccMenuRepo : BaseRepository<TAccMenu>, ITAccMenuRepo
    {
        public TAccMenuRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
