using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Enums;
using IntegraHub.Domain.Interfaces;
using IntegraHub.Infra.CrossCutting.Utils;
using Microsoft.EntityFrameworkCore;
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
            IQueryable<User> query = _baseRepository
                .Query()
                .Include(x => x.Company)
                .Include(x => x.UserType);
            UserTypeEnum[] userTypes = [UserTypeEnum.Admin, UserTypeEnum.Master];
            return query.Where(x => x.Email == login || x.Phone == login && userTypes.Contains((UserTypeEnum) x.UserTypeId)).FirstOrDefault();
        }

        public bool CheckPassword(string passwordAttempt, User user)
        {
            string attemptMD5 = Encryptor.GetMd5Hash(passwordAttempt);

            return attemptMD5.Equals(user.Password);
        }

    }
}
