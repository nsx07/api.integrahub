using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Infra.CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Service.Services
{
    public class UserService(IUserRepository userRepository) : BaseService<User, long>(userRepository), IUserService
    {

        public User? GetUserByLogin(string login)
        {
            IQueryable<User> query = _baseRepository.Query();
            return query.Where(x => x.Email == login || x.Phone == login).FirstOrDefault();
        }

        public bool CheckPassword(string passwordAttempt, User user)
        {
            string attemptMD5 = Encryptor.GetMd5Hash(passwordAttempt);

            return attemptMD5.Equals(user.Password);
        }

    }
}
