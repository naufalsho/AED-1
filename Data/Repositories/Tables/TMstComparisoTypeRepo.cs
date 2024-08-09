using Core.Interfaces.IRepositories.Tables;
using Core.Models.Entities.Tables;

namespace Data.Repositories.Tables
{
    public class TMstComparisonTypeRepo : BaseRepository<TMstComparisonType>, IMstComparisonTypeRepo
    {
        public TMstComparisonTypeRepo(ApplicationContext context) : base(context)
        {
        }
    }
}
