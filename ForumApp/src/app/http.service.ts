import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {RegisterUserModel} from './user';
   
@Injectable()
export class HttpService{
   
    constructor(private http: HttpClient){ }
 
    postRegisterUser(user: RegisterUserModel){
          
        const body = {username: user.username, email: user.email,password:user.password,passwordConfirm:user.passwordConfirm};
        return this.http.post('https://localhost:44381/api/register', body); 
    }
}