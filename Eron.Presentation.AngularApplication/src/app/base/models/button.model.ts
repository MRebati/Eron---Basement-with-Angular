export class Button {
  Url?: string;
  ExternalUrl = false;
  ButtonClass?: string;
  ButtonText?: string;
  ButtonIconClass?: string;
  ClickAction?: any;

  constructor(data: any) {
    this.Url = data.Url;
    this.ExternalUrl = data.ExternalUrl;
    this.ButtonClass = data.ButtonClass;
    this.ButtonIconClass = data.ButtonIconClass;
    this.ButtonText = data.ButtonText;
    this.ClickAction = data.ClickAction;
  }
}
