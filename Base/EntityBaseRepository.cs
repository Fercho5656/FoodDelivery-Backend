using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoodDelivery_Backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodDelivery_Backend.Base {
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new() {

        private readonly AppDbContext _context;
        public EntityBaseRepository(AppDbContext context) {
            _context = context;
        }
        public async Task Add(T entity) {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id) {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<T> Get(int id) => await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);

        public async Task<T> Get(int id, params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties));
            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties) {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> Update(int id, T entity) {
            var existingEntity = await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);
            if (existingEntity != null) {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return existingEntity;
        }
    }
}