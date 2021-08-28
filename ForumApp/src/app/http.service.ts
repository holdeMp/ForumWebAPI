import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RegisterUserModel} from './RegisterUserModel';
import { LoginUserModel } from './loginUser';
import { tap } from 'rxjs/operators';
import { LoginService } from './login.service';
@Injectable()
export class HttpService{
    isLoggedIn: boolean = false;
    constructor(private http: HttpClient,private loginService:LoginService){ }
 
    postRegisterUser(user: RegisterUserModel){
          
        const body = {username: user.username, email: user.email,password:user.password,passwordConfirm:user.passwordConfirm};
        return this.http.post('https://localhost:44381/api/register', body); 
    }
    postLogin(loginUser :LoginUserModel){
        const body = {usernameOrEmail: loginUser.usernameOrEmail, password: loginUser.password,rememberMe:loginUser.rememberMe};
        return this.http.post<any>('https://localhost:44381/api/login', body).pipe(
            tap(user => {
            if (user ) {
              if (loginUser.rememberMe) {
                this.resetcredentials();
                //your logged  out when you click logout
                localStorage.setItem('currentUser', JSON.stringify(user));
                localStorage.setItem('rememberCurrentUser', 'true');
              } else {
                //your logged  out when page/ browser is closed
                sessionStorage.setItem('currentUser', JSON.stringify(user));
              }
              // login successful if there's a jwt token in the response
              this.isLoggedIn = true;
              this.loginService.userChange.next(user);
              return true;
            } else {
              return false;
            }
          })
        );;
    }
    resetcredentials() {
        //clear all localstorages
        localStorage.removeItem('rememberCurrentUser');
        localStorage.removeItem('currentUser');
        sessionStorage.removeItem('currentUser');
        this.loginService.userChange.next(null);
      }
}
