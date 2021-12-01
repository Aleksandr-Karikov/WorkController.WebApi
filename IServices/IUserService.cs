using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Models;
using WebApiWorkControllerServer.NoDataModels;

namespace WebApiWorkControllerServer.IServices
{
    public interface IUserService
    {
        User Login(Login user);
        //Task<AuthenticateResponse> Register(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        Task<User> Register(User user);
    }
}
