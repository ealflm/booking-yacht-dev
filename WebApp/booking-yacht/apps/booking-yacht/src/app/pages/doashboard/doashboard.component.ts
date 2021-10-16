import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-doashboard',
  templateUrl: './doashboard.component.html',
  styleUrls: ['./doashboard.component.scss'],
})
export class DoashboardComponent implements OnInit {
  selectedIndex?: number = 1;
  constructor() {}

  ngOnInit(): void {}
  activeCard(id: any) {
    this.selectedIndex = id;
  }
}
