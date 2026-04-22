using AutoMapper.Execution;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using GymManagemrntBLL.Services.Interfaces;
using GymManagemrntBLL.ViewModels.AnalyticsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Member = GymManagementDAL.Entites.Member;

namespace GymManagemrntBLL.Services.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel? GetAnalyticsData()
        {
            var Sessions = _unitOfWork.sessionRepository.GetAll();
            return new AnalyticsViewModel()
            {
                ActiveMembers = _unitOfWork.GetRepository<MemberShip>().GetAll(x => x.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Sessions.Count(x => x.StartDate > DateTime.Now),
                OngoingSessions = Sessions.Count(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now),
                CompletedSessions = Sessions.Count(x=>x.EndDate < DateTime.Now),
            };
        }
    }
}
