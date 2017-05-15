export class Teams {
  teams: any;
  
  constructor() {
    fetch('https://civica-hackathon-api.azurewebsites.net/api/teams', {
        method: 'GET',
        mode: 'cors'
      }).then(response => {
          response.json().then(data => {
              data = data.filter(function(team) {
                  return team.Name !== "Observers";
              })
              this.teams = data;
          });
      });
   }
}