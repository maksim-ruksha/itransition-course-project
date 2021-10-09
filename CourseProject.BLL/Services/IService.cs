

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseProject.BLL.Services
{
    public interface IService<TModel, TEntity> where TModel: class where TEntity: class
    {

        public Task<IEnumerable<TModel>> GetAsync();
        public Task<IEnumerable<TModel>> GetAsync(Func<TEntity, bool> predicate);
        //public Task<TModel> CreateAsync(TModel item);
        public Task<bool> CreateAsync(TModel item);
        public bool RemoveAsync(TModel item);
        public Task<bool> UpdateAsync(TModel item);

    }
}