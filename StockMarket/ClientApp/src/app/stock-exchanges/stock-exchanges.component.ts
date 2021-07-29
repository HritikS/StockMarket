import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StockExchangeService } from '../shared/stock-exchange.service';

@Component({
  selector: 'app-stock-exchanges',
  templateUrl: './stock-exchanges.component.html',
  styleUrls: ['./stock-exchanges.component.css']
})

export class StockExchangesComponent implements OnInit {

  stockExchangeForms: FormArray = this.fb.array([]);
  notification = null;

  constructor(private fb: FormBuilder, private stockExchangeService: StockExchangeService) { }

  ngOnInit() {
      this.stockExchangeService.getStockExchangeList().subscribe((res: any) => {
          if (!res)
              this.addStockExchangeForm();
          else {
              (res as []).forEach((stockExchange: any) => {
                  this.stockExchangeForms.push(this.fb.group({
                      id: [stockExchange.id, Validators.required],
                      name: [stockExchange.name, Validators.required],
                      brief: [stockExchange.brief, Validators.required],
                      address: [stockExchange.address, Validators.required],
                      remarks: [stockExchange.remarks, Validators.required]
                  }));
              });
          }
      });
  }

  addStockExchangeForm() {
    this.stockExchangeForms.push(this.fb.group({
      id: [0],
      name: ['', Validators.required],
      brief: ['', Validators.required],
      address: ['', Validators.required],
      remarks: ['', Validators.required]
    }));
  }

  recordSubmit(fg: FormGroup) {
      if (fg.value.id == 0) {
          this.stockExchangeService.postStockExchange(fg.value).subscribe((res: any) => {
              fg.patchValue({ id: res.id });
              this.showNotification("insert");
          });
      }
      else {
          this.stockExchangeService.putStockExchange(fg.value).subscribe((res: any) => { this.showNotification("update"); });
      }
  }

  onDelete(id, i) {
      if (id && confirm("Record will be DELETED!")) {
          this.stockExchangeService.deleteStockExchange(id).subscribe((res: any) => {
              this.stockExchangeForms.removeAt(i);
              this.showNotification("delete");
          });
      }
      else if (!id)
          this.stockExchangeForms.removeAt(i);
  }

  showNotification(category) {
      switch (category) {
          case 'insert':
              this.notification = { class: "text-success", msg: "created successfully" };
              break;
          case 'update':
              this.notification = { class: "text-primary", msg: "updated successfully" };
              break;
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
