import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CompaniesComponent } from './companies/companies.component';
import { StockExchangesComponent } from './stock-exchanges/stock-exchanges.component';
import { CompanyStockExchangesComponent } from './company-stock-exchanges/company-stock-exchanges.component';
import { IpoDetailsComponent } from './ipo-details/ipo-details.component';
import { UsersComponent } from './users/users.component';
import { RegistrationsComponent } from './users/registrations/registrations.component';
import { LoginComponent } from './users/login/login.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { IposComponent } from './ipos/ipos.component';
import { CompanyComponent } from './company/company.component';
import { ProfileComponent } from './profile/profile.component';
import { UpdateComponent } from './profile/update/update.component';
import { PasswordComponent } from './profile/password/password.component';

import { UserService } from './shared/user.service';
import { AuthGuard } from './auth/auth.guard';
import { AuthInterceptor } from './auth/auth.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CompaniesComponent,
    StockExchangesComponent,
    CompanyStockExchangesComponent,
    IpoDetailsComponent,
    UsersComponent,
    RegistrationsComponent,
    LoginComponent,
    AdminPanelComponent,
    ForbiddenComponent,
    IposComponent,
    CompanyComponent,
    ProfileComponent,
    UpdateComponent,
    PasswordComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({ progressBar: true }),
    RouterModule.forRoot([
      {
        path: 'home', component: HomeComponent, canActivate: [AuthGuard], children: [
          { path: 'company', component: CompanyComponent },
          { path: 'ipos', component: IposComponent },
          {
            path: 'profile', component: ProfileComponent,
            children: [
              { path: 'update', component: UpdateComponent },
              { path: 'password', component: PasswordComponent }
            ]
          }
        ]
      },

      { path: 'forbidden', component: ForbiddenComponent, canActivate: [AuthGuard] },
      {
        path: 'adminpanel', component: AdminPanelComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin'] }, children: [
          { path: 'companies', component: CompaniesComponent },
          { path: 'stockexchanges', component: StockExchangesComponent },
          { path: 'cse', component: CompanyStockExchangesComponent },
          { path: 'ipodetails', component: IpoDetailsComponent }
        ]
      },
      { path: '', redirectTo: '/user/login', pathMatch: 'full' },
      {
        path: 'user', component: UsersComponent,
        children: [
          { path: 'registration', component: RegistrationsComponent },
          { path: 'login', component: LoginComponent }
        ]
      }
    ])
  ],
  providers: [UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
