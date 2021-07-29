import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IpoDetailService {

  constructor(private http: HttpClient) { }

  postIPODetail(formData) {
    return this.http.post(environment.apiBaseURI + "IPODetails", formData);
  }

  getIPODetail() {
    return this.http.get(environment.apiBaseURI + "IPODetails");
  }

  putIPODetail(companyId, stockExchangeId, formData) {
    return this.http.put(environment.apiBaseURI + "IPODetails/" + companyId + "/" + stockExchangeId, formData);
  }

  deleteIPODetail(companyId, stockExchangeId) {
    return this.http.delete(environment.apiBaseURI + "IPODetails/" + companyId + "/" + stockExchangeId);
  }

  getSortedIPODetail() {
    return this.http.get(environment.apiBaseURI + "IPODetails/sorted");
  }
}
