using AutoMapper;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using GymManagemrntBLL.Services.AttachmentService;
using GymManagemrntBLL.Services.Interfaces;
using GymManagemrntBLL.ViewModels.MemberViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentService _attachmentService;

        public MemberService(IUnitOfWork unitOfWork , IMapper mapper , IAttachmentService attachmentService)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = _unitOfWork.GetRepository<Member>().GetAll();
            if (Members == null || !Members.Any()) return [];

            //var MemberViewModel = new List<MemberViewModel>();

            #region Way01
            //foreach (var Member in Members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Id = Member.Id,
            //        Name = Member.Name,
            //        Phone = Member.Phone,
            //        Email = Member.Email,
            //        Photo = Member.Photo,
            //        Gender = Member.Gender.ToString(),
            //    };
            //    MemberViewModel.Add(memberViewModel);
            //} 
            #endregion

            #region Way02
            var MemberViewModel = _mapper.Map<IEnumerable<MemberViewModel>>(Members);

            #endregion

            return MemberViewModel;

        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {

                if (IsEmailExists(createMember.Email) || IsPhoneExists(createMember.Phone)) return false;

                var PhotoName = _attachmentService.Upload("Members", createMember.PhotoFile);
                if (string.IsNullOrEmpty(PhotoName)) return false;

                // CreateMemberViewModel - member => mapping

                var member = _mapper.Map<Member>(createMember);
                member.Photo = PhotoName;

               _unitOfWork.GetRepository<Member>().Add(member);
               var IsCreated = _unitOfWork.SaveChanges() > 0;

                if (!IsCreated)
                {
                    _attachmentService.Delete(PhotoName, "Members");
                }
                return IsCreated;
            }
            catch 
            {
                return false;
            }


        }

        public MemberViewModel? GetMemberDetails(int MemberId)
        {
           var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;

            var viewModel = _mapper.Map<MemberViewModel>(member);

            var Activemembership = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(x=>x.MemberId == MemberId && x.Status =="Active").FirstOrDefault();
            if (Activemembership is not null)
            {

                viewModel.MemberShipStartDate = Activemembership.CreatedAt.ToShortDateString();
                viewModel.MemberShipEndDate = Activemembership.EndDate.ToShortDateString();

                var Plan = _unitOfWork.GetRepository<Plan>().GetById(Activemembership.PlanId);
                viewModel.PlanName = Plan?.Name;
            }
            return viewModel;
            
        }

        public HealthRecordViewModel GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if (MemberHealthRecord == null)
                return null;
            return _mapper.Map<HealthRecordViewModel>(MemberHealthRecord);
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if(member == null)
                return null;

            return _mapper.Map<MemberToUpdateViewModel>(member);
        }

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel memberToUpdate)
        {
            try 
            {

                var emailexist = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Email == memberToUpdate.Email && x.Id != Id).Any();
                var phoneexist = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Phone == memberToUpdate.Phone && x.Id != Id).Any();

                if (emailexist ||phoneexist)  return false;

                var memberRepo= _unitOfWork.GetRepository<Member>();
                var Member = memberRepo.GetById(Id);
                if (Member == null) return false;

              _mapper.Map(memberToUpdate, Member);

                memberRepo.Update(Member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveMember(int MemberId)
        {
            try
            {
                var MemberRepo = _unitOfWork.GetRepository<Member>();
                var MemberShipRepo = _unitOfWork.GetRepository<MemberShip>();
                var member = MemberRepo.GetById(MemberId);
                if (member == null) return false;

                var SessionIds = _unitOfWork.GetRepository<MemberSession>()
                    .GetAll(x => x.MemberId == MemberId).Select(x=>x.SessionId);

                var HasActiveMemberSession = _unitOfWork.GetRepository<Session>()
                    .GetAll(x => SessionIds.Contains(x.Id) && x.StartDate > DateTime.Now).Any();

                if (HasActiveMemberSession) return false;

                var MemberShips = MemberShipRepo.GetAll(x => x.MemberId == MemberId);

                if (MemberShips.Any())
                {
                    foreach (var membership in MemberShips)
                    {
                        MemberShipRepo.Delete(membership);
                    }
                }
                MemberRepo.Delete(member);
                var IsDeleted =  _unitOfWork.SaveChanges()> 0;
                if (IsDeleted)
                    _attachmentService.Delete(member.Photo, "Members");
                return IsDeleted;
            }
            catch 
            {
                return false;
            }
        }


        #region Helper Methods

        private bool IsEmailExists(string Email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == Email).Any();
        }

        private bool IsPhoneExists(string Phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == Phone).Any();
        }

        #endregion
    }
}
