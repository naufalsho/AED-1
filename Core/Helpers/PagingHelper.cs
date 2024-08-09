using Core.Models;
using System.Linq.Expressions;

namespace Core.Helpers
{
    public interface IPagingCondition<T> where T : class
    {
        Expression<Func<T, bool>> Where { get; }
        string OrderBy { get; }
        bool? IsOrderDesc { get; }
        int? Skip { get; }
        int? Take { get; }
    }

    public class PagingCondition<T> : IPagingCondition<T> where T : class
    {
        public Expression<Func<T, bool>> Where { get; }
        public string OrderBy { get; }
        public bool? IsOrderDesc { get; }
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        public PagingCondition(Expression<Func<T, bool>> where, string orderBy, bool isOrderDesc, int? skip, int? take)
        {
            Where = where;
            OrderBy = orderBy;
            IsOrderDesc = isOrderDesc;
            Skip = skip;
            Take = take;
        }

        public PagingCondition(Expression<Func<T, bool>> where, PagingRequest pRequest)
        {
            var tOrder = pRequest.Order;
            var tOrderCol = pRequest.Parameters[tOrder.ParameterIndex];

            bool _orderDesc = true;
            if (tOrder.Dir.ToLower() == "asc")
            {
                _orderDesc = false;
            }

            if (pRequest.Page > 0)
            {
                pRequest.Page--;
            }

            Where = where;
            OrderBy = tOrderCol.Name;
            IsOrderDesc = _orderDesc;
            Skip = pRequest.Page * pRequest.Length;
            Take = pRequest.Length;
        }
    }
}
