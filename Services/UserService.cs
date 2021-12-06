using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApiWorkControllerServer.IServices;
using WebApiWorkControllerServer.Models;
using WebApiWorkControllerServer.NoDataModels;
using WorkController.WebApi.Requests;

namespace WebApiWorkControllerServer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<Register> Register(Register user)
        {
            var check = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email);
            if (check != null) return null;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.Add(new User() {  FirstName = user.FirstName,ChiefId= user.ChiefId, Email= user.Email, LastName = user.LastName, Password = user.Password });
            return user;
        }
        public User Login(Login user)
        {
            User check = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email);
            if (check == null) return null;
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(user.Password, check.Password);
            if (!isValidPassword) return null;
            return check;
            
        }

        //public async Task<AuthenticateResponse> Register(UserModel userModel)
        //{
        //    var user = _mapper.Map<User>(userModel);

        //    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        //    var check = _userRepository.GetAll().FirstOrDefault(x => x.Login == userModel.Login);
        //    if (check != null) return null;
        //    await _userRepository.Add(user);

        //    var response = Authenticate(new AuthenticateRequest
        //    {
        //        Login = userModel.Login,
        //        Password = userModel.Password
        //    });

        //    return response;
        //}

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}
