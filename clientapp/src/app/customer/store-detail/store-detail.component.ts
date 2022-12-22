import { Component, OnInit } from '@angular/core';
import { Store } from '../BillinInformation.model';
import { StoreService } from '../store.service';

@Component({
  selector: 'app-store-detail',
  templateUrl: './store-detail.component.html',
  styleUrls: ['./store-detail.component.css']
})
export class StoreDetailComponent implements OnInit {

  public storeModel: Store;
  constructor(private storeService: StoreService) { }

  ngOnInit() {
    this.storeService.StoreInfo
      .subscribe(storeInfo => {
        this.storeModel = storeInfo;
      });
  }

}
