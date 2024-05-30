using POS.Infraestructura.Commons.Bases.Request;

namespace POS.Infraestructura.helpers
{
    public static class QueryableHelpers
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T>quryable,BasePaginationRequest request)
        {
            return quryable.Skip((request.NumPage - 1) * request.Records).Take(request.Records);
        }
    }
}
