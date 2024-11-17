using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Contact
{
    public class Contact
    {
        public int Id { get; set; }  // Primary key, Identity column
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string OtherDetails { get; set; }
        public int UserId { get; set; }
    }
}
