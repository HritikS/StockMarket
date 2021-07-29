import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-registrations',
  templateUrl: './registrations.component.html',
  styleUrls: ['./registrations.component.css']
})

export class RegistrationsComponent implements OnInit {

  constructor(public service: UserService, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
    this.service.formModel.reset();
  }

  onSubmit() {
    this.service.register().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.service.formModel.reset();
          this.toastr.success('Please check your email for the confirmation link!', 'Registered Successfully');
          this.router.navigateByUrl('/user/login');
        }
        else {
          res.errors.forEach(error => {
            switch (error.code) {
              case 'DuplicateUserName':
                this.toastr.error('Username is alreay taken', 'Registration failed');
                break;
              default:
                this.toastr.error(error.description, 'Registration failed');
                break;
            }
          });
        }
      },
      (err: any) => { console.log(err); },
    );
  }

}
