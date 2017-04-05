using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstClassAPI.Models
{
    public class ScoutRepository : IScoutRepository
    {
        private readonly ScoutContext _context;

        public ScoutRepository(ScoutContext context)
        {
            _context = context;
        }

        public IEnumerable<Scout> GetAll()
        {
            return _context.Scouts.ToList();
        }

        public void Add(Scout scout)
        {
            _context.Scouts.Add(scout);
            _context.SaveChanges();
        }

        public Scout Find(long key)
        {
            return _context.Scouts.FirstOrDefault(t => t.Key == key);
        }

        public void Remove(long key)
        {
            var entity = _context.Scouts.First(t => t.Key == key);
            _context.Scouts.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Scout scout)
        {
            _context.Scouts.Update(scout);
            _context.SaveChanges();
        }
    }
}