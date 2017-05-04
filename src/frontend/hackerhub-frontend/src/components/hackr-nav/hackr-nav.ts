export class HackrNav {
  name: string

  constructor() {
    if (localStorage['name']) {
      this.name = localStorage['name'];
    } else {
      this.name = 'Guest';
    }
  }
}