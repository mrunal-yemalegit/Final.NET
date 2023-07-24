using Final.NetAPI.DataModels;
using Final.NetAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.NetAPI.Repository
{
    public interface IstudentRepository
    {
        Task<List<DataModels.Student>> GetStudentsAsync();

        Task<bool> Exists(Guid studentId);
        Task<DataModels.Student> GetById(Guid id);

        Task<DataModels.Student> UpdateStudent(DataModels.Student request, Guid StudentId);

        Task<DataModels.Student> AddStudent(DataModels.Student student);

        Task<DataModels.Student> DeleteStudent(Guid StudentId);

        Task<DataModels.Student> GetStudentsAsync(Guid id);

        Task<bool> updateProfileImage(Guid StudentId, string profileImageUrl);
    }
}
