export class Teams {
  teams: any;
  
  constructor() {
    let stupidHack = this;
    fetch('http://civica-hackathon-api.azurewebsites.net/api/teams', {
        method: 'GET',
        mode: 'cors'
      }).then(function (response) {
          response.json().then(function(data) {
              stupidHack.teams = data;
          });
      });
   }
}