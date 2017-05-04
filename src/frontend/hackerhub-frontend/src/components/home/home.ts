import {tracking} from 'tracking';

export class Home {   

  loginType: string;

  constructor() {
    this.loginType = LoginTypes.None;
  }

  showSelfieLogin() {
    this.loginType = LoginTypes.Selfie;
  }

  showGuestLogin() {
    this.loginType = LoginTypes.Guest;
  }

  cancelLogin() {
    this.loginType = LoginTypes.None;
  }
}

class LoginTypes {
  public static None: string = "None";
  public static Selfie: string = "Selfie";
  public static Guest: string = "Guest";
}