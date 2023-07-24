import { Component, OnInit } from '@angular/core';
import { StudentserviceService } from '../studentservice.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { Student } from 'src/app/models/ApiModels/student.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css'],
})
export class StudentsComponent implements OnInit {
  students: Student[] = [];
  filteredStudents: Student[] = [];
  searchQuery = '';
  constructor(
    private service: StudentserviceService,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    //fetch student
    this.service.getStudents().subscribe((data) => {
      this.students = data;
      this.filteredStudents = data;
      console.log(data);
    });
  }

  searchStudents(): void {
    this.filteredStudents = this.students.filter((student) =>
      student.firstName.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }
}
