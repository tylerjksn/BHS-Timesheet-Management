import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentsService } from 'src/app/services/students.service';
import { TimesheetsService } from 'src/app/services/timesheets.service';
import { SelectType, Student, Timesheet } from 'src/app/models/defs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-edit-student',
  templateUrl: './edit-student.component.html',
  styleUrls: ['./edit-student.component.css']
})
export class EditStudentComponent implements OnInit {

  panelOpenState = false;

  displayedColumns: string[] = ['select','sheetId', 'periodId', 'studentNumber', 'firstName', 'lastName', 'companyName', 'mentorName'];

  selectType = [
    { text: "Single", value: SelectType.single },
    { text: "Multiple", value: SelectType.multiple }
  ];

  selection = new SelectionModel<Timesheet>(true, []);
  displayType = SelectType.single;
  dataSource!: MatTableDataSource<Timesheet>;

  timesheets: Timesheet[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  studentDetails: Student = {
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
    private route: ActivatedRoute,
    private studentsService: StudentsService,
    private timesheetsService: TimesheetsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getStudentInfo();
  }

  paginatePlusSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  selectHandler(row: Timesheet) {
    if (this.displayType == SelectType.single) {
      if (!this.selection.isSelected(row)) {
        this.selection.clear();
      }
    }
    this.selection.toggle(row);
  }

  onChange(typeValue: number) {
    this.displayType = typeValue;
    this.selection.clear();
  }

  getStudentInfo() {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.studentsService.getStudent(id)
          .subscribe({
            next: (response) => {
              this.studentDetails = response;
              this.getTimesheetsInfo();
            }
          });
        }
      }
    });
  }

  getTimesheetsInfo() {
    var sid = this.studentDetails.studentNumber;
        if (sid) {
          this.timesheetsService.getStudentTimesheetsBySID(sid)
          .subscribe({
            next: (response) => {
              this.timesheets = response;
              this.dataSource = new MatTableDataSource(this.timesheets);
              this.paginatePlusSort();
            }
          });
        }
  }

  updateStudent() {
    this.studentsService.updateStudent(this.studentDetails.id, this.studentDetails)
    .subscribe({
      next: (response) => {
        this.panelOpenState = !this.panelOpenState;
      }
    });
  }

  saveAndClose() {
    this.updateStudent();
    this.router.navigate(['dashboard']);
    setTimeout(()=> { //sketchy but works?
      location.reload();
    }, 100);
  }

  deleteStudent(id: string) {
    this.studentsService.deleteStudent(id)
    .subscribe({
      next: (response) => {
        this.router.navigate(['dashboard']);
      }
    });

  }
}
