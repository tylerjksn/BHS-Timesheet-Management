import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SelectType, Student } from 'src/app/models/defs';
import { StudentsService } from 'src/app/services/students.service';



@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  displayedColumns: string[] = ['select', 'studentNumber', 'firstName', 'middleInitial', 'lastName', 
  'programId', 'status'];

  selectType = [
    { text: "Single", value: SelectType.single },
    { text: "Multiple", value: SelectType.multiple }
  ];

  selection = new SelectionModel<Student>(true, []);
  displayType = SelectType.single;
  dataSource!: MatTableDataSource<Student>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  students: Student[] = [];

  paginatePlusSort() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }
  
  selectHandler(row: Student) {
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

  constructor(private studentsService: StudentsService) {
  }

  ngOnInit(): void {
    this.studentsService.getAllStudents()
    .subscribe({ 
      next: (students) => {
        this.students = students;
        this.dataSource = new MatTableDataSource(this.students);
        this.paginatePlusSort();
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  

}
