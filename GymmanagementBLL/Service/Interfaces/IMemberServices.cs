using GymmanagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagementBLL.Service.Interfaces
{
    internal interface IMemberServices
    {
        IEnumerable<MemberViewModels> GetAllMembers();
    }
}
