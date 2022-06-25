import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { share } from "rxjs/operators";
import { environment } from "src/environments/environment";
import { SectionTitle } from "../models/RequestModels/sectionTitle";
import { UpdateSectionTitle } from "../models/UpdateModels/UpdateSectionTitle";

@Injectable({'providedIn':'root'})
export class SectionTitleService{
    
    private _sectionTitlesChange = new Subject<SectionTitle[]>();
    private _sectionsTitles : Observable<SectionTitle[]>;
    public sectionTitles = this._sectionTitlesChange.asObservable();
    private readonly headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
    }
    private url = "sectiontitle"

    constructor(private http: HttpClient) {}

    getSectionsTitles$(isRerequest : boolean) : Observable<SectionTitle[]> {
        if (this._sectionsTitles && !isRerequest) {
            return this._sectionsTitles;
        }
        else{
            this._sectionsTitles = this.http.get<SectionTitle[]>(`${environment.apiUrl}/${this.url}`,{headers:this.headerDict,withCredentials:true})
            .pipe(share());
            return this._sectionsTitles;
        }
    }

    updateSectionTitle(updateSectionTitle : UpdateSectionTitle) : Observable <SectionTitle>{
        let response : Observable<SectionTitle>;
        response = this.http.put<SectionTitle>(`${environment.apiUrl}/${this.url}`, updateSectionTitle, {headers:this.headerDict,withCredentials:true});
        this.fetchSectionTitleData();

        return response;
    }

    findSectionIdByName(sectionTitleName:string, sectionsTitles:SectionTitle[]) : number{

        let sectionId = 0;

        for(let section of sectionsTitles)
        { 
            if(section.name === sectionTitleName){
                sectionId = section.id;
            } 
        }

        return sectionId;
    }

    fetchSectionTitleData(){
        this.getSectionsTitles$(true).subscribe(resp => {
            this._sectionTitlesChange.next(resp);
          });
    }
}