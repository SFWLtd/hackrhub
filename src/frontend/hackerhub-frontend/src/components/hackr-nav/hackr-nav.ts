export class HackrNav {
  name: string
  personid: string;
  hasFace: boolean;

  constructor() {

    this.hasFace = false;

    if (localStorage['name']) {
      this.name = localStorage['name'];
      this.personid = localStorage['personid'];
      this.hasFace = true;
    } else {
      this.name = 'Faceless guest :(';
    }
  }

  logOut() {
    localStorage.removeItem('name');
    localStorage.removeItem('personid');

    window.location.href=('/');
  }
};