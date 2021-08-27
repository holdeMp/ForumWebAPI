import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {Routes, RouterModule} from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule  }   from '@angular/forms';
import { EmailconfirmationComponent }   from './emailconfirmation/emailconfirmation.component';
import { NotFoundComponent }   from './not-found/not-found.component';
import { HttpClientModule }   from '@angular/common/http';
import { LoginformComponent } from './loginform/loginform.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';

// определение маршрутов
const appRoutes: Routes =[
  { path: 'ConfirmEmail', component: EmailconfirmationComponent},
  { path:'login',component:LoginformComponent},
  { path: '*', component: NotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginformComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
