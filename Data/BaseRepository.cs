using Core.Extensions;
using Core.Helpers;
using Core.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<T> 
        
        Set()
        {
            return _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetPagedData(IPagingCondition<T> condition)
        {
            IQueryable<T> data = _context.Set<T>();

            if (condition.Where != null)
            {
                data = data.Where(condition.Where);
            }

            if (!string.IsNullOrEmpty(condition.OrderBy))
            {
                data = data.OrderBy(condition.OrderBy, condition.IsOrderDesc.Value);
            }

            if (condition.Skip.HasValue && condition.Take.HasValue)
            {
                data = data.Skip(condition.Skip.Value).Take(condition.Take.Value);
            }

            return await data.ToListAsync();
        }

        public async Task<int> CountData(IPagingCondition<T> condition)
        {
            IQueryable<T> data = _context.Set<T>();

            if (condition.Where != null)
            {
                data = data.Where(condition.Where);
            }

            return await data.CountAsync();
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }


		//public async Task<IEnumerable<T>> ExecuteStoredProcedure(string storedProcedureName, params Microsoft.Data.SqlClient.SqlParameter[] parameters)
		//{
		//    var sqlParams = parameters != null ? parameters.ToList() : new List<Microsoft.Data.SqlClient.SqlParameter>();
		//    var sqlQuery = new System.Text.StringBuilder();
		//    sqlQuery.Append($"EXEC {storedProcedureName} ");

		//    for (int i = 0; i < sqlParams.Count; i++)
		//    {
		//        var param = sqlParams[i];
		//        sqlQuery.Append($"@{i}");
		//        if (i < sqlParams.Count - 1)
		//        {
		//            sqlQuery.Append(", ");
		//        }
		//    }

		//    var queryString = sqlQuery.ToString();
		//    return await _context.Set<T>().FromSqlRaw(queryString, sqlParams.ToArray()).ToListAsync();
		//}
		//public async Task<IEnumerable<T>> ExecuteStoredProcedure(string storedProcedureName, params Microsoft.Data.SqlClient.SqlParameter[] parameters)
		//{
		//    var sqlParams = parameters != null ? parameters.ToList() : new List<Microsoft.Data.SqlClient.SqlParameter>();
		//    var sqlQuery = new System.Text.StringBuilder();
		//    sqlQuery.Append($"EXEC {storedProcedureName} ");

		//    for (int i = 0; i < sqlParams.Count; i++)
		//    {
		//        var param = sqlParams[i];
		//        sqlQuery.Append($"@{param.ParameterName} = {param[i]}");
		//        if (i < sqlParams.Count - 1)
		//        {
		//            sqlQuery.Append(", ");
		//        }
		//    }

		//    var queryString = sqlQuery.ToString();
		//    return await _context.Set<T>().FromSqlRaw(queryString, sqlParams.ToArray()).ToListAsync();
		//}

		public async Task<IEnumerable<T>> ExecuteStoredProcedure(string storedProcedureName, params Microsoft.Data.SqlClient.SqlParameter[] parameters)
		{
			var sqlParams = parameters != null ? parameters.ToList() : new List<Microsoft.Data.SqlClient.SqlParameter>();
			var sqlQuery = new System.Text.StringBuilder();
			sqlQuery.Append($"EXEC {storedProcedureName} ");

			for (int i = 0; i < sqlParams.Count; i++)
			{
				var param = sqlParams[i];
				sqlQuery.Append($"@{param.ParameterName} = ");

				// Tambahkan tanda kutip jika nilai parameter adalah string
				if (param.SqlDbType == System.Data.SqlDbType.NVarChar || param.SqlDbType == System.Data.SqlDbType.VarChar)
				{
					sqlQuery.Append($"'{param.Value}'");
				}
				else
				{
					sqlQuery.Append(param.Value);
				}

				if (i < sqlParams.Count - 1)
				{
					sqlQuery.Append(", ");
				}
			}

			var queryString = sqlQuery.ToString();
			return await _context.Set<T>().FromSqlRaw(queryString, sqlParams.ToArray()).ToListAsync();
		}



	}
}
