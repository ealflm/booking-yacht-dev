import { UsersService } from './../../services/users.service';

import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-home-pages',
  templateUrl: './home-pages.component.html',
  styleUrls: ['./home-pages.component.scss'],
})
export class HomePagesComponent implements OnInit {
  users: any = [];
  loading?: boolean = true;
  constructor(private userService: UsersService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): any {
    this.userService.getUsers().subscribe((responseData) => {
      this.users = responseData;
      setInterval(() => {
        this.loading = false;
      }, 1000);
    });
  }
}
