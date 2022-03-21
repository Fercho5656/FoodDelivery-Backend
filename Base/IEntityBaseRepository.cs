using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FoodDelivery_Backend.Base {
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new() {

        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);
        Task<T> Get(int id);
        Task<T> Get(int id, params Expression<Func<T, object>>[] includeProperties);
        Task Add(T entity);
        Task<T> Update(int id, T entity);
        Task Delete(int id);
    }
}