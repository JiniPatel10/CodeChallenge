import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  getUsers(): Observable<any> {
    return this.http.get<any>(`${environment.server}user/getallusers`);
}
save(user: User): Observable<User> {
  return this.http.post<User>(`${environment.server}user`, user);
}
update(user: User): Observable<User> {
  return this.http.put<User>(`${environment.server}user`, user);
}

delete(id: number): Observable<any> {
  return this.http.delete<any>(`${environment.server}user/${id}`);
}

}
