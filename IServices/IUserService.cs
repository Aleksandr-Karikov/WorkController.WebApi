using System.Collections.Generic;
using System.Threading.Tasks;
using WorkController.WebApi.DataBase.Models;
using WorkController.WebApi.Requests;

namespace WorkController.WebApi.IServices
{
    public interface IUserService
    {
        Login Login(Login user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Task<Register> Register(Register user);
        IEnumerable<User> GetEmployees(int ID);
        IEnumerable<Time> GetTimes(int ID);
        AddEmployee SetNewEmployee(AddEmployee emp);

        Task SetTime(TimeRequest time);
    }
}
