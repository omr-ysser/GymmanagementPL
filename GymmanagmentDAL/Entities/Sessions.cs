using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Entities
{
    public class Sessions : BaseEntity
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        #region RelationShips
        #region Category - Sessions (1-M)(mandatory)
        public int CategoryId { get; set; } 
        public Category SessionCategory { get; set; } = null!;
        #endregion
        #region Trainer - Sessions (1-M)(mandatory)
        public int TrainerId { get; set; }
        public Trainer SessionTrainer { get; set; } = null!;
        #endregion
        #region Sessions - Member (M-M)
        public ICollection<MemberSession> SessionMembers { get; set; } = null!;
        #endregion

        #endregion
    }
}
