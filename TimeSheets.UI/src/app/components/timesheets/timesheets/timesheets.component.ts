import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SelectType, Timesheet } from 'src/app/models/defs';
import { TimesheetsService } from 'src/app/services/timesheets.service';

@Component({
  selector: 'app-timesheets',
  templateUrl: './timesheets.component.html',
  styleUrls: ['./timesheets.component.css']
})
export class TimesheetsComponent implements OnInit {

  displayedColumns: string[] = ['select', 'sheetName', 'periodId', 'studentNumber', 'firstName',
  'lastName', 'companyName', 'status'];

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

  constructor(
    private timesheetsService: TimesheetsService
  ) { }

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

  ngOnInit(): void {
    this.timesheetsService.getAllTimesheets()
    .subscribe({ 
      next: (timesheets) => {
        this.timesheets = timesheets;
        this.dataSource = new MatTableDataSource(this.timesheets);
        this.paginatePlusSort();
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

}


