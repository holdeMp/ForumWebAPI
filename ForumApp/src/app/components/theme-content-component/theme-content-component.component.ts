import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ThemeService } from 'src/app/Services/theme.service';

@Component({
  selector: 'app-theme-content-component',
  templateUrl: './theme-content-component.component.html',
  styleUrls: ['./theme-content-component.component.css']
})
export class ThemeContentComponentComponent implements OnInit {

  private themeId:any;

  theme:any;

  constructor(private _activatedRoute: ActivatedRoute, private _themeService: ThemeService) { }

  ngOnInit(): void {
    
    this._activatedRoute.paramMap.subscribe(params => {   
      this.themeId = Number(params.get('id')); 
    });

    this._themeService.getThemeByThemeId(this.themeId)
    .subscribe((data:any)=>{

          this.theme = data

      }), (err: Error) => {
        //When unsuccessful, this will run
        console.error('Something broke!', err);
        
      }
    
  }

}
