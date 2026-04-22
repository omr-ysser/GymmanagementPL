using AutoMapper;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using GymManagemrntBLL.Services.Interfaces;
using GymManagemrntBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var Sessions = _unitOfWork.sessionRepository.GetAllSessionWithTrainersAndCategories();
            if (Sessions == null || !Sessions.Any()) return [];

            #region ManualMapping
            //return Sessions.Select(x => new SessionViewModel()
            //{
            //    Id = x.Id,
            //    Capacity = x.Capacity,
            //    Description = x.Description,
            //    EndDate = x.EndDate,
            //    StartDate = x.StartDate,
            //    TrainerName = x.SessionTrainer.Name,
            //    CategoryName = x.SessionCategory.CategoryName,
            //    AvailableSlots = x.Capacity - _unitOfWork.sessionRepository.GetCountOfBookedSlots(x.Id),

            //}); 
            #endregion

            #region AutoMapping

            var MappedSession = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(Sessions);
            return MappedSession;

            #endregion


        }

        public SessionViewModel? GetSessionById(int id)
        {
           var session = _unitOfWork.sessionRepository.GetSessionByIdWithTrainersAndCategories(id);
            if (session == null) return null;

            var MappedSession = _mapper.Map<Session,SessionViewModel>(session);
            return MappedSession;

        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {

            try
            {
                if (!IsTrainerExists(createSession.TrainerId)) return false;
                if (!IsCategoryExists(createSession.CategoryId)) return false;
                if (!IsValidDateRange(createSession.StartDate, createSession.EndDate)) return false;

                var MappedSession = _mapper.Map<CreateSessionViewModel, Session>(createSession);

                _unitOfWork.sessionRepository.Add(MappedSession);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;   
            }

        }

        public UpdateSessionViewModel? GetSessionToUpdate(int SessionId)
        {
            var session = _unitOfWork.sessionRepository.GetById(SessionId);
            if(!IsSessionAvailableForUpdating(session!)) return null;
            return _mapper.Map<UpdateSessionViewModel>(session);

        }

        public bool UpdateSession(UpdateSessionViewModel updateSession, int SessionId)
        {
            try
            {
                var session = _unitOfWork.sessionRepository.GetById(SessionId);
                if (!IsSessionAvailableForUpdating(session!)) return false;
                if (!IsTrainerExists(updateSession.TrainerId)) return false;
                if (!IsValidDateRange(updateSession.StartDate, updateSession.EndDate)) return false;


                _mapper.Map(updateSession, session);
                session!.UpdatedAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }

        }

        public bool RemoveSession(int SessionId)
        {
            try
            {
                var session = _unitOfWork.sessionRepository.GetById(SessionId);
                if (!IsSessionAvailableForRemoving(session!)) return false;

                _unitOfWork.sessionRepository.Delete(session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }

        public IEnumerable<TrainerSelectViewModel> GetAllTrainerForDropDown()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerSelectViewModel>>(Trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetAllCategoriesForDropDown()
        {
            var Categories = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategorySelectViewModel>>(Categories);

        }


        #region Helpers

        private bool IsTrainerExists(int TrainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(TrainerId) != null;
        }
        private bool IsCategoryExists(int CategoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(CategoryId) != null;
        }
        private bool IsValidDateRange(DateTime StartDate , DateTime EndDate)
        {
            return StartDate < EndDate && StartDate > DateTime.Now;
        }
        private bool IsSessionAvailableForUpdating(Session session)
        {
            if (session == null) return false;
            if(session.EndDate < DateTime.Now) return false;
            if(session.StartDate <= DateTime.Now) return false;

            var HasActiveBooking = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }
        private bool IsSessionAvailableForRemoving(Session session)
        {
            if (session == null) return false;
            if(session.StartDate > DateTime.Now) return false;
            if(session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            var HasActiveBooking = _unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (HasActiveBooking) return false;

            return true;

        }

        #endregion
    }
}
