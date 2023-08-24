import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AddStudentComponent } from './components/students/add-student/add-student.component';
import { EditStudentComponent } from './components/students/edit-student/edit-student.component';
import { AddTemplateComponent } from './components/timesheets/add-template/add-template.component';
import { AddTimesheetComponent } from './components/timesheets/add-timesheet/add-timesheet.component';
import { EditTimesheetComponent } from './components/timesheets/edit-timesheet/edit-timesheet.component';
import { TimesheetsComponent } from './components/timesheets/timesheets/timesheets.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'dashboard/add',
    component: AddStudentComponent
  },
  {
    path: 'dashboard/edit/:id',
    component: EditStudentComponent
  },
  {
    path: 'timesheets',
    component: TimesheetsComponent
  },
  {
    path: 'timesheets/add',
    component: AddTimesheetComponent
  },
  {
    path: 'timesheets/edit/:id',
    component: EditTimesheetComponent
  },
  {
    path: 'timesheets/template',
    component: AddTemplateComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
