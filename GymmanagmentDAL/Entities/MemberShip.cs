using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Entities
{
    public class MemberShip:BaseEntity
    {
        // StartDate is CreateAt of BaseEntity
        public DateTime EndDate { get; set; }

        //0=inactive,1=active,2=expired
        //readonly property
        public string status {
            get 
            {
                if (EndDate >= DateTime.Now) 
                    return "Expired";
                
                else return "Active";
            }
                } 
        public Member Member { get; set; }=null!;
        public int MemberId { get; set; }
        public Plan Plan { get; set; }=null!;
        public int PlanId { get; set; }
    }
}
