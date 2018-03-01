import { Component, OnInit, AfterViewInit } from '@angular/core';
import { UserService } from '../../base/user/user.service';
import { MenuService } from '../../../base/services/menu.service';
import { Menu } from '../../../base/models/menu.model';

declare var $: any;

@Component({
  selector: 'app-side',
  templateUrl: './side.component.html',
  styleUrls: ['./side.component.scss'],
  providers: [MenuService]
})
export class SideComponent implements OnInit, AfterViewInit {
  MenuList: Array<Menu>;
  userInfo: any;
  userName: string;
  constructor(
    private MenuService: MenuService,
    private userService: UserService) {
    this.MenuList = MenuService.getMenues();
    this.userService.getUserInfo().subscribe(
      (response) => {
        this.userInfo = response;
        this.userName = this.userInfo.firstName != null && this.userInfo.lastName != null ?
          this.userInfo.firstName + ' ' + this.userInfo.lastName :
          this.userInfo.email;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  ngOnInit() {
  }

  ngAfterViewInit() {
    $('#side-menu').metisMenu();
  }

}
