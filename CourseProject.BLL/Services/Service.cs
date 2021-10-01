using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CourseProject.DAL.EF;
using Serilog;


namespace CourseProject.BLL.Services
{
    public class Service<TModel, TEntity> : IService<TModel, TEntity> where TModel : class where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TModel>> GetAsync()
        {
            IEnumerable<TEntity> entities = await _repository.GetAsync();
            return _mapper.Map<IEnumerable<TModel>>(entities);
        }

        public async Task<TModel> FindByIdAsync(Guid id)
        {
            TEntity entity = await _repository.FindByIdAsync(id);
            return _mapper.Map<TModel>(entity);
        }
        

        public async Task<IEnumerable<TModel>> GetAsync(Func<TEntity, bool> predicate)
        {
            IEnumerable<TEntity> entity = await _repository.GetAsync(predicate);
            return _mapper.Map<IEnumerable<TModel>>(entity);
        }

        public async Task<bool> CreateAsync(TModel tModel)
        {
            TEntity dbEntity;
            try
            {
                TEntity tEntity = _mapper.Map<TEntity>(tModel);
                await _repository.CreateAsync(tEntity);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log.Error(e.Message);
            }

            return false;
        }
        /*public async Task<TModel> CreateAsync(TModel tModel, Guid id)
        {
            TEntity dbEntity;
            try
            {
                TEntity tEntity = _mapper.Map<TEntity>(tModel);
                await _repository.CreateAsync(tEntity);
                dbEntity = await _repository.FindByIdAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Log.Error(e.Message);
                return null;
            }

            return _mapper.Map<TModel>(dbEntity);
        }*/

        public bool RemoveAsync(TModel tModel)
        {
            try
            {
                TEntity tEntity =
                    _mapper.Map<TEntity>(tModel);

                _repository.RemoveAsync(tEntity);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }

            return true;
        }

        public bool UpdateAsync(TModel tModel)
        {
            try
            {
                TEntity tEntity = _mapper.Map<TEntity>(tModel);
                _repository.UpdateAsync(tEntity);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }

            return true;
        }
        
    }
}