import { UsersService } from './../../services/users.service';
import { LocalStorageService } from './../../auth/localstorage.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss'],
})
export class NavMenuComponent implements OnInit {
  user: any;
  constructor(
    private localStorageService: LocalStorageService,
    private userService: UsersService
  ) {}

  ngOnInit(): void {
    const token = this.localStorageService.getToken();
    if (token && token !== null) {
      const tokenDecode = JSON.parse(atob(token.split('.')[1]));
      if (tokenDecode.Id) {
        this.userService.getMyInfor(tokenDecode.Id).subscribe((myInforRes) => {
          this.user = myInforRes.data;
          // console.log(this.user);
        });
      }
    }
  }
}
