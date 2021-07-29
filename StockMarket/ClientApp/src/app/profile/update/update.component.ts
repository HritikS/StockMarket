import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../shared/user.service';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    PhoneNumber: ['', Validators.required]
  });

  constructor(public service: UserService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
    this.service.getUserProfile().subscribe(
      (res: any) => {
        this.formModel.controls['UserName'].setValue(res.userName);
        this.formModel.controls['Email'].setValue(res.email);
        this.formModel.controls['PhoneNumber'].setValue(res.phoneNumber);
      },
      (err: any) => { this.toastr.error(err, 'Unable to get the User'); }
    );
  }

  onSubmit() {
    this.service.updateProfile(this.formModel.value.UserName, this.formModel.value).subscribe(
      (res: any) => {
        if (res.succeeded)
          this.toastr.success('Your profile has been updated.', 'Success');
        else {
          res.errors.forEach(error => {
            console.log(error);
            this.toastr.error(error.code, error.description);
          });
        }
      },
      (err: any) => {
        if (err.status == 404) {
          this.toastr.success('Your profile has been successfully updated', 'Please confirm your new Email');
          localStorage.removeItem('token');
          this.router.navigateByUrl('/user/login');
        }
        else {
          console.log(err);
          this.toastr.error(err, 'Error');
        }
      }
    );
  }

  onDelete(userName) {
    if (confirm("Your profile will be DELETED!")) {
      this.service.deleteProfile(userName).subscribe(
        (res: any) => {
          localStorage.removeItem('token');
          this.toastr.success('Your profile has been successfully deleted!', 'Sorry to see you go');
          this.router.navigateByUrl('/user/registration');
        },
        (err: any) => { console.log(err); this.toastr.error(err, 'Error'); }
      );
    }
  }

}
