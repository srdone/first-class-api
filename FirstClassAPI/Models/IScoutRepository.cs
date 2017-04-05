using System.Collections.Generic;

namespace FirstClassAPI.Models
{
    public interface IScoutRepository
    {
        void Add(Scout scout);
        IEnumerable<Scout> GetAll();
        Scout Find(long key);
        void Remove(long key);
        void Update(Scout scout);
    }
}