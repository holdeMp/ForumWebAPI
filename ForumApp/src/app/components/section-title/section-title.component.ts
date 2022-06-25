import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map } from 'rxjs/operators';
import { SectionService } from '../../services/section.service';
import { SectionTitleService } from '../../services/sectionTitles.service';

@Component({
  selector: 'app-section-title',
  templateUrl: './section-title.component.html',
  styleUrls: ['./section-title.component.css']
})
export class SectionTitleComponent implements OnInit {

  constructor(private _sectionService : SectionService,
    private _activatedRoute: ActivatedRoute,
    private _sectionTitleService:SectionTitleService) { }
  sections : any;
  sectionsByTitleId = [];
  sectionTitle :any;
  sectionTitleId : number;
  sub=[];
  ngOnInit() {
    this.sub.push(this._activatedRoute.paramMap.subscribe(params => { 
      
      this.sectionTitleId = Number(params.get('id')); 
    }));
    //call the service
    this.sub.push(this._sectionService.getSections$()
        .subscribe((data: any) => {
          //when successful, data is returned here and you can do whatever with it
          this.sections = data;
          
          this.sectionsByTitleId = this.getSectionByTitleId(this.sectionTitleId,this.sections);
        }, (err: Error) => {
            //When unsuccessful, this will run
            console.error('Something broke!', err);
            
        }));
    this.sub.push(this._sectionTitleService.getSectionsTitles$(false)
    .subscribe((data:any)=>{
      data.forEach(element => {
        if(element.id==this.sectionTitleId){
          this.sectionTitle = element
        }
      });
    }, (err: Error) => {
      //When unsuccessful, this will run
      console.error('Something broke!', err);
      
    }));
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
