import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SectionService } from 'src/app/Services/section.service';
import { SubSectionService } from 'src/app/Services/subSection.service';
import { ThemeService } from 'src/app/Services/theme.service';

@Component({
  selector: 'app-theme-components',
  templateUrl: './theme-components.component.html',
  styleUrls: ['./theme-components.component.css']
})
export class ThemeComponentsComponent implements OnInit {
  private subcribes = [];
  themes$:any;
  subSection:any;
  private subSectionId:any;
  private subSections:any;
  constructor(
    private _themeService:ThemeService,
    private _activatedRoute: ActivatedRoute,
    private _subSectionService:SubSectionService) { }

  ngOnInit(): void {
    this.subcribes.push(this._activatedRoute.paramMap.subscribe(params => {  
      this.subSectionId = Number(params.get('id')); 
    }));
    this.themes$ = this._themeService.getThemesBySubSectionsId$(this.subSectionId);
    this.subcribes.push(this._subSectionService.getSubSections()
    .subscribe((data:any)=>{
      data.forEach(element => {
        if(element.sectionId==this.subSectionId){
          this.subSection = element
        }
      });
    }), (err: Error) => {
      //When unsuccessful, this will run
      console.error('Something broke!', err);
      
  });
  }
}
