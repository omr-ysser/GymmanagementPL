using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Entities
{
    public class Member:GymUser
    {
        //joinDate=CreateAt of BaseEntity 
        public string?photo { get; set; }
        #region RelationShips
        #region Member - HealthRecord (1-1)(mandatory)
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion
        #region Member - MemberShip
        public ICollection<MemberShip> MemberShips { get; set; } = null!;
        #endregion
        #region Member - Sessions (M-M)
        public ICollection<MemberSession> MemberSessions { get; set; } = null!;
        #endregion

        #endregion
    }
}
