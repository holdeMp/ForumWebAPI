import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {Routes, RouterModule} from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule  }   from '@angular/forms';
import { EmailconfirmationComponent }   from './components/emailconfirmation/emailconfirmation.component';
import { NotFoundComponent }   from './components/not-found/not-found.component';
import { HttpClientModule, HTTP_INTERCEPTORS }   from '@angular/common/http';
import { LoginformComponent } from './components/loginform/loginform.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AuthInterceptor } from './auth.interceptor';;
import { MatFormFieldModule } from '@angular/material/form-field';
import {  MatOptionModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import {  MatSelectModule } from '@angular/material/select';
import { SectionTitleComponent } from './components/section-title/section-title.component';
import { MainComponentComponent } from './components/main-component/main-component.component';
import { SubsectionComponentComponent } from './components/subsection-component/subsection-component.component';
import { ThemeComponentsComponent } from './components/theme-components/theme-components.component';
import { AddthemecomponentComponent } from './components/addthemecomponent/addthemecomponent.component';
const subSectionRoutes: Routes = [{ path: 'subsection/:id', 
component: ThemeComponentsComponent}];
const sectionRoutes: Routes = [
  { path: 'section/:id', component: SubsectionComponentComponent,children:subSectionRoutes}
];

//определение маршрутов
const appRoutes: Routes =[
  { path:'',component:MainComponentComponent},
  { path: 'ConfirmEmail', component: EmailconfirmationComponent},
  { path:'login',component:LoginformComponent},
  { path:'sectiontitle/:id',component:SectionTitleComponent,children:sectionRoutes},
  { path:'sectiontitle/:id/section/:id',component:SubsectionComponentComponent},
  { path:'section/:id',component:SubsectionComponentComponent,children:subSectionRoutes},
  { path:'addtheme/:id',component:AddthemecomponentComponent},
  { path:'subsection/:id',component:ThemeComponentsComponent},
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginformComponent,
    SectionTitleComponent,
    MainComponentComponent,
    SubsectionComponentComponent,
    ThemeComponentsComponent,
    AddthemecomponentComponent,
    
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
