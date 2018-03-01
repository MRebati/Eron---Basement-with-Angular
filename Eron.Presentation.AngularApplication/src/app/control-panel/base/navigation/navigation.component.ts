import { Component, OnInit } from '@angular/core';
import { BreadCrump } from '../../../base/models/breadCrump.model';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {
  breadCrump: BreadCrump;
  constructor() { }

  ngOnInit() {
  }

}
