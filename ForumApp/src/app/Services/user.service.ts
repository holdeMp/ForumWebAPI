import { HttpClient } from '@angular/common/http';
import {Injectable} from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({'providedIn':'root'})

export class UserService
{
    constructor(private httpClient: HttpClient) 
    {

    }

    getImage(imageUrl: string): Observable<Blob> 
    {
        return this.httpClient.get(imageUrl, { responseType: 'blob' });
    }
}