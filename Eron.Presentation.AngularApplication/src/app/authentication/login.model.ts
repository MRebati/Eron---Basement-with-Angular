export class LoginViewModel {
  UserName: string;
  Password: string;
  RememberMe: boolean;
  CaptchaResponse: string;
  grant_type: 'password';

  constructor(loginViewModel: LoginViewModel) {
    this.UserName = loginViewModel.UserName;
    this.Password = loginViewModel.Password;
    this.RememberMe = loginViewModel.RememberMe;
    this.CaptchaResponse = loginViewModel.CaptchaResponse;
  }
}
