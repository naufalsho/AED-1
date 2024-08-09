using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TMstGeneralRepo : BaseRepository<TMstGeneral>, ITMstGeneralRepo
    {
        public TMstGeneralRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
