import { Component } from '@angular/core';
import {RegisterUserModel} from './user';
import { HttpService} from './http.service';
import { Router } from '@angular/router';
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
    constructor(private httpService: HttpService, private route: Router){
      
    }
    registerUser(user: RegisterUserModel){
        this.httpService.postRegisterUser(user)
                .subscribe(
                    (data: any) => {this.receivedUser=data; this.done=true;this.route.navigate(['login']);},
                    error => {
                      alert(error);
                      console.log(error);}
                );
    }
}