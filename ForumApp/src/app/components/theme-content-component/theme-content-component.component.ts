import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Toast, ToastrService } from 'ngx-toastr';
import { AddAnswerModel } from 'src/app/models/AddAnswerModel';
import { AnswerService } from 'src/app/Services/answer.service';
import { LoginService } from 'src/app/Services/login.service';
import { ThemeService } from 'src/app/Services/theme.service';
import { UserService } from 'src/app/Services/user.service';
import { QuillConfiguration } from './quillConfigurations.module';

@Component({
  selector: 'app-theme-content-component',
  templateUrl: './theme-content-component.component.html',
  styleUrls: ['./theme-content-component.component.css']
})
export class ThemeContentComponentComponent implements OnInit {

  answerForm = new FormGroup({
    content:new FormControl('',[Validators.required,Validators.minLength(3)])
  });
  private themeId:any;
  answers:any;
  quillConfiguration = QuillConfiguration;
  theme:any;
  users:any[] = [];

  constructor(private _activatedRoute: ActivatedRoute, private _themeService: ThemeService, 
    private _answerService: AnswerService, private _userService: UserService,
    private _loginService: LoginService, private toast: ToastrService) { }

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
      this._answerService.getAnswersByThemeId(this.themeId)
      .subscribe((data:any)=>{
  
            this.answers = data
            for(let answer of this.answers){
              if(answer.authorId){
                this.getUsersById(answer.authorId);
              }
            }
        }), (err: Error) => {
          //When unsuccessful, this will run
          console.error('Something broke!', err);
          
        }
    
  }
  AddAnswer(answerForm)
  {
    let authorId = this._loginService.user.id;
    let newAnswer = new AddAnswerModel(authorId,answerForm.value.content);
    newAnswer.themeId = this.themeId;
    this._answerService.postAddAnswer(newAnswer).subscribe(
      async () => {
        this.toast.success("","Succesful adding new answer",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
        await new Promise(f => setTimeout(f, 1200));      
        },
      error => {
        this.toast.error("Error while adding answer");       
      }
    );
  }
  getUsersById(authorId){
    let user;
  this._userService.getUserByUserId(authorId)
      .subscribe((data:any)=>{
  
            user = data;
            this.users.push(user);
  
        }), (err: Error) => {
          //When unsuccessful, this will run
          console.error('Something broke!', err);
          
        }
        
  }
  getUserById(authorId){
    if(this.users && this.users.length > 0)
    {
        for(let user of this.users)
      {
          if(user.id === authorId)
          {
            return user;
          }
      }
    }
  }

}
