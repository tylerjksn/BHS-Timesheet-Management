import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { QA, SelectType, Timesheet, Work } from 'src/app/models/defs';
import { TimesheetsService } from 'src/app/services/timesheets.service';

@Component({
  selector: 'app-edit-timesheet',
  templateUrl: './edit-timesheet.component.html',
  styleUrls: ['./edit-timesheet.component.css']
})
export class EditTimesheetComponent implements OnInit {

  panelOpenState = false;

  displayedColumns: string[] = ['select', 'sheetId', 'periodId', 'studentNumber', 'firstName',
  'lastName', 'companyName', 'status'];

  selectType = [
    { text: "Single", value: SelectType.single },
    { text: "Multiple", value: SelectType.multiple }
  ];

  selection = new SelectionModel<Timesheet>(true, []);
  displayType = SelectType.single;
  dataSource!: MatTableDataSource<Timesheet>;


  timesheets: Timesheet[] = [];
  works: Work[] = [];
  qAs: QA[] = []

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  timesheetDetails: Timesheet = {
    id: '',
    sheetName: '',
    periodId: '',
    studentNumber: '',
    firstName: '',
    lastName: '',
    companyName: '',
    mentorName: '',
    workList: [],
    qAList: [],
    mDate: Date(),
    cDate: Date(),
    mUser: '', 
    cUser: '', 
    status: '' 
  }
  workDetails: Work = {
    id: '',
    date: '',
    day: '',
    timeIn: '',
    timeOut: '',
    hours: 0,
    comment: '',
    mDate: '',
    cDate: '',
    mUser: '', 
    cUser: '', 
    status: '' 
  }
  qADetails: QA = {
    id: '',
    questionId: '',
    ans: '',
    mDate: '',
    cDate: '',
    mUser: '', 
    cUser: '', 
    status: '' 
  }

  

  constructor(
    private route: ActivatedRoute,
    private timesheetsService: TimesheetsService,
    private router: Router) { }

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
  
  ngOnInit(): void {
    this.getTimesheetInfo();
  }

  getTimesheetInfo() {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.timesheetsService.getTimesheet(id)
          .subscribe({
            next: (response) => {
              this.timesheetDetails = response;
              console.log(this.timesheetDetails); 
            }
          });
        }
      }
    });
  }

  updateTimesheet() {
    this.timesheetsService.updateTimesheet(this.timesheetDetails.id, this.timesheetDetails)
    .subscribe({
      next: (response) => {
        this.panelOpenState = !this.panelOpenState;
      }
    });
  }

  saveAndClose() {
    this.updateTimesheet();
    this.router.navigate(['timesheets']);
    setTimeout(()=> { //sketchy but works?
      location.reload();
    }, 100);
  }

  deleteStudent(id: string) {
    this.timesheetsService.deleteTimesheet(id)
    .subscribe({
      next: (response) => {
        this.router.navigate(['timesheets']);
      }
    });

  }
}
