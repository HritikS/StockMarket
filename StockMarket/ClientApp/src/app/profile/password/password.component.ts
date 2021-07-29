import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.css']
})
export class PasswordComponent implements OnInit {

  userDetails;

  formModel = this.fb.group({
    CurrentPassword: ['', [Validators.required, Validators.minLength(4)]],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.service.comparePasswords })
  });

  constructor(public service: UserService, private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit() {
    this.formModel.reset();
    this.service.getUserProfile().subscribe(
      (res: any) => { this.userDetails = res; },
      (err: any) => { this.toastr.error(err, 'Unable to get the User'); }
    );
  }

  onSubmit() {
    var body = {
      CurrentPassword: this.formModel.value.CurrentPassword,
      NewPassword: this.formModel.value.Passwords.Password
    }
    this.service.changePassword(this.userDetails.userName, body).subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.formModel.reset();
          this.toastr.success('Your Password has been changed', 'Success!');
        }
        else {
          res.errors.forEach(error => {
            console.log(error);
            this.toastr.error(error.code, error.description);
          });
        }
      },
      (err: any) => { console.log(err); this.toastr.error(err, 'Error'); }
    );
  }

}
