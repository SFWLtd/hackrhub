export class HackrNav {
  name: string

  constructor() {
    if (localStorage['name']) {
      this.name = localStorage['name'];
    } else {
      this.name = 'you faceless entity';
    }
  }

  logOut() {
    localStorage.removeItem('name');
    localStorage.removeItem('faceid');

    window.location.href=('/');
  }
}