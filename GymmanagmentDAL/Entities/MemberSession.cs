using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymmanagmentDAL.Entities
{
    public class MemberSession:BaseEntity
    {
        //BookingDate is CreateAt of BaseEntity
        public bool IsAttended { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int SessionId { get; set; }
        public Sessions Session { get; set; } = null!;
    }
}
