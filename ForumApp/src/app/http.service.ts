import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {RegisterUserModel} from './RegisterUserModel';
import { LoginUserModel } from './loginUser';
import { map, tap } from 'rxjs/operators';
import { LoginService } from './login.service';
import { SectionModel } from './models/sectionModel';
import { SectionTitleModel } from './models/SectionTitleModel';
import { Observable } from 'rxjs';
@Injectable()
export class HttpService{
    isLoggedIn: boolean = false;
    headerDict = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Allow-Headers': 'Content-Type',
    }
    constructor(private http: HttpClient,private loginService:LoginService){ }
    // getSectionsByTitleId(id:number){
      
    //   return this.http.get('https://localhost:44381/section/'+id,{headers:this.headerDict,withCredentials:true});
    // }
    getSections(){
      return this.http.get('https://localhost:44381/section',{headers:this.headerDict,withCredentials:true});
    }
    postLogout(){
      return this.http.post('https://localhost:44381/logout',null,{headers:this.headerDict,withCredentials:true} );
    }
    postAddSectionTitle(section: SectionTitleModel)
    {
      const headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
                                                                                                                                                                                       
      const body = {id:section.id,name:section.name,sectionsIds:section.sections};
      return this.http.post('https://localhost:44381/sectiontitle', body,{headers:headerDict,withCredentials:true} ); 
    }
    postAddSection(section: SectionModel)
    {
      const headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
                                                                                                                                                                                       
      const body = {name:section.name,sectionTitle:section.sectionTitle};
      return this.http.post('https://localhost:44381/section', body,{headers:headerDict,withCredentials:true} ); 
    }
    postRegisterUser(user: RegisterUserModel){
      const headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
        const body = {username: user.username, email: user.email,password:user.password,passwordConfirm:user.passwordConfirm};
        return this.http.post('https://localhost:44381/api/register', body , {headers:headerDict,withCredentials: true}); 
    }
    postLogin(loginUser :LoginUserModel){
      const headerDict = {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Access-Control-Allow-Headers': 'Content-Type',
      }
        const body = {usernameOrEmail: loginUser.usernameOrEmail, password: loginUser.password,rememberMe:loginUser.rememberMe};
        return this.http.post<any>('https://localhost:44381/api/login', body ,{headers:headerDict,withCredentials: true}).pipe(
            tap(user => {
            if (user ) {
              if (loginUser.rememberMe) {
                this.resetcredentials();
                //your logged  out when you click logout
                localStorage.setItem('currentUser', JSON.stringify(user));
                localStorage.setItem('token', user.token);
                localStorage.setItem('rememberCurrentUser', 'true');
              } else {
                //your logged  out when page/ browser is closed
                sessionStorage.setItem('token', user.token);
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
        localStorage.removeItem('token');
        sessionStorage.removeItem('token');
        sessionStorage.removeItem('currentUser');
        this.loginService.userChange.next(null);
      }
}
