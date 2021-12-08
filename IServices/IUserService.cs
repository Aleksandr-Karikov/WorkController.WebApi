using System.Collections.Generic;
using System.Threading.Tasks;
using WorkController.WebApi.DataBase.Models;
using WorkController.WebApi.Requests;

namespace WorkController.WebApi.IServices
{
    public interface IUserService
    {
        User Login(Login user);
        //Task<AuthenticateResponse> Register(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Task<Register> Register(Register user);

    }
}
