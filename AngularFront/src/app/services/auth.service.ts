import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Item } from '../models/item';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  public register(user: User): Observable<any> {
    return this.http.post<any>(
      'https://localhost:55001/api/auth/register',
      user
    );
  }

  public login(user: User): Observable<string> {
    return this.http.post('https://localhost:55001/api/auth/login', user, {
      responseType: 'text',
    });
  }

  public getMe(): Observable<string> {
    return this.http.get('https://localhost:55001/api/auth', {
      responseType: 'text',
    });
  }

  public getItems(): Observable<Array<Item>> {
    return this.http.get<Array<Item>>('https://localhost:55001/api/items');
  }
}
