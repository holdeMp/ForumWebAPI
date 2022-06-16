import { HttpClient } from '@angular/common/http';
import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({'providedIn':'root'})

export class UserService
{

    private user : Observable<any>;
    constructor(private httpClient: HttpClient) 
    {

    }

    getImage(imageUrl: string): Observable<Blob> 
    {
        return this.httpClient.get(imageUrl, { responseType: 'blob' });
    }

    getUserByUserId(authorId: string):Observable<any>{
        this.user = this.httpClient.get(
            'https://localhost:44381/api/Users?id='
            +authorId,
            {withCredentials:true});
        return this.user;
      }
}