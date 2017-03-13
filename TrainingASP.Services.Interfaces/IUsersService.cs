using System.Collections.Generic;
using TrainingASP.Models;

namespace TrainingASP.Services.Interfaces
{
    public interface IUsersService
    {
        List<UserViewModel> GetUsersList();

        UserViewModel Create(UserViewModel user);

        bool Delete(int userId);
    }
}
