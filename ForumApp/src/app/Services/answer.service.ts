import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AddAnswerModel } from "../models/AddAnswerModel";
import { Answer } from "../models/RequestModels/answer";

@Injectable({'providedIn':'root'})
export class AnswerService{

    private  readonly headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
    }
    private readonly url = "theme/answer";

    constructor(private http: HttpClient) {}

    getAnswersByThemeId(answerId : number) : Observable<Answer[]>{
      return this.http.get<Answer[]>(`${environment.apiUrl}/${answerId}`,
          {headers:this.headerDict, withCredentials:true});
    }

    postAddAnswer(answer: AddAnswerModel)
    {                                                            
      const body = {
                    authorId:answer.authorId,
                    content:answer.content,
                    referenceAnswerId:answer.referenceAnswerId,
                    themeId:answer.themeId
                  };
     
      return this.http.post(`${environment.apiUrl}/${this.url}`, body, {headers:this.headerDict,withCredentials:true} ); 
    }
}