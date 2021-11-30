using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWorkControllerServer.Common;
using WebApiWorkControllerServer.Context;
using WebApiWorkControllerServer.DataBase.Models.NoDataModels;
using WebApiWorkControllerServer.IServices;
using WebApiWorkControllerServer.Models;

namespace WebApiWorkControllerServer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            
            var check = _userRepository.GetAll().FirstOrDefault(x => x.Login == model.Login);
            if (check == null) return null;
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(model.Password, check.Password);

            if (!isValidPassword) return null;

            if (check == null)
            {
                // todo: need to add logger
                return null;
            }

            //var token = _configuration.GenerateJwtToken(user);

            // return new AuthenticateResponse(user, token);
            return new AuthenticateResponse(check, "test");
        }

        public async Task<AuthenticateResponse> Register(UserModel userModel)
        {
            var user = _mapper.Map<User>(userModel);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var check = _userRepository.GetAll().FirstOrDefault(x => x.Login == userModel.Login);
            if (check != null) return null;
            await _userRepository.Add(user); 

            var response = Authenticate(new AuthenticateRequest
            {
                Login = userModel.Login,
                Password = userModel.Password
            });

            return response;
        }

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
