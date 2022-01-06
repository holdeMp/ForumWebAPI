import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-section-title',
  templateUrl: './section-title.component.html',
  styleUrls: ['./section-title.component.css']
})
export class SectionTitleComponent implements OnInit {

  constructor(private httpService: HttpService ,private _activatedRoute: ActivatedRoute,) { }
  sections : any;
  sectionTitleId : number;
  sub=[];
  ngOnInit(): void {
    this.sub.push(this._activatedRoute.paramMap.subscribe(params => { 
      console.log(params);
      this.sectionTitleId = Number(params.get('id')); 
    }));
    this.sub.push(this.httpService.getSectionsByTitleId(this.sectionTitleId).subscribe((sections:any)=>{
      console.log(sections);
      this.sections = sections;
      }
    ));
  }
  ngOnDestroy() {
    for(let sb of this.sub){
      sb.unsubscribe();
    }
    
  }
}
