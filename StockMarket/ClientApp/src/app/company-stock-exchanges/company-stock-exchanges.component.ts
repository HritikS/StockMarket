import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CompanyStockExchangeService } from '../shared/company-stock-exchange.service';
import { CompanyService } from '../shared/company.service';
import { StockExchangeService } from '../shared/stock-exchange.service';

@Component({
  selector: 'app-company-stock-exchanges',
  templateUrl: './company-stock-exchanges.component.html',
  styleUrls: ['./company-stock-exchanges.component.css']
})

export class CompanyStockExchangesComponent implements OnInit {

  companyStockExchangeForms: FormArray = this.fb.array([]);
  companyList = [];
  stockExchangeList = [];
  notification = null;

  constructor(private fb: FormBuilder, private companyService: CompanyService, private stockExchangeService: StockExchangeService,
    private companyStockExchangeService: CompanyStockExchangeService) { }

  ngOnInit() {
    this.companyService.getCompanyList().subscribe(res => this.companyList = res as []);
    this.stockExchangeService.getStockExchangeList().subscribe(res => this.stockExchangeList = res as []);
    this.companyStockExchangeService.getCompanyStockExchange().subscribe(res => {
      if (res == [])
        this.addCompanyStockExchangeForm();
      else {
        (res as []).forEach((companyStockExchange: any) => {
          this.companyStockExchangeForms.push(this.fb.group({
            companyId: [companyStockExchange.companyId, Validators.min(1)],
            stockExchangeId: [companyStockExchange.stockExchangeId, Validators.min(1)]
          }));
        });
      }
    });
  }

  addCompanyStockExchangeForm() {
    this.companyStockExchangeForms.push(this.fb.group({
      companyId: [0, Validators.min(1)],
      stockExchangeId: [0, Validators.min(1)]
    }));
  }

  recordSubmit(fg: FormGroup) {
    this.companyStockExchangeService.postCompanyStockExchange(fg.value).subscribe((res: any) => { this.showNotification('insert'); });
  }

  onDelete(companyId, stockExchangeId, i) {
    if (!companyId || !stockExchangeId)
      this.companyStockExchangeForms.removeAt(i);
    else if (confirm("Record will be DELETED!"))
      this.companyStockExchangeService.deleteCompanyStockExchange(companyId, stockExchangeId).subscribe((res: any) => {
        this.companyStockExchangeForms.removeAt(i);
        this.showNotification('delete');
      });
  }

  showNotification(category) {
    switch (category) {
      case 'insert':
        this.notification = { class: "text-success", msg: "created successfully" };
        break;
      case 'delete':
        this.notification = { class: "text-danger", msg: "deleted successfully" };
        break;
      default:
        this.notification = {class: "text-primary", msg: "already added"};
        break;
    }

    setTimeout(() => {
      this.notification = null;
    }, 3000);
  }

}
