export class Vote {
  teams: any;
  votingForTeamId: number;

  constructor() {
    this.votingForTeamId = 0;
  }

  attached() {
    let stupidHack = this;
    fetch('https://civica-hackathon-api.azurewebsites.net/api/teams', {
        method: 'GET',
        mode: 'cors'
      }).then(function (response) {
          response.json().then(function(data) {

              let teamNumber: number = 1;
              data = data.filter(function(team) {
                  let canVoteForTeam: boolean = true;
                  team.People.forEach(p => {
                    if (p.Name === localStorage['name']) {
                      canVoteForTeam = false;
                    }
                  });

                  if (canVoteForTeam && team.Name !== "Observers") {
                     team.Number = teamNumber;
                     teamNumber++;
                     return true;
                  }

                  return false;
              });

              stupidHack.teams = data;
          });
      });
    }

   voteForTeam(teamId: number) {
      this.votingForTeamId = teamId;
   }
  }