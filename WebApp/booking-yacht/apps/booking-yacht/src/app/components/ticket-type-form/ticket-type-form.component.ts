import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'booking-yacht-ticket-type-form',
  templateUrl: './ticket-type-form.component.html',
  styleUrls: ['./ticket-type-form.component.scss'],
})
export class TicketTypeFormComponent implements OnInit {
  loading = false;

  constructor(private router: Router, private location: Location) {}

  ngOnInit(): void {}
  onCancle() {
    this.location.back();
  }
}
