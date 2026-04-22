using GymManagementDAL.Entites;
using GymManagemrntBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionById(int id);
        bool CreateSession(CreateSessionViewModel createSession);
        UpdateSessionViewModel? GetSessionToUpdate(int SessionId); 
        bool UpdateSession(UpdateSessionViewModel updateSession,int SessionId);

        bool RemoveSession(int SessionId);
        IEnumerable<TrainerSelectViewModel>  GetAllTrainerForDropDown();
        IEnumerable<CategorySelectViewModel>  GetAllCategoriesForDropDown();
    }
}
