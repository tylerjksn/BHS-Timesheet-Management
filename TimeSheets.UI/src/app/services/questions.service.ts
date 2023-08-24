import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Question } from '../models/defs';

@Injectable({
  providedIn: 'root'
})
export class QuestionsService {

  baseApiUrl: string = environment.baseApiURL;

  constructor(
    private http: HttpClient
  ) { }

  getQuestion(uniqueId: string): Observable<Question> {
    return this.http.get<Question>(this.baseApiUrl + '/api/Questions/' + uniqueId);
  }
}
