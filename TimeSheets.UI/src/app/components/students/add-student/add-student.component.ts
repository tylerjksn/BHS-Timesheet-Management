import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Student } from 'src/app/models/defs';
import { StudentsService } from 'src/app/services/students.service';

@Component({
  selector: 'app-add-student',
  templateUrl: './add-student.component.html',
  styleUrls: ['./add-student.component.css']
})
export class AddStudentComponent implements OnInit {

  addStudentReq: Student = {
    id: '',
    programId: '',
    studentNumber: '',
    firstName: '',
    middleInitial: '',
    lastName: '',
    studentEmail: '',
    mDate: Date(),
    cDate: Date(),
    mUser: '', 
    cUser: '', 
    status: '' 
  }
  
  constructor(
    private studentsService: StudentsService,
    private router: Router
    ) { }

  ngOnInit(): void {
  }

  addStudent() {
    this.studentsService.addStudent(this.addStudentReq)
    .subscribe({
      next: (student) => {
        this.router.navigate(['dashboard']);
      },
      error: (response) => {
        console.log(response);
        alert("Please fill out form completely.")
      }
    });
  }
}
