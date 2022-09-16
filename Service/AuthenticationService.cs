﻿using WebAPI.DAO;
using WebAPI.Entity;

namespace WebAPI.Service
{
    public class AuthenticationService
    {
        public static SysUser Login(string Email, string Password)
        {
            UserDAO UserDAO = new UserDAO();
            var UserList = UserDAO.QueryUserByEmail(Email);
            if (UserList.Count() == 0)
            {
                throw new Exception("NO member found");
            }
            SysUser User = UserList[0];

            if (User.PasswordHash 
                == SecurityService.GetMD5Hash(User.Salt + Password))
            {
                return User;
            }
            else
            {
                throw new Exception("Wrong Password");
            }
        }

        public static int AuthorizationLevel(ISession session, PublicInfoModel UserInfo)
        {
            try
            {
                int Permission = (int)session.GetInt32("Permission");
                return Math.Min(Permission, UserInfo.Permission);
            }
            catch
            {
                return 0;
            }
        }
    }
}
