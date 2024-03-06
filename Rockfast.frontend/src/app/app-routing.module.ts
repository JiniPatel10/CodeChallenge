import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersListComponent } from './components/users-list/users-list.component';
import { TodosListComponent } from './components/todos-list/todos-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'users',
    pathMatch: 'full'
  },

  { path: 'users', component: UsersListComponent},
  { path: 'todos/:id', component: TodosListComponent },
  { path: '**', redirectTo: '/',pathMatch: 'full' },
   { path: '', redirectTo: '/users', pathMatch: 'full' }, // Default route
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
