import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../http.service';
import { SectionService } from '../Services/section.service';

@Component({
  selector: 'app-section-title',
  templateUrl: './section-title.component.html',
  styleUrls: ['./section-title.component.css']
})
export class SectionTitleComponent implements OnInit {

  constructor(private httpService: HttpService ,private _sectionService : SectionService,private _activatedRoute: ActivatedRoute,) { }
  sections : any;
  sectionsByTitleId = [];
  sectionTitleId : number;
  sub=[];
  ngOnInit() {
    this.sub.push(this._activatedRoute.paramMap.subscribe(params => { 
      console.log(params);
      this.sectionTitleId = Number(params.get('id')); 
    }));
    //call the service
    this.sub.push(this._sectionService.getSections$()
        .subscribe((data: any) => {
          //when successful, data is returned here and you can do whatever with it
          this.sections = data;
          console.log(this.sections);
          this.sectionsByTitleId = this.getSectionByTitleId(this.sectionTitleId,this.sections);
        }, (err: Error) => {
            //When unsuccessful, this will run
            console.error('Something broke!', err);
            
        }));
    
  }
  ngOnDestroy() {
    for(let sb of this.sub){
      sb.unsubscribe();
    }
    
  }
  //get sections by title id
  getSectionByTitleId(titleId:any,sections:any){
    let resSectionsByTitleId = [];
    for(let section of sections){
      if(section.sectionTitleId == titleId){
        resSectionsByTitleId.push(section);
      }
    }
    return resSectionsByTitleId;
  }
}
