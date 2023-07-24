import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentsComponent } from './student/students/students.component';
import { HomeComponent } from './home/home.component';
import { ViewStudentComponent } from './student/view-student/view-student.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'students', component: StudentsComponent },
  { path: 'students/students/:id', component: ViewStudentComponent },
  { path: 'students/add', component: ViewStudentComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
