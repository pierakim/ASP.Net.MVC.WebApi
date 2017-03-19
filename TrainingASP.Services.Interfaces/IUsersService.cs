using System.Collections.Generic;
using TrainingASP.Models;

namespace TrainingASP.Services.Interfaces
{
    public interface IUsersService
    {
        List<UserViewModel> GetUsersList();

        UserViewModel GetUser(int userId);

        UserViewModel Create(UserViewModel user);

        UserViewModel Delete(int userId);

        bool Edit(int userId, UserViewModel user);
    }
}
