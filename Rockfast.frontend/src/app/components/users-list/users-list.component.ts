import { Component } from '@angular/core';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { NotificationService } from '../../services/notification.service';
import { ConfirmationService } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss'
})
export class UsersListComponent {
  user: User = new User();
  usersList: User[] = [];
  loading: boolean = true;
  copiedUser: { [s: string]: User; } = {};
  newDialog: boolean = false;
  userForm: FormGroup;
  initialUserName: string;
  constructor(
    private userService: UserService, private confirmationService: ConfirmationService,
    private fb: FormBuilder,
    private _notificationService: NotificationService) {
  }

  ngOnInit(): void {
    this.createForm();
    this.loadUsers();
  }

  // load users
  loadUsers() {
    this.userService.getUsers().subscribe((data: any) => {
      if (data == null) {
        this.usersList = [];
      }
      else {
        this.usersList = data;
      }
      this.loading = false;
    });


  }
  // add new user
  addNew() {
    this.createForm();
    this.newDialog = true;
    this.user = new User();
  }

  // user edit initialize 
  onRowEditInit(user) {
    this.initialUserName = user.name;
    this.copiedUser[user.id] = { ...user };
  }

  // user edit saved
  onRowEditSave(user) {
    if (!user.name) {
      this._notificationService.showMessage('error', true, `User name is required`, '');
      return;
    }
    this.userService.update(user).subscribe({
      next: (response) => {
        this.loading = false;
        this.loadUsers();
        this._notificationService.showMessage('success', true, `User updated successfully`, '');

      },
      error: (error: any) => {
        this.loading = false;
        this.loadUsers();
        if (error.status == 422) {
          this._notificationService.showMessage('error', true, `${error.error}`, '');
        } else {
          this._notificationService.showMessage('error', true, `${error.status}
  - ${error.statusText} - ${error.error}`, '');
        }
      }
    });
  }

  // user edit is cancelled
  onRowEditCancel(user, rowIndex) {
    this.copiedUser[rowIndex] = this.copiedUser[user.id];
    delete this.copiedUser[user.id];
    this.usersList[rowIndex].name = this.initialUserName;
  }

  // user delete
  onRowDelete(user, rowIndex) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to delete this user?',
      accept: () => {
        this.loading = true;
        this.userService.delete(user.id).subscribe({
          next: (response) => {
            this.loading = false;
            this.loadUsers();
            this._notificationService.showMessage('success', true, `User deleted successfully`, '');
          },
          error: (error: any) => {
            this.loading = false;
            this.loadUsers();
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

  // create form and set validation
  createForm() {
    this.userForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]]
    })
  }

  // save user
  submitUser() {
    this.loading = true;
    if (this.userForm.invalid) {
      this.loading = false;
      return;
    }
    this.createUser();
  }


  hideDialog(): void {
    this.newDialog = false;
    this.createForm();
  }

  // create new user
  createUser() {
    this.userService.save(this.user).subscribe({
      next: (response) => {
        this.loading = false;
        this.newDialog = false;
        this.createForm();
        this.loadUsers();
        this._notificationService.showMessage('success', true, `New user added successfully`, '');

      },
      error: (error: any) => {
        this.loading = false;
        this.newDialog = false;
        this.loadUsers();
        if (error.status == 422) {
          this._notificationService.showMessage('error', true, `${error.error}`, '');
        } else {
          this._notificationService.showMessage('error', true, `${error.status}
  - ${error.statusText} - ${error.error}`, '');
        }
      }
    });
  }
}
