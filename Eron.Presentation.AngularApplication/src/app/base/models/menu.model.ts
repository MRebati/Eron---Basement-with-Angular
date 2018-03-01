import { Status } from './status.model';


export class Menu {
  Path?: string;
  Name: string;
  Icon?: string;
  Label?: string;
  LabelType?: Status;
  Children?: Array<Menu>;

  constructor(menu: Menu) {
    this.Children = menu.Children;
    this.Icon = menu.Icon;
    this.Label = menu.Label;
    this.LabelType = menu.LabelType;
    this.Path = menu.Path;
    this.Name = menu.Name;

  }
}
