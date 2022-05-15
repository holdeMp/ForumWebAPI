import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SectionService } from '../../Services/section.service';
import { SectionTitleService } from '../../Services/sectionTitles.service';
import { SubSectionService } from '../../Services/subSection.service';

@Component({
  selector: 'app-subsection-component',
  templateUrl: './subsection-component.component.html',
  styleUrls: ['./subsection-component.component.css']
})
export class SubsectionComponentComponent implements OnInit {
  sub = [];
  section :any;
  subSections$:any;
  sectionId : any;
  subSectionsBySectionId = [];
  constructor(
    private _subSectionService: SubSectionService,
    private _activatedRoute: ActivatedRoute,
    private _sectionService : SectionService) { }

  ngOnInit(): void {
    this.sub.push(this._activatedRoute.paramMap.subscribe(params => { 
      
      this.sectionId = Number(params.get('id')); 
    }));
    this.subSections$ = this._subSectionService.getSubSectionsById$(this.sectionId);
    this.sub.push(this._sectionService.getSections$()
      .subscribe((data:any)=>{
        data.forEach(element => {
          if(element.sectionId==this.sectionId){
            this.section = element
          }
        });
      }), (err: Error) => {
        //When unsuccessful, this will run
        console.error('Something broke!', err);
        
    });
  }
}
