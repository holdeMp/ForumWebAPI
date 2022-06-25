import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { share } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { Section } from "../models/RequestModels/section";
import { UpdateSectionModel } from "../models/UpdateModels/UpdateSectionModel";

@Injectable({'providedIn':'root'})
export class SectionService{

    private url = "section"
    private sections : Observable<Section[]>;
    private readonly headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
    }

    constructor(private http: HttpClient) {}
    
    getSections$(): Observable<Section[]>{
        //if we already got the sections, just return that
        if (this.sections) {
            return this.sections;
        }
        else{
            this.sections = this.http.get<Section[]>('https://localhost:44381/section',{headers:this.headerDict,withCredentials:true})
                .pipe(share());
            return this.sections;
        }
    }

    findSectionByName(sectionName:string, sections:Section[]) : Section{
        for(var section of sections){
            if(section.name==sectionName){
                return section;
            }
        };
        return null;
    }

    updateSection(updateSection:UpdateSectionModel){
        return this.http.put(`${environment.apiUrl}/${this.url}`, updateSection, {headers:this.headerDict,withCredentials:true});
    }

    findSectionIdByName(sectionName:string, sections:Section[]) : number{
        var sectionId = 0;
        for(var section of sections)
        { 
            if(section.name == sectionName){
                sectionId = section.sectionId;
            } 
        }
        return sectionId;
    }
}