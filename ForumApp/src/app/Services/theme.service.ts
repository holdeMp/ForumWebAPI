import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { share } from "rxjs/operators";
import { AddThemeModel } from "../models/AddThemeModel";
import { SubSectionModel } from "../models/subSectionModel";
@Injectable({'providedIn':'root'})
export class ThemeService{

    private themes : Observable<any>;
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
    }
    constructor(private http: HttpClient) {}
    postAddTheme(theme: AddThemeModel)
    {                                                                                                                                                                                      
      const body = {name:theme.name,subSectionId:theme.subSectionId,
      content:theme.content};
      return this.http.post('https://localhost:44381/theme', body,{headers:this.headerDict,withCredentials:true} ); 
    }
    getThemesBySubSectionsId$(subSectionId:any): Observable<any>{
      this.themes = this.http.get(
          'https://localhost:44381/theme/'
          +subSectionId,
          {headers:this.headerDict,withCredentials:true})
          .pipe(share());
      return this.themes;
    }
}