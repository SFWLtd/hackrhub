export class People {
  people: any;

  constructor() {
    let stupidHack = this;
    fetch('https://civica-hackathon-api.azurewebsites.net/api/people', {
        method: 'GET',
        mode: 'cors'
      }).then(function (response) {
          response.json().then(function(data) {
              stupidHack.people = data;
          });
      });
   }
}