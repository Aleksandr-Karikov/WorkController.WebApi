using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Models.BaseModel;

namespace WebApiWorkControllerServer.IServices
{
    public interface IRepository<T> where T : Base
    {
        List<T> GetAll();
        T GetById(int id);
        Task<int> Add(T entity);
    }
}
