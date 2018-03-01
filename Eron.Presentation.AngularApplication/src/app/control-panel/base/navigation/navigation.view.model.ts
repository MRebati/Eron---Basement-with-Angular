export interface NavigationViewModel {
  id: number;
  linkType: number;
  linkPlacement: number;
  url: string;
  linkText: string;
  urlTargetType: number;
  target: number;
  image: string;
  iconClass: string;
  parentId?: number;
  children: NavigationViewModel[];
}
