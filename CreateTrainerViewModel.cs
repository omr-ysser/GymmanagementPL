using GymManagementDAL.Entites.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagemrntBLL.ViewModels.TrainerViewModel
{
    public class CreateTrainerViewModel
    {

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must Be Between 2 and 50 Char")]
        [RegularExpression(@"^[a-zA-Z\S]+$", ErrorMessage = "Name Can Contain Only Letters And Spaces")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = "Email Is Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must Be Between 5 and 100 Char")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone Is Required")]
        [Phone(ErrorMessage = "Invalid Phone Format")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;


        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; } 


        [Required(ErrorMessage = "Gender Is Required")]
        public Gender Gender { get; set; } 


        [Required(ErrorMessage = "Building Number Is Required")]
        [Range(1, 1000, ErrorMessage = "Building Number Must be Between 1 and 1000")]
        public int BuildingNumber { get; set; }


        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street Must Be Between 2 and 30 Char")]
        public string Street { get; set; } = null!;


        [Required(ErrorMessage = "City Is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City Must Be Between 2 and 30 Char")]
        [RegularExpression(@"^[a-zA-Z\S]+$", ErrorMessage = "City Can Contain Only Letters And Spaces")]
        public string City { get; set; } = null!;


        [Required(ErrorMessage = "Specialties Is Required")]
        public Specialties Specialties { get; set; } 
    }
}
