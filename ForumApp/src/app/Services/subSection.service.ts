import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SubSectionModel } from "../models/subSectionModel";
@Injectable({'providedIn':'root'})
export class SubSectionService{
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
    constructor(private http: HttpClient) {}
    postAddSection(subSection: SubSectionModel)
    {
                                                                                                                                                                                       
      const body = {name:subSection.name,sectionId:subSection.sectionId};
      return this.http.post('https://localhost:44381/subsection', body,{headers:this.headerDict,withCredentials:true} ); 
    }
}