import { ChangeDetectorRef, Component } from '@angular/core';
import { Todo } from '../../models/todo.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { ConfirmationService } from 'primeng/api';
import { NotificationService } from '../../services/notification.service';
import { TodoService } from '../../services/todo.service';
import { ActivatedRoute, Router } from '@angular/router';

interface selectOptions {
  key: string;
  value: string;
}

@Component({
  selector: 'app-todos-list',
  templateUrl: './todos-list.component.html',
  styleUrl: './todos-list.component.scss'
})

export class TodosListComponent {
  todo: Todo = new Todo();
  todosList: Todo[] = [];
  loading: boolean = true;
  newDialog: boolean = false;
  isNew: boolean = false;
  todoForm: FormGroup;
  userId: number;
  usersOptions?: selectOptions[];
  today?: Date;

  constructor(private fb: FormBuilder,
    private todoService: TodoService, private _route: ActivatedRoute,
    private confirmationService: ConfirmationService, private userService: UserService,
    private ref: ChangeDetectorRef, private router: Router,
    private _notificationService: NotificationService) {
    this._route.params.subscribe(params => {
      let paramId = params['id'];
      if (typeof paramId != 'undefined') {
        this.userId = paramId;
      }
    });
  }

  ngOnInit(): void {
    this.createForm();
    this.loadTodos();
    this.loadUsers();
    this.today = new Date();
  }


  addNewTodo() {
    this.createForm();
    this.isNew = true
    this.newDialog = true;
    this.todo = new Todo();
  }

  //load all todos
  loadTodos() {
    this.todoService.getByUserId(this.userId).subscribe((data: any) => {
      if (data == null) {
        this.todosList = [];
      }
      else {
        this.todosList = data;
      }
      this.loading = false;
    });


  }

  // edit todo
  onEdit(todo: Todo): void {
    this.isNew = false;
    this.todo = todo;
    if (this.todo.dateCompleted) this.todo.dateCompleted = new Date(this.todo.dateCompleted);
    this.newDialog = true;
  }

  // delete todo
  onDelete(todo: Todo) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to delete this todo?',
      accept: () => {
        this.loading = true;
        this.todoService.delete(todo.id).subscribe({
          next: (response) => {
            this.loading = false;
            this.todosList = this.todosList.filter(x => x.id != todo.id);
            this._notificationService.showMessage('success', true, `Todo deleted successfully`, '');
          },
          error: (error: any) => {
            this.loading = false;
            this.loadTodos();
            if (error.status == 422) {
              this._notificationService.showMessage('error', true, `${error.error}`, '');
            } else {
              this._notificationService.showMessage('error', true, `${error.status}
        - ${error.statusText} - ${error.error}`, '');
            }
          }
        });
      }
    });

  }
// crete forma and set validation
  createForm() {
    this.todoForm = this.fb.group({
      name: [, [Validators.required]],
      complete: [false],
      dateCompleted: [null],
      user: [, [Validators.required]]
    })
  }
//save todo
  submiTodo() {
    this.loading = true;
    if (this.todoForm.invalid || (this.todo.complete && !this.todo.dateCompleted)) {
      this.loading = false;
      this._notificationService.showMessage('error', true, `Please validate your fields`, '');
      return;
    }
    this.createTodo();
  }

  // hide new todo form dialog
  hideDialog(): void {
    this.newDialog = false;
    this.createForm();
    this.loadTodos();
  }

  // add update todo
  createTodo() {
    if (this.isNew) {
      this.addTodo();
    } else {
      this.updateTodo();
    }

  }
  // add todo method
  addTodo() {
    this.todoService.save(this.todo).subscribe({
      next: (response) => {
        this.loading = false;
        this.newDialog = false;
        this.createForm();
        this.loadTodos();
        this._notificationService.showMessage('success', true, `New todo added successfully`, '');

      },
      error: (error: any) => {
        this.loading = false;
        this.newDialog = false;
        this.loadTodos();
        if (error.status == 422) {
          this._notificationService.showMessage('error', true, `${error.error}`, '');
        } else {
          this._notificationService.showMessage('error', true, `${error.status}
    - ${error.statusText} - ${error.error}`, '');
        }
      }
    });
  }
//update todo method
  updateTodo() {
    this.todoService.update(this.todo).subscribe({
      next: (response) => {
        this.loading = false;
        this.newDialog = false;
        this.createForm();
        this.loadTodos();
        this._notificationService.showMessage('success', true, `Todo updated successfully`, '');

      },
      error: (error: any) => {
        this.loading = false;
        this.newDialog = false;
        this.loadTodos();
        if (error.status == 422) {
          this._notificationService.showMessage('error', true, `${error.error}`, '');
        } else {
          this._notificationService.showMessage('error', true, `${error.status}
    - ${error.statusText} - ${error.error}`, '');
        }
      }
    });
  }

  // get severity color base on complete green for yes, red for no
  getSeverity(isComplete) {
    if (isComplete)
      return "success"
    else return "danger"
  }

//load users
  loadUsers() {
    this.userService.getUsers().subscribe((data: any) => {
      if (data == null) {
        this.usersOptions = [];
      }
      else {
        this.usersOptions = this.formatOptions(data, "name", "id");
      }

    });


  }
  // method to create key value pair for user dropdown
  private formatOptions(data: any[], key: string, value: string): selectOptions[] {
    let options: selectOptions[] = [];

    data.forEach(i => {
      let option: selectOptions = {
        key: i[key],
        value: i[value]
      }
      options.push(option);
    });

    return options;
  }

  // set datecompleted as null if complete is deselected
  isCompletedChanged(isComplete) {
    if (!isComplete) {
      this.todo.dateCompleted = null;
    }
  }

}
