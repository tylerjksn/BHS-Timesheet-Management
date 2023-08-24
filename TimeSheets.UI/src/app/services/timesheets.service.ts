import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Question, SheetTemplate, Timesheet, Work } from '../models/defs';

@Injectable({
  providedIn: 'root'
})
export class TimesheetsService {

  baseApiUrl: string = environment.baseApiURL;

  constructor(
    private http: HttpClient,
  ) { }

  getAllTimesheets(): Observable<Timesheet[]> {
    return this.http.get<Timesheet[]>(this.baseApiUrl + '/api/timesheets');
  }

  getTimesheet(id: string): Observable<Timesheet> {
    return this.http.get<Timesheet>(this.baseApiUrl + '/api/timesheets/' + id);
  }

  getStudentTimesheetsBySID(sid: string): Observable<Timesheet[]> {
    return this.http.get<Timesheet[]>(this.baseApiUrl + '/api/timesheets/' + sid);
  }

  addTimesheet(addTimesheetReq: Timesheet): Observable<Timesheet> {
    var cDate = new Date();
    cDate.setHours(cDate.getHours() - 4);
    /* addTimesheetReq.sheetId = this.getSheetId(); */
    addTimesheetReq.id = Guid.create().toString();
    addTimesheetReq.cDate = cDate.toISOString();
    addTimesheetReq.mDate = cDate.toISOString();
    return this.http.post<Timesheet>(this.baseApiUrl + '/api/timesheets', 
    addTimesheetReq);
  }

  updateTimesheet(id: string, updateTimesheetReq: Timesheet): Observable<Timesheet> {
    var uDate = new Date();
    console.log(uDate);
    uDate.setHours(uDate.getHours() - 4);
    updateTimesheetReq.mDate = uDate.toISOString();
    return this.http.put<Timesheet>(this.baseApiUrl + '/api/timesheets/' + id, updateTimesheetReq);
  }

  deleteTimesheet(id: string): Observable<Timesheet> {
    return this.http.delete<Timesheet>(this.baseApiUrl + '/api/timesheets/' + id);
  }

  getQuestion(id: string): Observable<Question> {
    return this.http.get<Question>(this.baseApiUrl + '/api/questions/' + id);
  }

  getAllQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(this.baseApiUrl + '/api/questions');
  }

  getAllQuestionsByTemplateId(sheetTemplateId: string) : Observable<Question[]> {
    return this.http.get<Question[]>(this.baseApiUrl + '/api/questions/' + sheetTemplateId);
  }

  addQuestion(addQuestionReq: Question): Observable<Question> {
    var cDate = new Date();
    cDate.setHours(cDate.getHours() - 4);
    addQuestionReq.id = Guid.create().toString();
    addQuestionReq.cDate = cDate.toISOString();
    addQuestionReq.mDate = cDate.toISOString();
    return this.http.post<Question>(this.baseApiUrl + '/api/questions', 
    addQuestionReq);
  }

  deleteQuestion(id: string): Observable<Question> {
    return this.http.delete<Question>(this.baseApiUrl + '/api/questions/' + id);
  }

  getAllTemplates(): Observable<SheetTemplate[]> {
    return this.http.get<SheetTemplate[]>(this.baseApiUrl + '/api/sheetTemplates');
  }

  addTemplate(addTemplateReq: SheetTemplate): Observable<SheetTemplate> {
    var cDate = new Date();
    cDate.setHours(cDate.getHours() - 4);
    addTemplateReq.id = Guid.create().toString();
    addTemplateReq.cDate = cDate.toISOString();
    addTemplateReq.mDate = cDate.toISOString();
    return this.http.post<SheetTemplate>(this.baseApiUrl + '/api/sheetTemplates', 
    addTemplateReq);
  }

  deleteTemplate(id: string): Observable<SheetTemplate> {
    return this.http.delete<SheetTemplate>(this.baseApiUrl + '/api/sheetTemplates/' + id);
  }


}
