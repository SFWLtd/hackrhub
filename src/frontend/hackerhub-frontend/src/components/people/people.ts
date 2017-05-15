export class People {
  people: any;

  constructor() {
    fetch('https://civica-hackathon-api.azurewebsites.net/api/people', {
        method: 'GET',
        mode: 'cors'
      }).then(response => {
          response.json().then(data => {
              this.people = data;
          });
      });
   }
}