using GymManagementDAL.Entites;
using GymManagemrntBLL.ViewModels.AccountViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser(AccountViewModel accountViewModel);
    }
}
