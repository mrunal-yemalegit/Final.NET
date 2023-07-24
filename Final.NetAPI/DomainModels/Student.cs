using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.NetAPI.DomainModels
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public string ProfileImgUrl { get; set; }
        public string gender { get; set; }

        public string Address { get; set; }


    }
}
