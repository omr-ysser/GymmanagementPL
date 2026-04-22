using GymManagemrntBLL.ViewModels.MemberViewModel;
using GymManagemrntBLL.ViewModels.TrainerViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerViewModel? GetTrainerDetails(int TrainerId);
        TrainerToUpdateViewModel GetTrainerToUpdate(int TrainerId);
        bool UpdateTrainerDetails(int Id, TrainerToUpdateViewModel trainerToUpdate);
        bool RemoveTrainer(int TrainerId);


    }
}
