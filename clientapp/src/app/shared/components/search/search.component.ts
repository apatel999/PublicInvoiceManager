import { Component, OnInit, Output, EventEmitter, Input, OnChanges, SimpleChanges } from '@angular/core';
import { SearchParam } from '../../model/search-param';

@Component({
  selector: 'search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, OnChanges {

  @Input() param: SearchParam;
  @Output() change: EventEmitter<SearchParam> = new EventEmitter();
  @Output() searchButtonClick: EventEmitter<SearchParam> = new EventEmitter();
  @Output() reset: EventEmitter<any> = new EventEmitter();

  public searchParam: SearchParam = new SearchParam();

  constructor() { }

  ngOnInit() {

  }
  ngOnChanges(changes: SimpleChanges) {
    this.param = changes["param"].currentValue;
  }

  search() {
    this.searchButtonClick.emit(this.searchParam);
  }

  resetSearch() {
    this.searchParam = new SearchParam();
    this.reset.emit();
  }
}
