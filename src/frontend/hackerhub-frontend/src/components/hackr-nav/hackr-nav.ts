export class HackrNav {
  name: string
  personid: string;

  constructor() {
    if (localStorage['name']) {
      this.name = localStorage['name'];
      this.personid = localStorage['personid'];
    } else {
      this.name = 'you faceless entity';
    }
  }

  logOut() {
    localStorage.removeItem('name');
    localStorage.removeItem('personid');

    window.location.href=('/');
  }
};