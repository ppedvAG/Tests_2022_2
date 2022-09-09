using Stammdatenverwaltung.Model;
using Stammdatenverwaltung.Model.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Stammdatenverwaltung.Data
{
    public class EfRepository : IRepository
    {
        EfContext _context = new EfContext();
        
        public void Add<T>(T entity) where T : Entity
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public int SaveAll()
        {
            return _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Update(entity);
        }
    }
}
