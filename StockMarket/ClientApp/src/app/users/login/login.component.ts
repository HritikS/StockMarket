import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel = {
    UserName: '',
    Password: ''
  }

  constructor(private service: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit() {
    if (localStorage.getItem('token'))
      this.router.navigateByUrl('/home/company');
  }

  onSubmit(form: NgForm) {
    this.service.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/home/company');
      },
      (err: any) => {
        if (err.status == 404)
          this.toastr.error('Email is not verified', 'Authentication failed');
        else if (err.status == 400)
          this.toastr.error('Incorrect Username or Password', 'Authentication failed');
        else
          console.log(err);
      }
    );
  }

}
