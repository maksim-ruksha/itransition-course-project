using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.EF
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;


        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public Repository()
        {
            _context = new DatabaseContext();
            _dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            IEnumerable<T> dbItemEnumerable = await GetAsync(x => x.Equals(item));
            return dbItemEnumerable.FirstOrDefault();
        }


        public async Task<T> FindByIdAsync(Guid id)
        {
            T model = await _dbSet.FindAsync(id);
            return model;
        }


        /*public async Task<T> FindAsync(T item)
        {
            return await _dbSet.FindAsync(item);
        }*/

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await Task.Run(() => _dbSet.ToList());
        }

        public async Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => _dbSet.Where(predicate));
        }

        public async void RemoveAsync(T item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        public void Attach(T item)
        {
            _context.Attach(item);
        }

        public IEnumerable<T> AsNoTracking(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public void DetachLocal(T item)
        {
            T localItem = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Equals(item));
            if (localItem != null)
            {
                _context.Entry(localItem).State = EntityState.Detached;
            }

            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}