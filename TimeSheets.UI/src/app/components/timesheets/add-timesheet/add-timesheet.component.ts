import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Question, Timesheet } from 'src/app/models/defs';
import { TimesheetsService } from 'src/app/services/timesheets.service';
import { QuestionsService } from 'src/app/services/questions.service';

@Component({
  selector: 'app-add-timesheet',
  templateUrl: './add-timesheet.component.html',
  styleUrls: ['./add-timesheet.component.css']
})
export class AddTimesheetComponent implements OnInit {
  
  addTimesheetReq: any = new FormGroup({
    id: new FormControl(''),
    sheetName: new FormControl(''),
    periodId: new FormControl(''),
    studentNumber: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    companyName: new FormControl(''),
    mentorName: new FormControl(''),
    workList: new FormControl([]),
    qAList: new FormControl([]),
    mDate: new FormControl(Date()),
    cDate: new FormControl(Date()),
    mUser: new FormControl(''), 
    cUser: new FormControl(''), 
    status: new FormControl('') 
  });

  constructor(
    private timesheetsService: TimesheetsService,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }
  
  addTimesheet() {
    this.timesheetsService.addTimesheet(this.addTimesheetReq.value)
    .subscribe({
      next: (timesheet) => {
        this.router.navigate(['timesheets']);
      }
    });
  }
}
