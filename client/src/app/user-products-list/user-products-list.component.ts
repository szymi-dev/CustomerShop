import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-products-list',
  templateUrl: './user-products-list.component.html',
  styleUrls: ['./user-products-list.component.scss']
})
export class UserProductsListComponent implements OnInit {
  products: any[];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('https://localhost:6001/api/products/GetProducts/{username}').subscribe((response: any) => {
      this.products = response;
    }, error => {
      console.log(error);
    });
  }

}
