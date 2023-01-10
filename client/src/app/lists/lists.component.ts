import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from '../_models/pagination';
import { IProduct } from '../_models/product';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.scss']
})
export class ListsComponent implements OnInit {
  products: IProduct[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('https://localhost:6001/api/products?pageSize=10').subscribe((response:any) => {
      this.products = response;
    }, error => {
      console.log(error);
    });
  }
}
