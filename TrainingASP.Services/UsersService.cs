using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrainingASP.Data;
using TrainingASP.Services.Interfaces;
using TrainingASP.Models;

namespace TrainingASP.Services
{
    public class UsersService : IUsersService
    {
        public UserViewModel GetUser(int userId)
        {
            try
            {
                using (var context = new TrainingDbEntities())
                {
                    var user = context.UsersTbls.Find(userId);
                    if (user == null)
                        return null;
                    var u = new UserViewModel
                    {
                        UserId = user.UserId,
                        UserName = user.UserName
                    };
                    return u;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

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
                var newUser = new UsersTbl();
                using (var context = new TrainingDbEntities())
                {
                    var p = context.UsersTbls.OrderByDescending(c => c.UserId).FirstOrDefault();
                    var newId = (p?.UserId ?? 0) + 1;

                    user.UserId = newId;

                    newUser.UserId = newId;
                    newUser.UserName = user.UserName;

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

        public UserViewModel Delete(int userId)
        {
            try
            {
                using (var context = new TrainingDbEntities())
                {
                    var userToDelete = context.UsersTbls.Find(userId);
                    if (userToDelete == null)
                        return null;
                    context.UsersTbls.Remove(userToDelete);
                    context.SaveChanges();

                    var u = new UserViewModel
                    {
                        UserId = userToDelete.UserId,
                        UserName = userToDelete.UserName
                    };
                    return u;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/Delete:", ex);
            }

        }

        public bool Edit(int userId, UserViewModel user)
        {
            try
            {
                if (userId != user.UserId)
                {
                    return false;
                }
                using (var context = new TrainingDbEntities())
                {
                    var userTb = new UsersTbl
                    {
                        UserId = user.UserId,
                        UserName = user.UserName
                    };

                    context.Entry(userTb).State = EntityState.Modified;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/Edit:", ex);
            }

        }
    }
}
