import { Component } from '@angular/core';
import {RegisterUserModel} from './RegisterUserModel';
import { HttpService} from './http.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { passwordValidator,match } from './passvalidator.directive';
import { ToastrService } from 'ngx-toastr';
import 'bootstrap';
import { LoginUserModel } from './loginUser';
import { User } from './user';
import { LoginService } from './login.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [HttpService]
})
export class AppComponent {
  title = 'ForumApp';
  RegisterUserModel = new RegisterUserModel("","","","");
  receivedUser :RegisterUserModel|undefined;
  user : User|any; // полученный пользователь
  logged: boolean = false;
  registred :boolean = false;
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
    loginPassword:new FormControl('',[Validators.required,Validators.minLength(6),passwordValidator()]),
    rememberMe:new FormControl(false)
  })
  get rememberMe(){
    return this.loginForm.get('rememberMe')
  }
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
  setUser(user:User){

  }
  logout(){
    this.httpService.resetcredentials();
    //this.loginService.logout();
  }
  getLoginService():LoginService{
    return this.loginService;
  }
  getCurrentUser():any{
      return this.loginService.currentUser;
  }
  
  getUser():User{
    return this.loginService.user;
  }
  constructor(private httpService: HttpService, private route: Router,private toastr: ToastrService,
    private loginService :LoginService){
      
    }
    registerUser(user: any){
        let usertemp3 = new RegisterUserModel(user.value.username,user.value.email,user.value.password,user.value.passwordConfirm);
        this.httpService.postRegisterUser(usertemp3)
                .subscribe(
                    async (data: any) => {
                      this.receivedUser=data; 
                      this.registred=true;
                      this.toastr.success("navigating to login page ...","Succesful registration",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
                      await new Promise(f => setTimeout(f, 1200));
                      
                      this.route.navigate(['login']);
                    document.getElementById("registerButton").click();},
                    error => {
                     
                      this.toastr.error(error.error[0].description);
                      console.log(error.error[0].description);
                      this.route.navigate(['']);
                      
                      
                    }
                );
    }
    login(user:any){
      let userLoginModel = new LoginUserModel(user.value.loginUsername,user.value.loginPassword,user.value.rememberMe);
      this.httpService.postLogin(userLoginModel) .subscribe(
        (data: User) => {
                           console.log("Succesful loggining...")
                          this.loginService.login(data);
                          this.toastr.success(null,"Succesful login",{tapToDismiss:true,timeOut:1000,progressAnimation:'increasing',progressBar:true})
                          
                          this.logged=true;                     
                          document.getElementById("loginButton").click();
                        },
        error => {
          if(error!==undefined){
            this.toastr.error("Incorrect username or password","Incorrect login attempt",{tapToDismiss:true,timeOut:2000});
            console.log(error.error[0].description);
          }
          this.route.navigate(['']);
                   
        }
    );
    }
}