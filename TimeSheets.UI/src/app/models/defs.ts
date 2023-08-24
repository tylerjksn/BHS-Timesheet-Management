export enum SelectType {
    single,
    multiple
}

export interface Student {
    id: string,
    programId: string,
    studentNumber: string,
    firstName: string,
    middleInitial: string,
    lastName: string,
    studentEmail: string,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}

export interface Timesheet {
    id: string,
    sheetName: string,
    periodId: string,
    studentNumber: string,
    firstName: string,
    lastName: string,
    companyName: string,
    mentorName: string,
    qAList: QA[],
    workList: Work[],
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}

export interface SheetTemplate {
    id: string,
    sheetName: string,
    countOfDays: number,
    questions: Template_Question[],
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}

export interface Question {
    id: string,
    questionName: string,
    qFullText: string,
    qShortText: string,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}

export class Template_Question {
    id: string = '';
    questionId: string = '';
}

export interface QA {
    id: string,
    questionId: string,
    ans: string,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}



export interface Period {
    id: string,
    periodId: string,
    year: number,
    quarter: number,
    week: number,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}

export interface Work {
    id: string,
    date: string,
    day: string,
    timeIn: string,
    timeOut: string,
    hours: number,
    comment: string,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}

export interface Program {
    id: string,
    programId: string,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string
}

export interface Company {
    id: string,
    companyName: string,
    phone: string,
    email: string,
    address: string,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string 
}

export interface Mentor {
    id: string,
    firstName: string,
    lastName: string,
    phone: string,
    email: string,
    employmentLevel: string,
    mDate: string,
    cDate: string,
    mUser: string, 
    cUser: string, 
    status: string
}
