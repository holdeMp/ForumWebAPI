import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { share } from "rxjs/operators";
import { AddAnswerModel } from "../models/AddAnswerModel";
import { AddThemeModel } from "../models/AddThemeModel";
@Injectable({'providedIn':'root'})
export class ThemeService{

    private themes : Observable<any>;
    private theme : Observable<any>;
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
    }
    constructor(private http: HttpClient) {}

    postAddTheme(theme: AddThemeModel)
    {                                                                                                                                                                                      
      const body = {name:theme.name,subsectionid:theme.subSectionId,
      content:theme.addAnswerModel.content,answer:theme.addAnswerModel};
      return this.http.post('https://localhost:44381/theme', body,{headers:this.headerDict,withCredentials:true} ); 
    }

    getThemesBySubSectionsId$(subSectionId:any): Observable<any>{
      this.themes = this.http.get(
          'https://localhost:44381/theme/subsection/'
          +subSectionId,
          {headers:this.headerDict,withCredentials:true})
          .pipe(share());
      return this.themes;
    }

    getThemeByThemeId(themeId:any): Observable<any>{
      this.theme = this.http.get(
          'https://localhost:44381/theme/'
          +themeId,
          {headers:this.headerDict,withCredentials:true});
      return this.theme;
    }
}