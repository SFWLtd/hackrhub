export class VoteResults {
  results: any;
  
  constructor() {
    fetch('https://civica-hackathon-api.azurewebsites.net/api/results', {
        method: 'GET',
        mode: 'cors'
      }).then(response => {
          response.json().then(data => {
                data = data.filter(function(result) {
                  return result.Team !== "Observers";
              })
              this.results = data;
          });
      });
   }
}