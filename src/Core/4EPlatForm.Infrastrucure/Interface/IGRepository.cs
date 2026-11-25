using _4EPlatForm.Infrastrucure.ResponseDTO;
using System.Linq.Expressions;

namespace _4EPlatForm.Infrastrucure.Repostories
{
    public interface IGRepository<T> where T : class
    {
        //Task DeleteRangeAsync(ICollection<T> entities);
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        //Task DeleteAsync(T entity);
        Task RemoveRange(ICollection<T> entities);

        IQueryable<T> GetAllNoTracking();
        IQueryable<T> GetAllAsTracking();
        IQueryable<T> GetAllAsTracking(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllAsTracking(Expression<Func<T, bool>> where, int pageIndex, int pageSize, ref ResponsePagination responseDto);
        IQueryable<T> GetAllAsTracking(int pageNumber, int pageSize, ref ResponsePagination responseDto);
        IQueryable<T> GetAllAsNoTracking(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllAsNoTrackingDescending(Expression<Func<T, bool>> where, Expression<Func<T, DateTime>> orderByDescending, int pageNumber, int pageSize, ref ResponsePagination responseDto);
        IQueryable<T> GetAllAsNoTracking(Expression<Func<T, bool>> where, int pageIndex, int pageSize, ref ResponsePagination responseDto);
        IQueryable<T> GetAllAsNoTracking(int pageNumber, int pageSize, ref ResponsePagination responseDto);
        IQueryable<T> GetAllAsTracking(Expression<Func<T, bool>> where, int pageNumber, int pageSize, ref ResponsePagination responseDto, Expression<Func<T, object>> order, bool orderDesc);


    }
}
