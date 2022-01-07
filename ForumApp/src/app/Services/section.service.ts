import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { tap } from "rxjs/operators";
import { UpdateSectionModel } from "../models/UpdateSectionModel";
@Injectable({'providedIn':'root'})
export class SectionService{
    sections: any;
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
    constructor(private http: HttpClient) {}
    getSections$(): Observable<any>{
        //if we already got the sections, just return that
        if (this.sections) {
            return of(this.sections);
        }
        return this.http.get<any>('https://localhost:44381/section',{headers:this.headerDict,withCredentials:true})
            .pipe(tap((returnedData: any) => {
                //save the returned data so we can re-use it later without making more HTTP calls
                this.sections = returnedData;
            }));;
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