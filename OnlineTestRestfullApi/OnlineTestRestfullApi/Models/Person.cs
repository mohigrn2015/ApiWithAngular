using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTestRestfullApi.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        [DisplayName("First Name")]
        [MaxLength(220)]
        public string FName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [MaxLength(220)]
        public string LName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime Dob { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        [MaxLength(220)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Contact No")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11)]
        public string Phone { get; set; }

        [Required]
        [DisplayName("Address")]
        [MaxLength(220)]
        public string Address { get; set; }
    }
} 
