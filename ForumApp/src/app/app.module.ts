import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {Routes, RouterModule} from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule }   from '@angular/forms';
import { EmailconfirmationComponent }   from './emailconfirmation/emailconfirmation.component';
import { NotFoundComponent }   from './not-found/not-found.component';
import { HttpClientModule }   from '@angular/common/http';


// определение маршрутов
const appRoutes: Routes =[
  { path: 'email_confirm', component: EmailconfirmationComponent},
  { path: '*', component: NotFoundComponent }
];

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    RouterModule.forRoot(appRoutes),
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
