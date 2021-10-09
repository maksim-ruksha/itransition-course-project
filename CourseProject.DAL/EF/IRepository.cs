using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.DAL.EF
{
    public interface IRepository<T> where T: class
    {
        public Task<T> CreateAsync(T item);

        public Task<T> FindByIdAsync(Guid id);

        //public Task<T> FindAsync(T item);

        public Task<IEnumerable<T>> GetAsync();

        public Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate);

        public void RemoveAsync(T item);

        public Task UpdateAsync(T item);

    }
}