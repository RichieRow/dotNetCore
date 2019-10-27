using LZY.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LZY.DataAccess.EntityFramework
{
    /// <summary>
    /// 针对 IEntityRepository 的具体实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntityBase, new()
    {
        public SchoolDbContext EntitiesContext { get; set; }
        readonly SchoolDbContext _EntitiesContext;

        public EntityRepository(SchoolDbContext context)
        {
            _EntitiesContext = context;
            EntitiesContext = context;
        }

        public virtual void Save()
        {
            try
            {
                _EntitiesContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // 获取错误信息集合
                var errorMessages = ex.Message;
                var itemErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " Error: ", itemErrorMessage);
                throw new DbUpdateException(exceptionMessage, ex);
            }
        }

        public virtual IQueryable<T> GetAll()
        {
            return _EntitiesContext.Set<T>();
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _EntitiesContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        /// <summary>
        ///  第一个是判断的条件，除了提取本身的对象数据集合外，还提取包含根据表达式提取关联的的对象的集合
        /// </summary>
        /// <param name="predicate">需要直接提取关联类集合数据的表达式集合，通过逗号隔开<</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllMultiCondition(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _EntitiesContext.Set<T>().Where(predicate);
            foreach (var includePropertie in includeProperties)
            {
                query = query.Include(includePropertie);
            }
            return query;
        }

        public virtual T GetSingle(Guid id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public virtual T GetSingle(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> dbSet = _EntitiesContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    dbSet = dbSet.Include(includeProperty);
                }
            }

            var result = dbSet.FirstOrDefault(x => x.Id == id);
            return result;
        }


        public virtual T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            return _EntitiesContext.Set<T>().Where(predicate).FirstOrDefault(predicate);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _EntitiesContext.Set<T>().Where(predicate);
        }

      

        public virtual void Add(T entity)
        {
            _EntitiesContext.Set<T>().Add(entity);
        }

        public virtual void AddAndSave(T entity)
        {
            Add(entity);
            Save();
        }

        public virtual void Edit(T entity)
        {
            //DbEntityEntry dbEntityEntry = _EntitiesContext.Entry(entity);
            _EntitiesContext.Set<T>().Update(entity);
        }

        public virtual void EditAndSave(T entity)
        {
            Edit(entity);
            Save();
        }

        public virtual void EditAndSaveBy(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> newValueExpression)
        {
            var dbSet = _EntitiesContext.Set<T>();
            //dbSet.Where(predicate).Update(newValueExpression);
        }

        public virtual void AddOrEdit(T entity)
        {
            var p = GetAll().FirstOrDefault(x => x.Id == entity.Id);
            if (p == null)
            {
                Add(entity);
            }
            else
            {
                Edit(entity);
            }
        }

        public virtual void AddOrEditAndSave(T entity)
        {
            AddOrEdit(entity);
            Save();
        }

        public virtual void Delete(T entity)
        {
            _EntitiesContext.Set<T>().Remove(entity);
            //DbEntityEntry dbEntityEntry = _EntitiesContext.Entry(entity);
            //dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteAndSave(T entity)
        {
            Delete(entity);
            Save();
        }
       

        public virtual void DeleteAndSaveBy(Expression<Func<T, bool>> predicate)
        {
            var dbSet = _EntitiesContext.Set<T>();
            var toBeDeleteItems = dbSet.Where(predicate);//.Delete();
            foreach (var item in toBeDeleteItems)
            {
                dbSet.Remove(item);
            }

        }

        

        public virtual bool HasInstance(Guid id)
        {
            var dbSet = _EntitiesContext.Set<T>();
            return dbSet.Any(x => x.Id == id);
        }

        public bool HasInstance(Expression<Func<T, bool>> predicate)
        {
            var dbSet = _EntitiesContext.Set<T>();
            return dbSet.Any(predicate);
        }

        //public T1 GetSingleOther<T1>(Guid id)
        //{
        //    var dbSet = _EntitiesContext.Set<T1>();

        //}


        #region 异步方法的具体实现
        public virtual async Task<bool> SaveAsyn()
        {
            await _EntitiesContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IQueryable<T>> GetAllAsyn()
        {
            var dbSet = _EntitiesContext.Set<T>();
            var result = await dbSet.ToListAsync();
            return result.AsQueryable<T>();
        }

        public virtual async Task<List<T>> GetAllListAsyn()
        {
            var dbSet = _EntitiesContext.Set<T>();
            var result = await dbSet.ToListAsync();
            return result;
        }

        public virtual async Task<IQueryable<T>> GetAllIncludingAsyn(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _EntitiesContext.Set<T>(); //.Include(includeProperties);
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            var result = await query.ToListAsync();
            return result.AsQueryable();
        }

        public virtual async Task<T> GetSingleAsyn(Guid id)
        {
            var dbSet = _EntitiesContext.Set<T>();
            var result = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public virtual async Task<T> GetSingleAsyn(Guid id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> dbSet = _EntitiesContext.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    dbSet = dbSet.Include(includeProperty);
                }
            }

            var result = await dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public virtual async Task<IQueryable<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            var result = await _EntitiesContext.Set<T>().Where(predicate).ToListAsync();
            return result.AsQueryable();
        }

        public virtual async Task<bool> HasInstanceAsyn(Guid id)
        {
            return await _EntitiesContext.Set<T>().AnyAsync(x => x.Id == id);
        }

        public virtual async Task<bool> HasInstanceAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _EntitiesContext.Set<T>().AnyAsync(predicate);
        }

        public virtual async Task<bool> AddOrEditAndSaveAsyn(T entity)
        {
            var dbSet = _EntitiesContext.Set<T>();
            var hasInstance = await dbSet.AnyAsync(x => x.Id == entity.Id);
            if (hasInstance)
                dbSet.Update(entity);
            else
                await dbSet.AddAsync(entity);
            try
            {
                await _EntitiesContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

       

      
       
        #endregion
    }
}
