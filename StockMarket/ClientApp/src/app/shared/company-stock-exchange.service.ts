import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class CompanyStockExchangeService {

  constructor(private http: HttpClient) { }

  postCompanyStockExchange(formData) {
    return this.http.post(environment.apiBaseURI + "CompanyStockExchanges", formData);
  }

  getCompanyStockExchange() {
    return this.http.get(environment.apiBaseURI + "CompanyStockExchanges");
  }

  deleteCompanyStockExchange(companyId, stockExchangeId) {
    return this.http.delete(environment.apiBaseURI + "CompanyStockExchanges/" + companyId + "/" + stockExchangeId);
  }
}
