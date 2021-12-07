﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWorkControllerServer.IServices;
using WebApiWorkControllerServer.Models;
using WebApiWorkControllerServer.NoDataModels;
using WorkController.WebApi.DataBase.Models;
using WorkController.WebApi.Requests;

namespace WebApiWorkControllerServer.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<AllowsEmployee> _allowEmployeeRepository;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IConfiguration configuration,
            IRepository<AllowsEmployee> allowEmployeeRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _allowEmployeeRepository = allowEmployeeRepository;
        }
        public async Task<Register> Register(Register user)
        {
            var check = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email);
            if (check != null && !user.IsAdmin && check.ChiefId==null)
            {
                var isAllow = _allowEmployeeRepository.GetAll().FirstOrDefault(x => x.EmployeeId == check.ID
                && x.ChiefId == user.ChiefId);
                if (isAllow == null)
                {
                    user.SetErrorMessege("Вам не предоставлен доступ");
                    return user;
                }
                check.ChiefId = user.ChiefId;
                await _userRepository.Update(check);
                return user;
            }
            if (check != null)
            {
                user.SetErrorMessege("Такой пользователь уже существует");
                return user;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.Add(new User() {  FirstName = user.FirstName,ChiefId= user.ChiefId, Email= user.Email, LastName = user.LastName, Password = user.Password });
            return user;
        }
        public User Login(Login user)
        {
            User check;
            if (user.IsAdmin)
            {
                check = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email);
            }else
            check = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email && x.ChiefId!=null);
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
