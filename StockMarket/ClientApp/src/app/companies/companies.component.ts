import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CompanyService } from '../shared/company.service';
import { SectorService } from '../shared/sector.service';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  styleUrls: ['./companies.component.css']
})
export class CompaniesComponent implements OnInit {

  companyForms: FormArray = this.fb.array([]);
  sectorList = [];
  notification = null;

  constructor(private fb: FormBuilder, private sectorService: SectorService, private companyService: CompanyService) { }

  ngOnInit() {
    this.sectorService.getSectorList().subscribe(res => this.sectorList = res as []);
    this.companyService.getCompanyList().subscribe(res => {
      if (res == [])
        this.addCompanyForm();
      else {
        (res as []).forEach((company: any) => {
          this.companyForms.push(this.fb.group({
            id: [company.id],
            name: [company.name, Validators.required],
            turnover: [company.turnover, Validators.min(1)],
            ceo: [company.ceo, Validators.required],
            bod: [company.bod, Validators.required],
            sectorId: [company.sectorId, Validators.min(1)],
            brief: [company.brief, Validators.required]
          }));
        });
      }
    });
  }

  addCompanyForm() {
    this.companyForms.push(this.fb.group({
      id: [0],
      name: ["", Validators.required],
      turnover: [0, Validators.min(1)],
      ceo: ["", Validators.required],
      bod: ["", Validators.required],
      sectorId: [0, Validators.min(1)],
      brief: ["", Validators.required]
    }));
  }

  recordSubmit(fg: FormGroup) {
    if (fg.value.id == 0) {
      this.companyService.postCompany(fg.value).subscribe((res: any) => {
        fg.patchValue({ id: res.id });
        this.showNotification("insert");
      });
    }
    else {
      this.companyService.putCompany(fg.value).subscribe((res: any) => { this.showNotification("update"); });
    }
  }

  onDelete(id, i) {
    if (id && confirm("Record will be DELETED!")) {
      this.companyService.deleteCompany(id).subscribe((res: any) => {
        this.companyForms.removeAt(i);
        this.showNotification("delete");
      });
    }
    else if (!id)
      this.companyForms.removeAt(i);
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
