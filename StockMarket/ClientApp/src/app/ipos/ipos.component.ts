import { Component, OnInit } from '@angular/core';
import { IpoDetailService } from '../shared/ipo-detail.service';

@Component({
  selector: 'app-ipos',
  templateUrl: './ipos.component.html',
  styleUrls: ['./ipos.component.css']
})
export class IposComponent implements OnInit {

  ipos = [];

  constructor(private service: IpoDetailService) { }

  ngOnInit() {
    this.service.getSortedIPODetail().subscribe(
      (res: any) => { this.ipos = res as [] },
      (err: any) => { console.log(err); }
    );
  }

}
