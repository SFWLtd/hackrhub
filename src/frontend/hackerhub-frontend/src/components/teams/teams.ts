export class Teams {
  teams: any;
  
  constructor() {
    let stupidHack = this;
    fetch('https://civica-hackathon-api.azurewebsites.net/api/teams', {
        method: 'GET',
        mode: 'cors'
      }).then(function (response) {
          response.json().then(function(data) {
              data = data.filter(function(team) {
                  return team.Name !== "Observers";
              })
              stupidHack.teams = data;
          });
      });
   }
}