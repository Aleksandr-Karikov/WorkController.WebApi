using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkController.WebApi.DataBase.Models;
using WorkController.WebApi.IServices;
using WorkController.WebApi.Requests;

namespace WorkController.WebApi.Services
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
            if (check != null && !user.IsAdmin && check.ChiefId == null)
            {
                var isAllow = _allowEmployeeRepository.GetAll().FirstOrDefault(x=>x.ChiefId == user.ChiefId);
                if (isAllow == null)
                {
                    user.Error = "Вам не предоставлен доступ";
                    return user;
                }
                check.ChiefId = user.ChiefId;
                isAllow.EmployeeId = check.ID;
                await _allowEmployeeRepository.Update(isAllow);
                await _userRepository.Update(check);
                return user;
            }
            if (check != null)
            {
                user.Error="Такой пользователь уже существует";
                return user;
            }
            if (!user.IsAdmin && user.ChiefId!=null)
            {
                var emp = _allowEmployeeRepository.GetAll().FirstOrDefault(x => x.EmployeeEmail == user.Email && user.ChiefId==x.ChiefId);
                if (emp == null)
                {
                    user.Error = "Вам не предоставлен доступ";
                    return user;
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                await _userRepository.Add(new User() { FirstName = user.FirstName, ChiefId = user.ChiefId, Email = user.Email, LastName = user.LastName, Password = user.Password });
                emp.EmployeeId = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email).ID;
                await _allowEmployeeRepository.Update(emp);
                return user;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.Add(new User() { FirstName = user.FirstName, ChiefId = user.ChiefId, Email = user.Email, LastName = user.LastName, Password = user.Password });
            return user;
        }
        public Login Login(Login user)
        {
            User check;
            if (user.IsAdmin)
            {
                check = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email);
            }
            else
                check = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email && x.ChiefId != null);
            if (check == null) return new Login() {Error="Возможно у пользователя нет начальника\n Зарегистрируйтесь" };
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(user.Password, check.Password);
            if (!isValidPassword) return new Login() { Error = "Неверный пароль" };
            return new Login() { ChiefId = check.ChiefId, Email = check.Email, FirstName = check.FirstName, LastName = check.LastName, ID = check.ID};

        }

   
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetEmployees(int ID)
        {
            var emps =  _allowEmployeeRepository.GetAll().Where(x => x.ChiefId == ID);
            if (emps == null) return null;
            List<User> users = new List<User>();
            foreach(var emp in emps)
            {
                var user = _userRepository.GetAll().FirstOrDefault(x => x.ID == emp.EmployeeId);
                if (user != null)
                {
                    user.ChiefId = null;
                    user.Chief = null;
                    user.Password = null;
                    users.Add(user);
                }
                else 
                { 
                    users.Add(new User() { Email = emp.EmployeeEmail }); 
                }

                
            }
            return users;
        }

        public AddEmployee SetNewEmployee(AddEmployee emp)
        {
            var newE = new AllowsEmployee() { EmployeeEmail = emp.Email, ChiefId = emp.ChiefId };
            _allowEmployeeRepository.Add(newE);
            return emp;
        }
    }
}
