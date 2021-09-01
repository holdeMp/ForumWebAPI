import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
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
}