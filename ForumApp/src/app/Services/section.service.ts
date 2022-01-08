import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { share, tap } from "rxjs/operators";
import { UpdateSectionModel } from "../models/UpdateSectionModel";
@Injectable({'providedIn':'root'})
export class SectionService{
    sections : Observable<any>;
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
    }
    constructor(private http: HttpClient) {}
    getSections$(): Observable<any>{
        //if we already got the sections, just return that
        if (this.sections) {
            return this.sections;
        }
        else{
            this.sections = this.http.get('https://localhost:44381/section',{headers:this.headerDict,withCredentials:true})
            .pipe(share());
            return this.sections;
        }
    }
    findSectionByName(sectionName:string,sections:any){
        for(var section of sections){
            if(section.name==sectionName){
                return section;
            }
        }
    }
    updateSection(updateSection:UpdateSectionModel){
        let body = updateSection;
        return this.http.put('https://localhost:44381/section',body,{headers:this.headerDict,withCredentials:true});
    }
    findSectionIdByName(sectionName:string,sections:any){
        var sectionId = 0;
        for(var section of sections)
        { 
            if(section.name==sectionName){
                sectionId = section.sectionId;
            } 
        }
        return sectionId;
    }
}