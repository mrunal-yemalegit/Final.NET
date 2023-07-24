using Final.NetAPI.DataModels;
using Final.NetAPI.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Final.NetAPI.Repository
{
    public class SqlStudentRepository : IstudentRepository
    {
        private readonly StudentAdminContext context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }
        public async Task<List<DataModels.Student>> GetStudentsAsync()
        {
            return await context.Student.ToListAsync();
        }
        public async Task<DataModels.Student> GetById(Guid id)
        {
            return await context.Student.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> Exists(Guid studentId)
        {
            return await context.Student.AnyAsync(x => x.Id == studentId);
            
            
        }

        public async Task<DataModels.Student> UpdateStudent(DataModels.Student request,Guid studentId)
        {
            
            var student = await GetById(studentId);
            if (student != null)
            {
                context.Update(student);
                await context.SaveChangesAsync();
                return student;
            }
            return null;
            
        }

        public async Task<DataModels.Student> AddStudent(DataModels.Student request)
        {
            
            var student=await context.Student.AddAsync(request);

            await context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<DataModels.Student> DeleteStudent(Guid StudentId)
        {
            DataModels.Student student = await context.Student.FirstOrDefaultAsync(each => each.Id == StudentId);


            if (student != null)
            {
                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return student ;

            }

            return null;
        }

        public async Task<DataModels.Student> GetStudentsAsync(Guid id)
        {
            var student = await GetStudentsAsync(id);
            if (student !=null)
            {
                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return student;

            }
            return null;
        }

        public async Task<bool> updateProfileImage(Guid StudentId, string profileImageUrl)
        {
            var student = await GetById(StudentId);
            if(student!= null)
            {
                student.ProfileImgUrl = profileImageUrl;
                await context.SaveChangesAsync();
                return true;
            }
     
            return false;
        }
    }

    
}
