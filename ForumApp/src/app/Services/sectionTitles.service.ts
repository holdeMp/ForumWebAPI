import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { map, share, shareReplay, tap } from "rxjs/operators";
import { SectionModel } from "../models/sectionModel";
import { SectionTitleModel } from "../models/SectionTitleModel";
@Injectable({'providedIn':'root'})
export class SectionTitleService{
    private sectionsTitles : Observable<any>;
    headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
    constructor(private http: HttpClient) {}
    getSectionsTitles$(){
        if (this.sectionsTitles) {
            return this.sectionsTitles;
        }
        else{
            this.sectionsTitles = this.http.get('https://localhost:44381/sectiontitle',{headers:this.headerDict,withCredentials:true})
            .pipe(share());
            return this.sectionsTitles;
        }
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