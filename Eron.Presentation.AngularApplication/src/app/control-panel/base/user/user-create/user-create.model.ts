export interface UserCreateModel {
  email: string;
  phoneNumber: string;
  postalCode: string;
  socialNumber: string;
  address: string;
  firstName: string;
  lastName: string;
  position: string;
  imageId: string;
  companyName: string;
  cityName: string;
  provinceName: string;
  faxNumber: string;
  selectedRoles: string[];
  password: string;
  confirmPassword: string;
}
