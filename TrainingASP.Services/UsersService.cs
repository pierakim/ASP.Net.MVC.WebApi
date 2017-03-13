using System;
using System.Collections.Generic;
using System.Linq;
using TrainingASP.Data;
using TrainingASP.Services.Interfaces;
using TrainingASP.Models;

namespace TrainingASP.Services
{
    public class UsersService : IUsersService
    {
        public List<UserViewModel> GetUsersList()
        {
            try
            {
                List<UserViewModel> usersList;

                using (var context = new TrainingDbEntities())
                {
                    usersList = (from u in context.UsersTbls
                        select new UserViewModel
                        {
                            UserId = u.UserId,
                            UserName = u.UserName
                        }).ToList();
                }
                return usersList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/GetUsersList():", ex);
            }
        }

        public UserViewModel Create(UserViewModel user)
        {
            try
            {
                using (var context = new TrainingDbEntities())
                {
                    var p = context.UsersTbls.OrderByDescending(c => c.UserId).FirstOrDefault();
                    var newId = (p?.UserId ?? 0) + 1;

                    var newUser = new UsersTbl
                    {
                        UserId = newId,
                        UserName = user.UserName
                    };

                    context.UsersTbls.Add(newUser);
                    context.SaveChanges();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/PostNewUser:", ex);
            }


        }

        public bool Delete(int userId)
        {
            try
            {
                using (var context = new TrainingDbEntities())
                {
                    var userToDelete = context.UsersTbls.Find(userId);
                    context.UsersTbls.Remove(userToDelete);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/PostNewUser:", ex);
            }
            
        }
    }
}
