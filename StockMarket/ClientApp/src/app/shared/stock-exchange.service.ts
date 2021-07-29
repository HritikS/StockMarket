import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StockExchangeService {

  constructor(private http: HttpClient) { }

  postStockExchange(formData) {
    return this.http.post(environment.apiBaseURI + "StockExchanges", formData);
  }

  getStockExchangeList() {
    return this.http.get(environment.apiBaseURI + "StockExchanges");
  }

  putStockExchange(formData) {
    return this.http.put(environment.apiBaseURI + "StockExchanges/" + formData.id, formData);
  }

  deleteStockExchange(id) {
    return this.http.delete(environment.apiBaseURI + "StockExchanges/" + id);
  }
}
