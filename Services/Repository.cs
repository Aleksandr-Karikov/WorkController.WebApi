using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Context;
using WebApiWorkControllerServer.IServices;
using WebApiWorkControllerServer.Models.BaseModel;

namespace WebApiWorkControllerServer.Services
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly WorkControllerContext _context;

        public Repository(WorkControllerContext context)
        {
            _context = context;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var result = _context.Set<T>().FirstOrDefault(x => x.ID == id);

            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            return result;
        }

        public async Task<int> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity.ID;
        }
    }
}
