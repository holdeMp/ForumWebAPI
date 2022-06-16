import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AddAnswerModel } from 'src/app/models/AddAnswerModel';
import { AddThemeModel } from 'src/app/models/AddThemeModel';
import { LoginService } from 'src/app/Services/login.service';
import { SubSectionService } from 'src/app/Services/subSection.service';
import { ThemeService } from 'src/app/Services/theme.service';
import { User } from 'src/app/user';
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
    private subSectionService: SubSectionService,
    private loginService:LoginService){}

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
    let authorId = this.loginService.user.id;
    let newAnswer = new AddAnswerModel(authorId,theme.value.content);
    let newTheme = new AddThemeModel(theme.value.name,this.subSectionId);
    newTheme.addAnswerModel = newAnswer;
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
