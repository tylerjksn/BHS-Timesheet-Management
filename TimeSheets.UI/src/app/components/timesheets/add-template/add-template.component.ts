import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Question, SelectType, SheetTemplate, Template_Question } from 'src/app/models/defs';
import { TimesheetsService } from 'src/app/services/timesheets.service';

@Component({
  selector: 'app-add-template',
  templateUrl: './add-template.component.html',
  styleUrls: ['./add-template.component.css']
})
export class AddTemplateComponent implements OnInit {
  
  displayedColumns: string[] = ['select', 'sheetName', 'countOfDays'];

  selectType = [
    { text: "Single", value: SelectType.single },
    { text: "Multiple", value: SelectType.multiple }
  ];

  selection = new SelectionModel<SheetTemplate>(true, []);
  displayType = SelectType.single;
  dataSource!: MatTableDataSource<SheetTemplate>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  questionGuids: string[] = [];
  templates: SheetTemplate[] = [];

  paginatePlusSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
  
  selectHandler(row: SheetTemplate) {
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

  questionList: Question[] = [];

  chosenQuestions = new FormControl();
  
  template_Question: Template_Question = {
    id: '',
    questionId: ''
  }

  template_QuestionArr: Template_Question[] = [];

  addTemplateReq: any = new FormGroup({
    id: new FormControl(''),
    sheetName: new FormControl(''),
    countOfDays: new FormControl(7),
    questions: new FormControl(this.template_QuestionArr),
    mDate: new FormControl(Date()),
    cDate: new FormControl(Date()),
    mUser: new FormControl(''), 
    cUser: new FormControl(''), 
    status: new FormControl('') 
  });
  
  addQuestionReq: any = new FormGroup({
    id: new FormControl(''),
    questionName: new FormControl(''),
    qFullText: new FormControl(''),
    qShortText: new FormControl(''),
    mDate: new FormControl(Date()),
    cDate: new FormControl(Date()),
    mUser: new FormControl(''), 
    cUser: new FormControl(''), 
    status: new FormControl('') 
  });

  constructor(
    private timesheetsService: TimesheetsService,
    private router: Router
    ) { }
   
  ngOnInit(): void {
    this.timesheetsService.getAllTemplates()
    .subscribe({
      next: ((templates) => {
        this.templates = templates;
        this.dataSource = new MatTableDataSource(this.templates);
        this.paginatePlusSort();
      })
    })
    this.timesheetsService.getAllQuestions()
    .subscribe({ 
      next: (questions) => {
        this.questionList = questions;
      }
    });
  }


  getQuestionGuids() {
    this.chosenQuestions.value.forEach((q:any) => {
      this.questionGuids.push(q.id);
    });
  }

  addQuestion() {
    this.timesheetsService.addQuestion(this.addQuestionReq.value)
    .subscribe({
      next: (question) => {
        this.questionList.push(question);
        this.addQuestionReq.reset();
      }
    });
  }

  addTemplate() {
    if ((this.chosenQuestions.value != null && this.chosenQuestions.value.length < 1)) {
      this.getQuestionGuids();
      var tqObjects = [];
  
      for (let i=0; i < this.chosenQuestions.value.length; i++) {
        tqObjects[i]  = new Template_Question;
        tqObjects[i].questionId = this.questionGuids[i];
        tqObjects[i].id = Guid.create().toString();
      }
      
      this.addTemplateReq.patchValue({questions: tqObjects});
    }
    this.timesheetsService.addTemplate(this.addTemplateReq.value)
    .subscribe({
      next: (template) => {
        this.addTemplateReq.reset();
        this.chosenQuestions.reset();
    }
    });
  }

  allocatedQuestions: Question[] = [];
  getAllocatedQuestions(sheetTemplateId: string) {
    this.timesheetsService.getAllQuestionsByTemplateId(sheetTemplateId)
    .subscribe((questions) => {
      this.allocatedQuestions = questions;
    });
  }

  deleteTemplate(id: string) {
    /* this.timesheetsService.deleteTemplate(id)
    .subscribe({
      next: (response) => {
        location.reload();
      }
    }); */

  }

}


