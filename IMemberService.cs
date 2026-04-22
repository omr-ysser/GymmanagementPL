using GymManagemrntBLL.ViewModels.MemberViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Interfaces
{
    public interface IMemberService 
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);

        MemberViewModel? GetMemberDetails(int MemberId);

        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);

        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        bool UpdateMemberDetails(int Id, MemberToUpdateViewModel memberToUpdate);

        bool RemoveMember(int MemberId); 


    }
}
