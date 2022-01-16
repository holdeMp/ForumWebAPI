import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../../app.component';
import { HttpService } from '../../http.service';
import { SectionService } from '../../Services/section.service';
import { SectionTitleService } from '../../Services/sectionTitles.service';
import { SubSectionService } from '../../Services/subSection.service';

@Component({
  selector: 'app-main-component',
  templateUrl: './main-component.component.html',
  styleUrls: ['./main-component.component.css']
})
export class MainComponentComponent implements OnInit {
  sectionsTitles:any;
  sections : any;
  subSections$ : any;
  constructor(
    
    private _sectionService: SectionService,
    private _sectionTitleService:SectionTitleService,
    private _subSectionService:SubSectionService) { }

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
    this._sectionTitleService.getSectionsTitles$()
    .subscribe((data: any) => {
      //when successful, data is returned here and you can do whatever with it
      this.sectionsTitles = data;
      
      
    }, (err: Error) => {
        //When unsuccessful, this will run
        console.error('Something broke!', err);
        
    });
    
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
