using AutoMapper;
using GymManagementDAL.Entites;
using GymManagementDAL.Repositories.Interfaces;
using GymManagemrntBLL.Services.Interfaces;
using GymManagemrntBLL.ViewModels.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainer == null || !trainer.Any()) return [];

            var TrainerViewModel = _mapper.Map<IEnumerable<TrainerViewModel>>(trainer);
            return TrainerViewModel;
        }
        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                if (IsEmailExists(createTrainer.Email) || IsPhoneExists(createTrainer.Phone)) return false;
                var trainer = _mapper.Map<Trainer>(createTrainer);
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }
        public TrainerViewModel? GetTrainerDetails(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer == null) return null;

            return _mapper.Map<TrainerViewModel>(trainer);
        }
        public TrainerToUpdateViewModel GetTrainerToUpdate(int TrainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer == null) return null;

            return _mapper.Map<TrainerToUpdateViewModel>(trainer);
        }
        public bool UpdateTrainerDetails(int Id, TrainerToUpdateViewModel trainerToUpdate)
        {
            try
            {

                var EmailExist = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == trainerToUpdate.Email && x.Id != Id).Any();

                var PhoneExist = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == trainerToUpdate.Phone && x.Id != Id).Any();

                if (EmailExist || PhoneExist) return false;

                var trainerRepo = _unitOfWork.GetRepository<Trainer>();
                var trainer = trainerRepo.GetById(Id);
                if (trainer == null) return false;



                _mapper.Map(trainerToUpdate, trainer);

                trainerRepo.Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }
        }
        public bool RemoveTrainer(int TrainerId)
        {
            try
            {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
                if (trainer == null) return false;

                
                var hasFutureSessions = _unitOfWork.GetRepository<Session>()
                    .GetAll(s => s.TrainerId == TrainerId && s.EndDate > DateTime.Now).Any();


                if (hasFutureSessions) return false;

                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }



        #region Helper Methods

        private bool IsEmailExists(string Email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == Email).Any();
        }

        private bool IsPhoneExists(string Phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == Phone).Any();
        }

        #endregion
    }
}
