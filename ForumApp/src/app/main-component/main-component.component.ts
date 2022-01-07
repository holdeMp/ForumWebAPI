import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';
import { HttpService } from '../http.service';
import { SectionTitleService } from '../Services/sectionTitles.service';

@Component({
  selector: 'app-main-component',
  templateUrl: './main-component.component.html',
  styleUrls: ['./main-component.component.css']
})
export class MainComponentComponent implements OnInit {
  sectionsTitles:any;
  sections : any;

  constructor(
    private httpService: HttpService,
    private sectionTitleService:SectionTitleService) { }

  ngOnInit() {
    this.httpService.getSections().subscribe((sections:any)=>{
      console.log(sections);
      this.sections = sections;
      }
    ); 
    this.sectionTitleService.getSectionsTitles().subscribe((sectionsTitles:any)=>{
      console.log(sectionsTitles);
      this.sectionsTitles = sectionsTitles;
    })
    
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
