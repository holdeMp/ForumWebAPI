import { Component, OnInit } from '@angular/core';
import {RegisterUserModel} from './RegisterUserModel';
import { HttpService} from './http.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { passwordValidator,match } from './passvalidator.directive';
import { ToastrService } from 'ngx-toastr';
import 'bootstrap';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import {MatSelectModule} from '@angular/material/select';
import { LoginUserModel } from './loginUser';
import { User } from './user';
import { LoginService } from './login.service';
import { SectionModel } from './sectionModel';
import { SectionTitleModel } from './models/SectionTitleModel';
import { Observable } from 'rxjs';
import { validateHorizontalPosition } from '@angular/cdk/overlay';
import { SectionTitleService } from './Services/sectionTitles.service';
import { SectionService } from './Services/section.service';
import { UpdateSectionModel } from './models/UpdateSectionModel';
import { resolveSanitizationFn } from '@angular/compiler/src/render3/view/template';
import { data } from 'jquery';
import { HttpResponse } from '@angular/common/http';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [HttpService]
})
export class AppComponent implements OnInit {
  selectedSection:any;
  
  title = 'ForumApp';
  RegisterUserModel = new RegisterUserModel("","","","");
  receivedUser :RegisterUserModel|undefined;
  user : User|any; // полученный пользователь
  logged: boolean = false;
  registred :boolean = false;
  sectionsTitles:any;
  sections : any;
  ngOnInit() {
    if(this.getLoginService().user && this.getLoginService().user.roles && this.getLoginService().user.roles.includes('admin')){
      this.httpService.getSections().subscribe((sections:any)=>{
            console.log(sections);
            this.sections = sections;
        }
      ); 
    }
    this.sectionTitleService.getSectionsTitles().subscribe((sectionsTitles:any)=>{
      console.log(sectionsTitles);
      this.sectionsTitles = sectionsTitles;
    })
  }
  updateSectionForm = new FormGroup({
    sectionName:new FormControl('',[Validators.required,Validators.minLength(3)]),
    sectionTitle:new FormControl('',[Validators.required,Validators.minLength(3)])
  })
  sectionTitleForm = new FormGroup({
    name:new FormControl('',[Validators.required,Validators.minLength(3)])
  })
  sectionForm = new FormGroup({
    name:new FormControl('',[Validators.required,Validators.minLength(3)])
  })
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
  updateSection(updateSectionModel:any){
    const sectionId =this.sectionService.findSectionIdByName(this.selectedSection,this.sections);
    let updateSection = new SectionModel(sectionId,updateSectionModel.value.sectionName,null);
    this.sectionService.updateSection(new UpdateSectionModel(updateSection,updateSectionModel.value.sectionTitle)).subscribe(
      async () => {
        this.toastr.success("","Succesful updating section",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
        await new Promise(f => setTimeout(f, 1200));      
        this.route.navigate(['']);
      document.getElementById("updateSectionButton").click();},
      error => {
        
        this.toastr.error("Error while updating section");       
      }
    );;

  }
  addSection(sectionName:any){
    let newsection = new SectionModel(0,sectionName.value.name,[0]);
    this.httpService.postAddSection(newsection).subscribe(
      async () => {
        this.toastr.success("","Succesful adding new section",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
        await new Promise(f => setTimeout(f, 1200));      
        this.route.navigate(['']);
      document.getElementById("sectionButton").click();},
      error => {     
        this.toastr.error("Error while adding section");       
      }
    );
  }

  addSectionTitle(sectionTitleName:any){
    let newSectionTitle = new SectionTitleModel(0,sectionTitleName.value.name,[0]);
    this.httpService.postAddSectionTitle(newSectionTitle).subscribe(
      async () => {
        this.toastr.success("","Succesful adding new section title",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
        await new Promise(f => setTimeout(f, 1200));      
        this.route.navigate(['']);
      document.getElementById("sectionTitleButton").click();},
      error => {     
        this.toastr.error("Error while adding section title");       
      }
    );
  }

  logout(){
    this.httpService.resetcredentials();
    this.loginService.logout();
    this.httpService.postLogout().subscribe(
      async (data: any) => {
        this.receivedUser=data; 
        this.registred=true;
        this.toastr.success("","Succesful logout",{timeOut:2000,progressBar:true,progressAnimation:'increasing'});},
      error => {      
        this.toastr.error(error.error[0].description);
        console.log(error.error[0].description);
        this.route.navigate(['']);        
      }
    );
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
    private loginService :LoginService,private sectionTitleService:SectionTitleService,private sectionService:SectionService){
      
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
                          this.route.navigate(['']);
                          
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