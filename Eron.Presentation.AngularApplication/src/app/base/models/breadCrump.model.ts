import { UrlNamePair } from './urlNamePair.model';
import { Button } from './button.model';

export class BreadCrump {
  Title: string;
  FirstNode?: UrlNamePair;
  SecondNode?: UrlNamePair;
  ThirdNode?: UrlNamePair;
  Button?: Button[];
  DarkBackground: boolean;

  constructor(breadCrump: BreadCrump) {
    this.Title = breadCrump.Title;
    this.FirstNode = breadCrump.FirstNode;
    this.SecondNode = breadCrump.SecondNode;
    this.ThirdNode = breadCrump.ThirdNode;
    this.Button = breadCrump.Button;
    this.DarkBackground = breadCrump.DarkBackground;
  }
}
