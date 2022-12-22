import { Component, OnInit, SimpleChanges, OnChanges } from '@angular/core';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor() { }

  ngOnInit() {

  }

  ngOnChanges(changes: SimpleChanges) {
    console.log('changing');
  }

  opendotmatrixLayout() {
    const emptyInvoiceUrl = environment.api_server + 'orders/0.html';
    window.open(emptyInvoiceUrl, '_blank', "width=800,height=800");
  }
}
