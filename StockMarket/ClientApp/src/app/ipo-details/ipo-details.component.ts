import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CompanyService } from '../shared/company.service';
import { IpoDetailService } from '../shared/ipo-detail.service';
import { StockExchangeService } from '../shared/stock-exchange.service';

@Component({
  selector: 'app-ipo-details',
  templateUrl: './ipo-details.component.html',
  styleUrls: ['./ipo-details.component.css']
})

export class IpoDetailsComponent implements OnInit {

  ipoDetailForms: FormArray = this.fb.array([]);
  companyList = [];
  stockExchangeList = [];
  notification = null;

  constructor(private fb: FormBuilder, private companyService: CompanyService, private stockExchangeService: StockExchangeService, private ipoDetailService: IpoDetailService) { }

  ngOnInit() {
    this.companyService.getCompanyList().subscribe(res => this.companyList = res as []);
    this.stockExchangeService.getStockExchangeList().subscribe(res => this.stockExchangeList = res as [])
    this.ipoDetailService.getIPODetail().subscribe(res => {
      if (res == [])
        this.addIPODetailForm();
      else {
        (res as []).forEach((ipoDetail: any) => {
          this.ipoDetailForms.push(this.fb.group({
            companyId: [ipoDetail.companyId, Validators.min(1)],
            stockExchangeId: [ipoDetail.stockExchangeId, Validators.min(1)],
            pps: [ipoDetail.pps, Validators.min(1)],
            tnos: [ipoDetail.tnos, Validators.min(1)],
            openDateTime: [ipoDetail.openDateTime, Validators.required],
            remarks: [ipoDetail.remarks, Validators.required]
          }));
        });
      }
    });
  }

  addIPODetailForm() {
    this.ipoDetailForms.push(this.fb.group({
      companyId: [0, Validators.min(1)],
      stockExchangeId: [0, Validators.min(1)],
      pps: [0, Validators.min(1)],
      tnos: [0, Validators.min(1)],
      openDateTime: [0, Validators.required],
      remarks: ['R', Validators.required]
    }));
  }

  recordSubmit(fg: FormGroup) {
    this.ipoDetailService.postIPODetail(fg.value).subscribe((res: any) => { this.showNotification('insert'); });
  }

  onUpdate(fg: FormGroup) {
    this.ipoDetailService.putIPODetail(fg.value.companyId, fg.value.stockExchangeId, fg.value).subscribe((res: any) => { this.showNotification('update'); });
  }

  onDelete(companyId, stockExchangeId, i) {
    if (companyId == 0 || stockExchangeId == 0)
      this.ipoDetailForms.removeAt(i)
    else if (confirm("Record will be DELETED!")) {
      this.ipoDetailService.deleteIPODetail(companyId, stockExchangeId).subscribe((res: any) => { this.ipoDetailForms.removeAt(i); this.showNotification('delete'); });
    }
  }

  showNotification(category) {
    switch (category) {
      case 'insert':
        this.notification = { class: "text-success", msg: "created successfully" };
        break;
      case 'update':
        this.notification = { class: "text-primary", msg: "updated successfully" };
        break
      case 'delete':
        this.notification = { class: "text-danger", msg: "deleted successfully" };
        break;
      default:
        break;
    }

    setTimeout(() => {
      this.notification = null;
    }, 3000);
  }
}
