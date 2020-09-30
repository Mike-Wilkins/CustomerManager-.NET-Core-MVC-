using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManager.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression(@"^\s*(([+]\s?\d[-\s]?\d|0)?\s?\d([-\s]?\d){9}|[(]\s?\d([-\s]?\d)+\s*[)]([-\s]?\d)+)\s*$", ErrorMessage ="Phone number not recognised")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage ="Email not recognised")]
        public string Email { get; set; }

        [DisplayName("Date Updated")]
        public string DateCreated { get; set; }
    }
}
