using AutoMapper;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using GymManagemrntBLL.Services.Interfaces;
using GymManagemrntBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll ();
            if (Plans == null || !Plans.Any()) return [];

            var PlanViewModel = _mapper.Map<IEnumerable<PlanViewModel>>(Plans);
            return PlanViewModel;

        }

        public PlanViewModel? GetPlanDetails(int PlanId)
        {
           var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan == null) return null;

            return _mapper.Map<PlanViewModel>(Plan);
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan == null || plan.IsActive == false || HasActiveMemberShips(PlanId)) return null;

            return _mapper.Map<UpdatePlanViewModel>(plan);
        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel UpdatedPlan)
        {
            try
            {

                var planRepo = _unitOfWork.GetRepository<Plan>();
                var plan = planRepo.GetById(PlanId);
                if (plan == null || HasActiveMemberShips(PlanId)) return false;


                _mapper.Map(UpdatedPlan,plan);

                planRepo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch 
            {
                return false;
            }
        }

        public bool ToggleStatus(int PlanId)
        {
            var planRepo = _unitOfWork.GetRepository<Plan>();
            var plan = planRepo.GetById(PlanId);
            if (plan == null || HasActiveMemberShips(PlanId)) return false;

            plan.IsActive = plan.IsActive == true ? false : true;

            try
            {
                planRepo.Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }


        #region Helper Methods

        private bool HasActiveMemberShips(int PlanId)
        {
            return _unitOfWork.GetRepository<MemberShip>()
                .GetAll(x => x.PlanId == PlanId && x.Status == "Active").Any();
        }

        #endregion
    }
}
