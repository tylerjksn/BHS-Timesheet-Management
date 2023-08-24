import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Student } from '../models/defs';
import { BehaviorSubject, Observable } from 'rxjs'
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  baseApiUrl: string = environment.baseApiURL;
  constructor(
    private http: HttpClient,
    ) { }

  getAllStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.baseApiUrl + '/api/students');
  }

  addStudent(addStudentReq: Student): Observable<Student> {
    var cDate = new Date();
    cDate.setHours(cDate.getHours() - 4);
    addStudentReq.id = Guid.create().toString();
    addStudentReq.cDate = cDate.toISOString();
    addStudentReq.mDate = cDate.toISOString();
    return this.http.post<Student>(this.baseApiUrl + '/api/students', 
    addStudentReq);
  }

  getStudent(id: string): Observable<Student> {
    return this.http.get<Student>(this.baseApiUrl + '/api/students/' + id);
  }

  updateStudent(id: string, updateStudentReq: Student): Observable<Student> {
    var uDate = new Date();
    console.log(uDate);
    uDate.setHours(uDate.getHours() - 4);
    updateStudentReq.mDate = uDate.toISOString();
    return this.http.put<Student>(this.baseApiUrl + '/api/students/' + id, updateStudentReq);
  }

  deleteStudent(id: string): Observable<Student> {
    return this.http.delete<Student>(this.baseApiUrl + '/api/students/' + id);
  }
}
