import { Component } from '@angular/core';
import { StudentserviceService } from '../studentservice.service';
import { ActivatedRoute } from '@angular/router';
import { Student } from 'src/app/models/ApiModels/student.model';
import { FormGroup } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { subscribeOn } from 'rxjs';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css'],
})
export class ViewStudentComponent {
  student: Student = {
    id: '',
    firstName: '',
    lastName: '',
    dateOfBirth:'',
    email: '',
    mobile: 0,
    gender: '',
    profileImgUrl: '',
    address: '',
  };

  studentId: string | undefined | any;
  studentForm!: FormGroup;
  data: any = [];
  isNewStudent = false;
  header = '';
  displayProfileImageUrl = "";
  imageUpload: any;
  mydate:any;

  constructor(
    private studentService: StudentserviceService,
    private readonly route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private router: Router,
    private datepipe: DatePipe
  ) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.studentId = params.get('id');

      if (this.studentId) {
        if (this.studentId.toLowerCase() === 'Add'.toLowerCase()) {
          this.isNewStudent = true;
          this.header = 'Add New Student';
          this.setImage();
        } else {
          this.header = 'Edit Student';
        }
      }
      console.log(this.studentService.getStudent(this.studentId));
      this.studentService.getStudent(this.studentId).subscribe({
        next: (response: Student) => {
          this.student = response;
          console.log(this.student);
          this.mydate = this.datepipe.transform(this.student.dateOfBirth, 'yyyy-MM-dd');
        },
      });
    });
  }

  onUpdate(): void {
    this.studentService.updateStudent(this.student.id, this.student).subscribe(
      (response) => {
        this.snackBar.open('student updated successfully', undefined, {
          duration: 2000,
        });

        setTimeout(() => {
          this.router.navigateByUrl('students');
        }, 2000);
      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
  }

  onDelete(): void {
    this.studentService.deleteStudent(this.student.id).subscribe(
      (successResponse) => {
        this.snackBar.open('student deleted successfully', undefined, {
          duration: 2000,
        });

        setTimeout(() => {
          this.router.navigateByUrl('students');
        }, 2000);
      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    );
  }

  onAdd(): void {
    this.studentService.addstudent(this.student).subscribe({
      next: (successResponse: Student) => {
        this.snackBar.open('student Added successfully', undefined, {
          duration: 2000,
        });
        setTimeout(() => {
          this.router.navigateByUrl('students');
        }, 2000);
      },
    });
  }

  // formatDate(date: Date): Date {
  //   const formattedDate = date.toISOString().substring(0, 10);
  //   return this.datePipe.transform(formattedDate, 'yyyy-MM-dd');
  // }

  private setImage(): void {
    if (this.student.profileImgUrl) {
     this.displayProfileImageUrl = this.studentService.getImagePath(this.student.profileImgUrl);
     //this.displayProfileImageUrl="./assets/Images/student.jpg";
    } 
    else {
      this.displayProfileImageUrl = "./assets/Images/student.jpg";
    }
  }

  uploadImage(event:any):void{
    if(this.studentId){
      console.log("start");
      const file:File=event.target.files[0];
      this.studentService.uploadImage(this.studentId,file).subscribe(
        (successResponse)=>{
          
          this.student.profileImgUrl=successResponse;

          this.setImage();
          console.log("working");

          this.snackBar.open("profile image updated successfully",undefined ,{
            duration: 2000
          });
        },
        (errorResponse)=>{
          console.log(errorResponse);
        }
      );
    }
  }
}
