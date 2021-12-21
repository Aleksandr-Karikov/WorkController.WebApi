using Microsoft.Extensions.Configuration;
using System;
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
        private readonly IRepository<Time> _timeRepository;
        private readonly IRepository<ScreenShots> _screeenRepository;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IConfiguration configuration,
            IRepository<AllowsEmployee> allowEmployeeRepository, IRepository<Time> timeRepository, IRepository<ScreenShots> screeenRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _allowEmployeeRepository = allowEmployeeRepository;
            _timeRepository = timeRepository;
            _screeenRepository = screeenRepository;
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
                await _userRepository.Add(new User() { FirstName = user.FirstName, ChiefId = user.ChiefId, Email = user.Email, LastName = user.LastName, Password = user.Password, ScreenShotPeriod = 10 });
                emp.EmployeeId = _userRepository.GetAll().FirstOrDefault(x => x.Email == user.Email).ID;
                await _allowEmployeeRepository.Update(emp);
                return user;
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.Add(new User() { FirstName = user.FirstName, ChiefId = user.ChiefId, Email = user.Email, LastName = user.LastName, Password = user.Password, ScreenShotPeriod = 10 });
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
            return new Login() { ChiefId = check.ChiefId, Email = check.Email, FirstName = check.FirstName, LastName = check.LastName, ID = check.ID,ScreenShotPeriod= check.ScreenShotPeriod};

        }

   
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public IEnumerable<Time> GetTimes(int ID)
        {
            var times = _timeRepository.GetAll().Where(x => x.UserId == ID);
            if (times == null) return null;
            return times;
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

        public async Task<TimeRequest> SetTime(TimeRequest time)
        {
            var record =  _timeRepository.GetAll().FirstOrDefault(x => x.UserId == time.UserId && x.DateTime.Date==time.DateTime.Date);
            int id = 0;
            if (record == null)
            {
                id = await _timeRepository.Add(new Time() { DateTime = time.DateTime.Date, UserId = time.UserId, Milleseconds = time.Milliseconds });
            }
            else
            {
                record.Milleseconds += time.Milliseconds;
                id = await _timeRepository.Update(record);
            }
            foreach(var image in time.Screens)
            {
                await _screeenRepository.Add(new ScreenShots()
                {
                    TimeId = id,
                    Screen = image
                }) ;
            }
            
            return time;
        }

        public async Task<EmployeeMoney> SetMoney(EmployeeMoney request)
        {
            if (!request.IsAdmin)
            {
                request.Error = "Вы не имеете прав";
                return request;
            }
            var record = _userRepository.GetAll().FirstOrDefault(x=>x.ID == request.Id);
            record.MoneyPerHour = request.Money;
            await _userRepository.Update(record);
            return request;
        }

        public IEnumerable<byte[]> GetScreens(GetScreensRequest request)
        {
            var ids = _timeRepository.GetAll().Where(x => x.DateTime.Date == request.Date && x.UserId== request.UserId);
            if (ids == null) return null;
            var rezult = new List<byte[]>();
            foreach(var id in ids)
            {
                var images = _screeenRepository.GetAll().Where(x => x.TimeId == id.ID);
                if (images == null) continue;
                foreach(var image in images)
                {
                     rezult.Add(image.Screen);
                }
            }
            return rezult;
        }

        public async Task<SetPeriodRequest> SetPeriod(SetPeriodRequest request)
        {
            var user = _userRepository.GetById(request.UserId);
            if (user == null)
            {
                request.Error = "Пользователя не существует";
                return request; 
            }
            user.ScreenShotPeriod = request.Time;
            await _userRepository.Update(user);
            return request;
        }
    }
}
