export class RegisterViewModel {
  email: string;
  password: string;
  confirmPassword: string;
  recaptcha: string;
  grant_type: 'password';

  constructor(registerViewModel: RegisterViewModel) {
    this.email = registerViewModel.email;
    this.password = registerViewModel.password;
    this.confirmPassword = registerViewModel.confirmPassword;
    this.recaptcha = registerViewModel.recaptcha;
  }
}
