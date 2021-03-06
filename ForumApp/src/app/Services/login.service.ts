import {Injectable} from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { User } from '../models/user';

@Injectable({'providedIn':'root'})
export class LoginService{

    private rememberMe: boolean = false;
    public user: User;
    public userChange: Subject<User> = new Subject<User>();
    public currentUser: Observable<User>;

    constructor(){
      this.rememberMe = localStorage.getItem('rememberCurrentUser') == 'true' ? true : false;

      if ((this.rememberMe == true)) {
        this.userChange = new BehaviorSubject<User>(
          JSON.parse(localStorage.getItem('currentUser'))
        );
      } 
      else 
      {
        this.userChange = new BehaviorSubject<User>(
          JSON.parse(sessionStorage.getItem('currentUser'))
        );
      }

      this.currentUser = this.userChange.asObservable();
        this.userChange.subscribe(value=>{       
          this.user = value;       
        });
    }

    public getToken(){
      return this.user.token;
    }

    login(user:User){
      this.userChange.next(user);    
    }
  
    logout(){
      this.userChange.next(null)
    }      
}