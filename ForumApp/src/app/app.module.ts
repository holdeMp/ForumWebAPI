import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {Routes, RouterModule} from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule  }   from '@angular/forms';
import { EmailconfirmationComponent }   from './emailconfirmation/emailconfirmation.component';
import { NotFoundComponent }   from './not-found/not-found.component';
import { HttpClientModule, HTTP_INTERCEPTORS }   from '@angular/common/http';
import { LoginformComponent } from './loginform/loginform.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AuthInterceptor } from './auth.interceptor';;
import { MatFormFieldModule } from '@angular/material/form-field';
import {  MatOptionModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import {  MatSelectModule } from '@angular/material/select';
import { SectionTitleComponent } from './section-title/section-title.component';
import { MainComponentComponent } from './main-component/main-component.component';
// определение маршрутов
const appRoutes: Routes =[
  {path:'',component:MainComponentComponent},
  { path: 'ConfirmEmail', component: EmailconfirmationComponent},
  { path:'login',component:LoginformComponent},
  { path:'sectiontitle/:id',component:SectionTitleComponent},
  { path: '*', component: NotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginformComponent,
    SectionTitleComponent,
    MainComponentComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    MatFormFieldModule,
    MatOptionModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatSelectModule,
    MatInputModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
  ],
  providers: [            
    // Http Interceptor(s) -  adds with Client Credentials
    [
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ],
],
  bootstrap: [AppComponent]
})
export class AppModule { }
