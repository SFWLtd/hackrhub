export class VoteResults {
  results: any;
  
  constructor() {
    let stupidHack = this;
    fetch('https://civica-hackathon-api.azurewebsites.net/api/results', {
        method: 'GET',
        mode: 'cors'
      }).then(function (response) {
          response.json().then(function(data) {
                data = data.filter(function(result) {
                  return result.Team !== "Observers";
              })
              stupidHack.results = data;
          });
      });
   }
}