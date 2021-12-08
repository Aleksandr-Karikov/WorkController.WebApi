using System.Collections.Generic;
using System.Threading.Tasks;
using WorkController.WebApi.DataBase.Models.BaseModel;

namespace WorkController.WebApi.IServices
{
    public interface IRepository<T> where T : Base
    {
        List<T> GetAll();
        T GetById(int id);
        Task<int> Add(T entity);
        Task<int> Update(T entity);
    }
}
