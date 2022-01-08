import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { share } from "rxjs/operators";
import { SubSectionModel } from "../models/subSectionModel";
@Injectable({'providedIn':'root'})
export class SubSectionService{

    private subSections : Observable<any>;
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
    constructor(private http: HttpClient) {}
    postAddSubSection(subSection: SubSectionModel)
    {
                                                                                                                                                                                       
      const body = {name:subSection.name,sectionId:subSection.sectionId};
      return this.http.post('https://localhost:44381/subsection', body,{headers:this.headerDict,withCredentials:true} ); 
    }
    getSubSections(): Observable<any>{
      this.subSections = this.http.get('https://localhost:44381/subsection/',{headers:this.headerDict,withCredentials:true})
      .pipe(share());
      return this.subSections;
  
    }
    getSubSectionsById$(sectionId:any): Observable<any>{
          this.subSections = this.http.get('https://localhost:44381/subsection/'+sectionId,{headers:this.headerDict,withCredentials:true})
          .pipe(share());
          return this.subSections;
      
    }
}