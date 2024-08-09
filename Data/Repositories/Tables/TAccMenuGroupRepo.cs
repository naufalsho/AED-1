using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TAccMenuGroupRepo : BaseRepository<TAccMenuGroup>, ITAccMenuGroupRepo
    {
        public TAccMenuGroupRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
