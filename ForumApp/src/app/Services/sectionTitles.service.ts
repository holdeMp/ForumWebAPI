import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SectionModel } from "../models/sectionModel";
import { SectionTitleModel } from "../models/SectionTitleModel";
@Injectable({'providedIn':'root'})
export class SectionTitleService{
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
    constructor(private http: HttpClient) {}
    getSectionsTitles(){
        return this.http.get('https://localhost:44381/sectiontitle',{headers:this.headerDict,withCredentials:true});
    }
    updateSectionTitle(updateSectionTitle:any){

        let body = {id:updateSectionTitle.id,name:updateSectionTitle.name,sections:updateSectionTitle.sections};
        return this.http.put('https://localhost:44381/sectiontitle',body,{headers:this.headerDict,withCredentials:true});
    }
    findSectionIdByName(sectionTitleName:string,sectionsTitles:any){
        var sectionId = 0;
        for(var section of sectionsTitles)
        { 
            if(section.name===sectionTitleName){
                sectionId = section.id;
            } 
        }
        return sectionId;
    }
}