import { Component, OnInit } from '@angular/core';
import {RegisterUserModel} from './models/RegisterUserModel';
import { HttpService} from './http.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { passwordValidator,match } from './passvalidator.directive';
import { ToastrService } from 'ngx-toastr';
import 'bootstrap';
import { LoginUserModel } from './loginUser';
import { User } from './user';
import { LoginService } from './Services/login.service';
import { SectionModel } from './models/sectionModel';
import { SectionTitleModel } from './models/SectionTitleModel';
import { SectionTitleService } from './Services/sectionTitles.service';
import { SectionService } from './Services/section.service';
import { UpdateSectionModel } from './models/UpdateSectionModel';
import { SubSectionModel } from './models/subSectionModel';
import { SubSectionService } from './Services/subSection.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [HttpService]
})
export class AppComponent implements OnInit {
  selectedSection:any;
  selectedSectionTitle:any;
  title = 'ForumApp';
  RegisterUserModel = new RegisterUserModel("","","","");
  receivedUser :RegisterUserModel|undefined;
  user : User|any; // полученный пользователь
  logged: boolean = false;
  registred :boolean = false;
  sectionsTitles:any;
  sections : any;
  ngOnInit() { 
    this.sectionService.getSections$()
        .subscribe((data: any) => {
          //when successful, data is returned here and you can do whatever with it
          this.sections = data;
          
          
        }, (err: Error) => {
            //When unsuccessful, this will run
            console.error('Something broke!', err);
            
        });
        this.sectionTitleService.getSectionsTitles$()
    .subscribe((data: any) => {
      //when successful, data is returned here and you can do whatever with it
      this.sectionsTitles = data;
      
      
    }, (err: Error) => {
        //When unsuccessful, this will run
        console.error('Something broke!', err);
        
    });
  }
  //get sections with specific section title
  public getSectionsBySectionTitleId(sections:any,sectionTitleId:number){
    let sectionsWithSectionTitleId=[];
    for(let section of sections){
      if(section.sectionTitleId == sectionTitleId){
        sectionsWithSectionTitleId.push(section);
      }
    }
    return sectionsWithSectionTitleId;
  }
  //add Sub Section 
  public addSubSection(subSectionModel:any){
    const sectionId = this.sectionService.
    findSectionIdByName(subSectionModel.value.Section,this.sections);
    (this.subSectionService.postAddSubSection(new SubSectionModel(0,subSectionModel.value.SubSectionName,
      sectionId)).subscribe(
        async () => {
          this.toastr.success("","Succesful adding new sub section",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
          await new Promise(f => setTimeout(f, 1200));      
          this.route.navigate(['']);
          document.getElementById("addSubSectionButton").click();},
        error => {     
          this.toastr.error("Error while adding section");       
        }))
  }
  addSubSectionForm = new FormGroup({
    SubSectionName:new FormControl('',[Validators.required,Validators.minLength(3)]),
    Section:new FormControl('',[Validators.required])
  }); 
  updateSectionTitleForm = new FormGroup({
    sectionTitleName:new FormControl('',[Validators.required,Validators.minLength(3)]),
    sections:new FormControl('',[Validators.required])
  })
  updateSectionForm = new FormGroup({
    sectionName:new FormControl('',[Validators.required,Validators.minLength(3)]),
    sectionTitle:new FormControl('',[Validators.required,Validators.minLength(3)])
  })
  sectionTitleForm = new FormGroup({
    name:new FormControl('',[Validators.required,Validators.minLength(3)])
  })
  sectionForm = new FormGroup({
    name:new FormControl('',[Validators.required,Validators.minLength(3)]),
    sectionTitle:new FormControl('',[Validators.required])
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
  updateSectionTitle(SectionTitle:any){
    const sectionTitleId = this.sectionTitleService.findSectionIdByName(this.selectedSectionTitle,this.sectionsTitles);
    let sections = [];
    for(let sectionName of SectionTitle.value.sections){
      
      sections.push(this.sectionService.findSectionByName(sectionName,this.sections));
    }
    let updateSectionTitle = new SectionTitleModel(sectionTitleId,SectionTitle.value.sectionTitleName,sections);
    this.sectionTitleService.updateSectionTitle(updateSectionTitle).subscribe(
      async () => {
        this.toastr.success("","Succesful updating section Title",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
        await new Promise(f => setTimeout(f, 1200));      
        this.route.navigate(['']);
        document.getElementById("updateSectionTitleButton").click();
        window.location.reload();
      },
      error => {
        
        this.toastr.error("Error while updating section title");       
      }
    );
    
    
  }
  updateSection(updateSectionModel:any){
    const sectionId = this.sectionService.findSectionIdByName(this.selectedSection,this.sections);
    
    this.sectionService.updateSection(new UpdateSectionModel(sectionId,updateSectionModel.value.sectionName,updateSectionModel.value.sectionTitle)).subscribe(
      async () => {
        this.toastr.success("","Succesful updating section",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
        await new Promise(f => setTimeout(f, 1200));      
        this.route.navigate(['']);
      document.getElementById("updateSectionButton").click();},
      error => {
        
        this.toastr.error("Error while updating section");       
      }
    );

  }
  addSection(section:any){
    let newsection = new SectionModel(2,section.value.name,section.value.sectionTitle);
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
  updateProfile(){
    this.route.navigate(["update-profile"]);
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
  constructor(private httpService: HttpService,
    private subSectionService:SubSectionService, 
    private route: Router,
    private toastr: ToastrService,
    private loginService :LoginService,
    private sectionTitleService:SectionTitleService,
    private sectionService:SectionService,
    ){
      
  }

  registerUser(user: any){
        let usertemp3 = new RegisterUserModel(user.value.username,user.value.email,user.value.password,user.value.passwordConfirm);
        this.httpService.postRegisterUser(usertemp3)
                .subscribe(
                    async (data: any) => {
                      this.receivedUser=data; 
                      this.registred=true;
                      this.toastr.success("","Succesful registration",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
                      await new Promise(f => setTimeout(f, 1200));
                      
                      this.route.navigate(['']);
                    document.getElementById("registerButton").click();},
                    error => {
                      if(error.status==0){
                        this.toastr.error("Connection refused");
                      }
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