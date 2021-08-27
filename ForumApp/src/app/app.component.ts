import { Component } from '@angular/core';
import {RegisterUserModel} from './user';
import { HttpService} from './http.service';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms'
import { passwordValidator,match } from './passvalidator.directive';
import { HttpErrorResponse } from '@angular/common/http';
import $ from "jquery";
import 'bootstrap';
import { LoginUserModel } from './loginUser';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [HttpService]
})
export class AppComponent {
  title = 'ForumApp';
  RegisterUserModel = new RegisterUserModel("","","","");
  receivedUser: RegisterUserModel | undefined; // полученный пользователь
  done: boolean = false;
  registerForm = new FormGroup({
    username:new FormControl('',[Validators.required,Validators.minLength(3)]),
    email:new FormControl('',[Validators.required,Validators.email]),
    password:new FormControl('',[Validators.required,Validators.minLength(6),passwordValidator()]),
    passwordConfirm:new FormControl('',[Validators.required,Validators.minLength(6)])
  },{
    validators:[match('password', 'passwordConfirm')]
  });
  loginForm = new FormGroup({
    loginUsername:new FormControl('',[Validators.required,Validators.minLength(3)]),
    loginPassword:new FormControl('',[Validators.required,Validators.minLength(6)])
  })
  get loginUsername(){
    return this.loginForm.get('loginUsername')
  }
  get loginPassword(){
    return this.loginForm.get('loginPassword')
  }
  get username(){
    return this.registerForm.get('username')
  }
  get email(){
    return this.registerForm.get('email')
  }
  get password(){
    return this.registerForm.get('password')
  }
  get passwordConfirm(){
    return this.registerForm.get('passwordConfirm')
  }
  constructor(private httpService: HttpService, private route: Router){
      
    }
    registerUser(user: any){
        let usertemp3 = new RegisterUserModel(user.value.username,user.value.email,user.value.password,user.value.passwordConfirm);
        this.httpService.postRegisterUser(usertemp3)
                .subscribe(
                    (data: any) => {this.receivedUser=data; this.done=true;this.route.navigate(['ConfirmEmail']);
                    document.getElementById("registerButton").click();},
                    error => {
                     
                      alert(error.error[0].description);
                      console.log(error.error[0].description);
                      this.route.navigate(['']);
                      
                      
                    }
                );
    }
    login(user:any){
      let userLoginModel = new LoginUserModel(user.value.loginUsername,user.value.loginPassword);
      this.httpService.postLogin(userLoginModel) .subscribe(
        (data: any) => {this.receivedUser=data; this.done=true;this.route.navigate(['']);
        document.getElementById("loginButton").click();},
        error => {
         
          alert(error.error[0].description);
          console.log(error.error[0].description);
          this.route.navigate(['']);
                   
        }
    );
    }
}