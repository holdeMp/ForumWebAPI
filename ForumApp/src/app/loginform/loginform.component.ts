import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { LoginUserModel } from '../loginUser';
import { HttpService} from '../http.service';
import { LoginService} from '../login.service';
import { Router } from '@angular/router';
import { passwordValidator } from '../passvalidator.directive';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-loginform',
  templateUrl: './loginform.component.html',
  styleUrls: ['./loginform.component.css'],
  providers: [HttpService]
})
export class LoginformComponent implements OnInit {
  done: boolean = false;
  receivedUser: LoginUserModel | undefined;
  constructor(private httpService: HttpService, private route: Router,private toastr: ToastrService,private loginService: LoginService) { }

  ngOnInit(): void {
  }
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
  getLoginService(){
    return this.loginService;
  }
  login(user:any){
    let userLoginModel = new LoginUserModel(user.value.loginUsername,user.value.loginPassword,user.value.rememberMe);
    this.httpService.postLogin(userLoginModel) .subscribe(
     async (data: any) => {
                      this.receivedUser=data; 
                      this.done=true;
                      this.toastr.success("","Succesful login",{timeOut:2000,progressBar:true,progressAnimation:'increasing'})
                      await new Promise(f => setTimeout(f, 1200));
                      this.route.navigate(['']);
                     },
      error => {
        if(error!==undefined){
          this.toastr.error(error.error[0].description);
          console.log(error.error[0].description);
        }                
      }
  );
}
}
