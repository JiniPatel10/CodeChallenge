import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Todo } from '../models/todo.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  constructor(private http: HttpClient) { }
  getByUserId(userId: number): Observable<Todo[]> {
    return this.http.get<Todo[]>(`${environment.server}todos/${userId}`);
}
save(todo: Todo): Observable<Todo> {
  return this.http.post<Todo>(`${environment.server}todos`, todo);
}
update(todo: Todo): Observable<Todo> {
  return this.http.put<Todo>(`${environment.server}todos`, todo);
}

delete(id: number): Observable<any> {
  return this.http.delete<any>(`${environment.server}todos/${id}`);
}

}
