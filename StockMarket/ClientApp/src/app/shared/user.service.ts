import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.required, Validators.email]],
    PhoneNumber: ['', Validators.required],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, { validator: this.comparePasswords })
  });

  comparePasswords(fb: FormGroup) {
    let confirmPwdCtrl = fb.get('ConfirmPassword');
    if (confirmPwdCtrl.errors == null || 'passwordMismatch' in confirmPwdCtrl.errors) {
      if (fb.get('Password').value != confirmPwdCtrl.value)
        confirmPwdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPwdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      PhoneNumber: this.formModel.value.PhoneNumber,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(environment.apiBaseURI + "Users/Register", body);
  }

  login(formData) {
    return this.http.post(environment.apiBaseURI + "Users/Login", formData);
  }

  getUserProfile() {
    return this.http.get(environment.apiBaseURI + "UserProfile");
  }

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payload = JSON.parse(window.atob(localStorage.getItem('token').split('.')[1]));
    var userRole = payload.role;
    allowedRoles.forEach(role => {
      if (userRole == role) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }

  updateProfile(userName, formData) {
    return this.http.put(environment.apiBaseURI + "Users/" + userName, formData);
  }

  changePassword(userName, formData) {
    return this.http.post(environment.apiBaseURI + "Users/pwd/" + userName, formData);
  }

  deleteProfile(userName) {
    return this.http.delete(environment.apiBaseURI + "Users/" + userName);
  }
}
