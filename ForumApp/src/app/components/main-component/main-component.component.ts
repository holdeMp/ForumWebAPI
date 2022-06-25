import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SectionTitle } from 'src/app/models/RequestModels/sectionTitle';

import { SectionService } from '../../services/section.service';
import { SectionTitleService } from '../../services/sectionTitles.service';
import { SubSectionService } from '../../services/subSection.service';

@Component({
  selector: 'app-main-component',
  templateUrl: './main-component.component.html',
  styleUrls: ['./main-component.component.css']
})
export class MainComponentComponent implements OnInit {
  sectionsTitles : Observable<SectionTitle[]>;
  sections : any;
  subSections$ : any;
  constructor(
    
    private _sectionService: SectionService,
    private _sectionTitleService:SectionTitleService,
    private _subSectionService:SubSectionService) {
      this.sectionsTitles = this._sectionTitleService.sectionTitles;
    }

  ngOnInit() {
        //call the service
    this.subSections$ = this._subSectionService.getSubSections();
    
    this._sectionService.getSections$()
    .subscribe((data: any) => {
      //when successful, data is returned here and you can do whatever with it
      this.sections = data;
     
      
    }, (err: Error) => {
        //When unsuccessful, this will run
        console.error('Something broke!', err);
        
    });
    this._sectionTitleService.fetchSectionTitleData();
  }
  //get sections with specific section title
  public getSectionsWithSpecificTitleId(sections:any,sectionTitleId:number){
    let sectionsWithSectionTitleId=[];
    for(let section of sections){
      if(section.sectionTitleId == sectionTitleId){
        sectionsWithSectionTitleId.push(section);
      }
    }
    return sectionsWithSectionTitleId;
  }
}
