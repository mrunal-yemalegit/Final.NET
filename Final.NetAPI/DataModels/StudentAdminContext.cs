using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.NetAPI.DataModels
{
    public class StudentAdminContext :DbContext

    {
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options)
        {

        }

        public DbSet<Student> Student { get; set; }
       
    }
}
