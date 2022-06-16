import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { share } from "rxjs/operators";
import { AddAnswerModel } from "../models/AddAnswerModel";
import { AddThemeModel } from "../models/AddThemeModel";
@Injectable({'providedIn':'root'})
export class AnswerService{

    private answers : Observable<any>;
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
    }
    constructor(private http: HttpClient) {}

    getAnswersByThemeId(answerId:any): Observable<any>{
      this.answers = this.http.get(
          'https://localhost:44381/Theme/answers/theme/'
          +answerId,
          {headers:this.headerDict,withCredentials:true});
      return this.answers;
    }
    postAddAnswer(answer: AddAnswerModel)
    {                                                                                                                                                                                      
      const body = {authorId:answer.authorId,content:answer.content,
        referenceAnswerId:answer.referenceAnswerId,themeId:answer.themeId};
      return this.http.post('https://localhost:44381/theme/answer', body,{headers:this.headerDict,withCredentials:true} ); 
    }
}