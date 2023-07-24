import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { Student } from '../models/ApiModels/student.model';
import { Student } from 'src/app/models/ApiModels/student.model';
import { UpdateStudentRequest } from '../models/ApiModels/update-student.model';
import { AddStudent } from '../models/ApiModels/add-student.model';

@Injectable({
  providedIn: 'root',
})
export class StudentserviceService {
  readonly apiUrll = 'https://localhost:44338/api/Student';
  constructor(private http: HttpClient) {}

  getStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.apiUrll + '/Student');
  }

  getStudent(studentId: string): Observable<Student> {
    return this.http.get<Student>(this.apiUrll + '/Student/' + studentId);
  }

  updateStudent(studentId: string, studentRequest: Student) {
    const UpdateStudentRequest: UpdateStudentRequest = {
      firstName: studentRequest.firstName,
      lastName: studentRequest.lastName,
      dateOfBirth: studentRequest.dateOfBirth,
      email: studentRequest.email,
      mobile: studentRequest.mobile,
      gender: studentRequest.gender,
      address: studentRequest.address,
    };

    return this.http.put<Student>(
      this.apiUrll + '/Student/' + studentId,
      UpdateStudentRequest
    );
  }

  deleteStudent(studentId: string) {
    return this.http.delete<Student>(this.apiUrll + '/Student/' + studentId);
  }

  addstudent(studentrequest: Student): Observable<Student> {
    const AddStudentRequest: AddStudent = {
      firstName: studentrequest.firstName,
      lastName: studentrequest.lastName,
      dateOfBirth: studentrequest.dateOfBirth,
      email: studentrequest.email,
      mobile: studentrequest.mobile,
      gender: studentrequest.gender,
      address: studentrequest.address,
    };

    return this.http.post<Student>(
      this.apiUrll + '/Student/Add/',
      AddStudentRequest
    );
  }

  uploadImage(studentId: string, file:File):Observable<any>{
    const formData = new FormData();
    formData.append("profileImage",file);
    return this.http.post(this.apiUrll+"/student/"+studentId+"/upload-image",formData,{
      responseType: 'blob'
    });
  }

  getImagePath(relativepath:string){
    return `${this.apiUrll}/${relativepath}`;

  }
}
