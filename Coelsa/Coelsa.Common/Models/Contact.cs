using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coelsa.Common.Models
{
    public class Contact
    {
        public int IdContact { get; set; }
        
        [Required, StringLength(maximumLength: 50)]
        public string FirstName { get; set; }
        [Required, StringLength(maximumLength: 50)]
        public string LastName { get; set; }
        [Required, StringLength(maximumLength: 50)]
        public string Company { get; set; }
        [Required, StringLength(maximumLength: 50),EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(maximumLength: 50),]
        public string PhoneNumber { get; set; }
    }
}
