import { HttpHeaders } from '@angular/common/http';
import {Injectable} from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { User } from './user';
@Injectable({'providedIn':'root'})
export class LoginService{
    httpOptions = null;
    rememberMe: boolean = false;
    public user: User;
    public islogged :boolean;
    public userChange: Subject<User> = new Subject<User>();
    public _isLoggedIn:Subject<boolean> = new Subject<boolean>();
    public currentUser: Observable<User>;
    constructor(){
      this.rememberMe = localStorage.getItem('rememberCurrentUser') == 'true' ? true : false;

    if ((this.rememberMe == true)) {
      this.userChange = new BehaviorSubject<User>(
        JSON.parse(localStorage.getItem('currentUser'))
      );
    } else {
      this.userChange = new BehaviorSubject<User>(
        JSON.parse(sessionStorage.getItem('currentUser'))
      );
    }

    this.currentUser = this.userChange.asObservable();

    this.httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
      this.userChange.subscribe(value=>{
        console.log("userChange.subscribe",value)
        
        this.user = value;
        this.islogged = true;
      });
      this._isLoggedIn.subscribe(logged=>{
        console.log("logged:",logged)
        this.islogged = logged;
      })
    }
  
    login(user:User){
      this.userChange.next(user);
      this._isLoggedIn.next(true);
    }
  
    logout(){
      this.userChange.next(null)
      this._isLoggedIn.next(false);
    }      
    get isLoggedIn() {
      return this._isLoggedIn.asObservable();
    }
}