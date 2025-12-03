using GymmanagementBLL.Service.Interfaces;
using GymmanagementBLL.ViewModels.MemberViewModels;
using GymmanagmentDAL.Entities;
using GymmanagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagementBLL.Service.Classes
{
    internal class MemberService : IMemberServices
    {
        //GenericRepository<Member>memberRepository=new GenericRepository<Member>();
        private readonly IGenericRepository<Member> _memberRepository;
        public MemberService(IGenericRepository<Member> memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public IEnumerable<MemberViewModels> GetAllMembers()
        {
            var Members = _memberRepository.GetAll();
            if (Members == null || !Members.Any()) return [];
            //{
            //    return Enumerable.Empty<MemberViewModels>();
            //}
            // Member - MemberViewModels => mapping
            var memberViewModels = new List<MemberViewModels>();
            //way1
            //foreach (var Member in Members)
            //{
            //    var memberViewModel = new MemberViewModels
            //    {
            //        Id = Member.Id,
            //        Name = Member.Name,
            //        Photo = Member.Phone,
            //        Email = Member.Email,
            //        Phone = Member.photo,
            //        Gender = Member.Gender.ToString()

            //    };
            //    memberViewModels.Add(memberViewModel);
            //}
            //return memberViewModels;


            var MemberViewModels = Members.Select(Member => new MemberViewModels
            {
                Id = Member.Id,
                Name = Member.Name,
                Photo = Member.photo,
                Email = Member.Email,
                Phone = Member.Phone,
            });
            return MemberViewModels;
        }
    }
}



