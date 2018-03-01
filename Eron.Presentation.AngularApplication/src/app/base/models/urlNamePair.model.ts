export class UrlNamePair {
  Url: string;
  Name: string;
  ExternalUrl: boolean;

  constructor(name: string, url?: string, externalUrl: boolean = false) {
    this.Name = name;
    this.Url = url;
    this.ExternalUrl = externalUrl;
  }
}
