using Final.NetAPI.DataModels;
using Final.NetAPI.DomainModels;
using Final.NetAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final.NetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class StudentController : Controller
    {
        private readonly IstudentRepository studentRepository;
        private readonly IimageRepository imageRepository;
        private readonly object context;

        public StudentController(IstudentRepository StudentRepository, IimageRepository ImageRepository)
        {
            studentRepository = StudentRepository;
            imageRepository = ImageRepository;
        }
        [HttpGet]
        [Route("[controller]")]

        public async Task<IActionResult> GetAllStudent()
        {
            var students = await studentRepository.GetStudentsAsync();

            var domainModelStudents = new List<DataModels.Student>();

            foreach (var student in students)
            {
                var domainModelStudent = new DataModels.Student()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                    Email = student.Email,
                    Mobile = student.Mobile,
                    ProfileImgUrl = student.ProfileImgUrl,
                    gender = student.gender,
                    Address= student.Address
                };

                domainModelStudents.Add(domainModelStudent);
            }

            return Ok(domainModelStudents);
        }

        [HttpGet]
        [Route("[controller]/{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var existingStudent = await studentRepository.GetById(id);
            if (existingStudent is null)
            {
                return NotFound();
            }



            var response = new DataModels.Student
            {
                Id = existingStudent.Id,
                FirstName = existingStudent.FirstName,
                LastName = existingStudent.LastName,
                DateOfBirth = existingStudent.DateOfBirth,
                Email = existingStudent.Email,
                Mobile = existingStudent.Mobile,
                ProfileImgUrl = existingStudent.ProfileImgUrl,
                gender = existingStudent.gender,
                Address = existingStudent.Address
            };



            return Ok(response);
        }
        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudent request)
        {
            var student = new DataModels.Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                Mobile = request.Mobile,
                gender = request.gender,
                Address=request.Address

            };


            await studentRepository.AddStudent(student);

            return Ok(student);

        }
        [HttpPut]
        [Route("[controller]/{id:guid}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentRequest request)
        {
          
            var existingStudent = await studentRepository.GetById(id);
            if(existingStudent!= null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.Mobile = request.Mobile;
                existingStudent.Email = request.Email;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.gender = request.gender;
                existingStudent.Address = request.Address;

                await studentRepository.UpdateStudent(existingStudent, id);
                return Ok(existingStudent);
            }

            return NotFound();

            

        }


        [HttpDelete]
        [Route("[controller]/{id:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
         
            if ( await studentRepository.Exists(id))
            {

                var student = await studentRepository.DeleteStudent(id);
                return Ok(student);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> uploadImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            var student = await studentRepository.GetById(studentId);
            if (student !=null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);

                var fileImagePath= await imageRepository.Upload(profileImage, fileName);

                if (await studentRepository.updateProfileImage(studentId, fileImagePath))
                {
                    return Ok(fileImagePath);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
            }

            return NotFound();
        }

    }
}

