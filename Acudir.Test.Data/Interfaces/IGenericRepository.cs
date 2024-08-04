using System.Linq.Expressions;

namespace Acudir.Test.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Retorna todos los registros de la entidad mediante una condicion.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// (Async) Retorna los registros de una entidad por medio de las pks
        /// </summary>
        /// <param name="pkeys"></param>
        /// <returns></returns>
        ValueTask<TEntity?> FindByIdAsync(params object[] pkeys);

        /// <summary>
        /// Retorna la entidad.
        /// </summary>
        IQueryable<TEntity> Entity { get; }
        /// <summary>
        /// Retorna todos los registros. 
        /// </summary>
        public Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Retorna la entidad SIN trackeo.
        /// </summary>
        IQueryable<TEntity> EntityNoTracking { get; }

        /// <summary>
        /// Agrega un nuevo registro a la entidad.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Agrega una coleccion de registros a la entidad.
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Actualiza un registro de la entidad.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Actualiza una coleccion de registros de la entidad.
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Elimina un registro de la entidad.
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Elimina una seria de registros de la entidad.
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// (Async) Elimina un registro de la entidad por pk.
        /// </summary>
        /// <param name="pkeys"></param>
        /// <returns></returns>
        Task RemoveByIdAsync(params object[] pkeys);

        /// <summary>
        /// (Async) Realiza commit de los cambios.
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Realiza commit de los cambios.
        /// </summary>
        /// <returns></returns>
        int Commit();

    }
}
