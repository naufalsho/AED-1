using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TMstEmployeeRepo : BaseRepository<TMstEmployee>, ITMstEmployeeRepo
    {
        public TMstEmployeeRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
