using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using AutoMapper;
using TrainingASP.Data;
using TrainingASP.Services.Interfaces;
using TrainingASP.Models;

namespace TrainingASP.Services
{
    public class UsersService : IUsersService
    {

        private readonly IMapper _mapper;
        public UsersService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public UserViewModel GetUser(int userId)
        {
            try
            {
                using (var context = new TrainingDbEntities())
                {
                    var userRes = context.UsersTbls.Find(userId);
                    if (userRes == null)
                        return null;

                    var user = _mapper.Map<UserViewModel>(userRes);
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/GetUser:", ex);
            }
        }

        public List<UserViewModel> GetUsersList()
        {
            try
            {
                List<UsersTbl> list;
                using (var context = new TrainingDbEntities())
                {
                    list = context.UsersTbls.ToList();
                }
                if (list.Any())
                {
                    var usersList = _mapper.Map<IEnumerable<UserViewModel>>(list).ToList();

                    return usersList;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/GetUsersList:", ex);
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
                    //apply new id
                    user.UserId = newId;

                    var newUUser = _mapper.Map<UsersTbl>(user);
                    context.UsersTbls.Add(newUUser);
                    context.SaveChanges();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error in UserService/Create:", ex);
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

                    var user = _mapper.Map<UserViewModel>(userToDelete);
                    return user;
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
                    var userTb = _mapper.Map<UsersTbl>(user);

                    context.Entry(userTb).State = EntityState.Modified;
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Error in UserService/Edit - SaveChanges Operation:", ex);
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
