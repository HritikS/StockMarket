import { Component, OnInit } from '@angular/core';
import { CompanyService } from '../shared/company.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {

  companies = [];
  allCompanies = [];

  constructor(private service: CompanyService) { }

  ngOnInit() {
    this.service.getCompanyListFull().subscribe(
      (res: any) => { this.companies = this.transform(res as []); this.allCompanies = this.transform(res as []); },
      (err: any) => { console.log(err); }
    );
  }

  onSearch(pat) {
    this.service.getCompanyByPattern(pat).subscribe(
      (res: any) => { this.companies = this.transform(res as []); },
      (err: any) => { this.companies = this.allCompanies }
    );
  }

  transform(arr) {
    var acc = arr.reduce((acc, obj) => {
      const key = obj.c.id;
      if (!acc[key])
        acc[key] = { name: obj.c.name, turnover: obj.c.turnover, ceo: obj.c.ceo, bod: obj.c.bod, stockExchanges: [], sector: obj.sector, brief: obj.c.brief };
      acc[key].stockExchanges.push(obj['stockExchanges']);
      return acc;
    }, {});
    return Object.values(acc);
  }

}
