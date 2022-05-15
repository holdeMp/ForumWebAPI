import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddThemeModel } from 'src/app/models/AddThemeModel';
import { SubSectionService } from 'src/app/Services/subSection.service';
import { ThemeService } from 'src/app/Services/theme.service';
import { QuillConfiguration } from "./quillConfigurations.module";

@Component({
  selector: 'app-addthemecomponent',
  templateUrl: './addthemecomponent.component.html',
  styleUrls: ['./addthemecomponent.component.css']
})


export class AddthemecomponentComponent implements OnInit {
  private subSectionId:any;
  subSection:any;
  quillConfiguration = QuillConfiguration;
  constructor(private _activatedRoute: ActivatedRoute,
    private themeService: ThemeService,
    private toastr: ToastrService,
    private route: Router,
    private subSectionService: SubSectionService){}

  ngOnInit(): void 
  {
    this._activatedRoute.paramMap.subscribe(params => { 
      
      this.subSectionId = Number(params.get('id')); 
    });
    this.subSectionService.getSubSections()
      .subscribe((data:any)=>{
        data.forEach(element => {
          if(element.sectionId==this.subSectionId){
            this.subSection = element
          }
        });
      }), (err: Error) => {
        //When unsuccessful, this will run
        console.error('Something broke!', err);
        
    }
  }

  themeForm = new FormGroup({
    name:new FormControl('',[Validators.required,Validators.minLength(3)]),
    content:new FormControl('',[Validators.required,Validators.minLength(3)])
  });

  AddTheme(theme:any){  
    let newTheme = new AddThemeModel(theme.value.name,theme.value.content,this.subSectionId);
    this.themeService.postAddTheme(newTheme).subscribe(
      async () => {
        this.toastr.success("","Succesful adding new theme",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
        await new Promise(f => setTimeout(f, 1200));      
        this.route.navigate(['']);
        },
      error => {
        this.toastr.error("Error while adding theme");       
      }
    );
  }
}
