using GymManagementDAL.Entites;
using GymManagemrntBLL.Services.Interfaces;
using GymManagemrntBLL.ViewModels.AccountViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Classes
{
    
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public ApplicationUser? ValidateUser(AccountViewModel accountViewModel)
        {
            var user = _userManager.FindByEmailAsync(accountViewModel.Email).Result;
            if (user is null)
            {
                return null;
            }
            var IsPasswordValid = _userManager.CheckPasswordAsync(user, accountViewModel.Password).Result;
            return IsPasswordValid ? user : null;
        }
    }
}
