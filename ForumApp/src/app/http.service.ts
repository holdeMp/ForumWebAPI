import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RegisterUserModel} from './RegisterUserModel';
import { LoginUserModel } from './loginUser';
   
@Injectable()
export class HttpService{
   
    constructor(private http: HttpClient){ }
 
    postRegisterUser(user: RegisterUserModel){
          
        const body = {username: user.username, email: user.email,password:user.password,passwordConfirm:user.passwordConfirm};
        return this.http.post('https://localhost:44381/api/register', body); 
    }
    postLogin(loginUser :LoginUserModel){
        const body = {usernameOrEmail: loginUser.usernameOrEmail, password: loginUser.password,rememberMe:loginUser.rememberMe};
        return this.http.post('https://localhost:44381/api/login', body);
    }
}