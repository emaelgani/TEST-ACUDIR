using Acudir.Test.Data.Interfaces;
using System.Linq.Expressions;
using System.Text.Json;

namespace Acudir.Test.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly string _filePath;
        private List<T> _entities;

        public GenericRepository(string filePath)
        {
            _filePath = filePath;
            _entities = ReadFromFile();
        }

        private List<T> ReadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                return new List<T>();
            }

            var jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
        }

        private void WriteToFile()
        {
            var jsonString = JsonSerializer.Serialize(_entities);
            File.WriteAllText(_filePath, jsonString);
        }

        public IQueryable<T> Entity => _entities.AsQueryable();

        public IQueryable<T> EntityNoTracking => _entities.AsQueryable();

        public Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(_entities);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            WriteToFile();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
            WriteToFile();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _entities.AsQueryable().Where(condition);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            WriteToFile();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _entities.Remove(entity);
            }
            WriteToFile();
        }

        public void Update(T entity)
        {
            WriteToFile();
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            WriteToFile();
        }

        public Task<int> CommitAsync()
        {
            WriteToFile();
            return Task.FromResult(0);
        }

        public int Commit()
        {
            WriteToFile();
            return 0;
        }

        ValueTask<T?> IGenericRepository<T>.FindByIdAsync(params object[] pkeys)
        {
            throw new NotImplementedException();
        }

        public Task RemoveByIdAsync(params object[] pkeys)
        {
            throw new NotImplementedException();
        }
    }
}
