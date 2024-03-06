import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {MenubarModule} from 'primeng/menubar';
import {MenuItem} from 'primeng/api';
import {ToastModule} from 'primeng/toast';
import {SidebarModule} from 'primeng/sidebar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PasswordModule } from "primeng/password";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import { MessageService } from 'primeng/api';
import {MenuModule} from 'primeng/menu';
import { BlockUIModule } from 'primeng/blockui';
import { UsersListComponent } from './components/users-list/users-list.component';
import { ToolbarModule } from 'primeng/toolbar';
import { TableModule } from 'primeng/table';
import {ConfirmationService} from 'primeng/api';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';
import { DialogModule } from 'primeng/dialog';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CardModule } from 'primeng/card';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { InputNumberModule } from 'primeng/inputnumber';
import { TodosListComponent } from './components/todos-list/todos-list.component';
import { TagModule } from 'primeng/tag';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { CalendarModule } from 'primeng/calendar';
import { InputTextModule } from 'primeng/inputtext';

@NgModule({
  declarations: [
    AppComponent,
    UsersListComponent,
    TodosListComponent
  ],
  imports: [
    ReactiveFormsModule,
    FormsModule,
    MenubarModule,
    ToastModule,
    PasswordModule,
    BrowserModule,
    PasswordModule,
    ButtonModule,
    AppRoutingModule,
    HttpClientModule,
    ProgressSpinnerModule,
    BrowserAnimationsModule,
    MenuModule,
    SidebarModule,
    BlockUIModule,
    ToolbarModule,
    MessagesModule,
    TableModule,
    DialogModule,
    ConfirmDialogModule,
    MessageModule,
    CardModule,
    InputTextareaModule,
    InputNumberModule,
    TagModule,
    DropdownModule,
    CheckboxModule,
    CalendarModule,
    InputTextModule
  ],
  providers: [MessageService,    ConfirmationService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
