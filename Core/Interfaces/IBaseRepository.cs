using Core.Helpers;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Set();
        Task<IEnumerable<T>> GetPagedData(IPagingCondition<T> condition);
        Task<int> CountData(IPagingCondition<T> condition);

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> ExecuteStoredProcedure(string storedProcedureName, params Microsoft.Data.SqlClient.SqlParameter[] parameters);

    }
}
