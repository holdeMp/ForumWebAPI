import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UpdateSectionModel } from "../models/UpdateSectionModel";
@Injectable({'providedIn':'root'})
export class SectionService{
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
    constructor(private http: HttpClient) {}
    updateSection(updateSection:UpdateSectionModel){
        let body = updateSection;
        return this.http.put('https://localhost:44381/section',body,{headers:this.headerDict,withCredentials:true});
    }
    findSectionIdByName(sectionName:string,sections:any){
        var sectionId = 0;
        for(var section of sections)
        { 
            if(section.name===sectionName){
                sectionId = section.id;
            } 
        }
        return sectionId;
    }
}