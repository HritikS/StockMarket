import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private http: HttpClient) { }

  postCompany(formData) {
    return this.http.post(environment.apiBaseURI + "Companies", formData); 
  }

  getCompanyList() {
    return this.http.get(environment.apiBaseURI + "Companies");
  }

  putCompany(formData) {
    return this.http.put(environment.apiBaseURI + "Companies/" + formData.id, formData);
  }

  deleteCompany(id) {
    return this.http.delete(environment.apiBaseURI + "Companies/" + id);
  }

  getCompanyListFull() {
    return this.http.get(environment.apiBaseURI + "Companies/full");
  }

  getCompanyByPattern(pat) {
    return this.http.get(environment.apiBaseURI + "Companies/pattern/" + pat);
  }
}
