using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiWorkControllerServer.DataBase.Models.NoDataModels;
using WebApiWorkControllerServer.Models;

namespace WebApiWorkControllerServer.IServices
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(UserModel userModel);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
