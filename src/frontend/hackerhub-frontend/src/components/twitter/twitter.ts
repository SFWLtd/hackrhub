export class Twitter {
  tweets: any;
  constructor() {
  }

  attached() {
      this.authorize();
  }

  authorize() {
        fetch('https://civica-hackathon-api.azurewebsites.net/api/twitter/tweets', {
          method: 'GET',
          mode: 'cors',
        }).then(response => {
          response.json().then(data => {
            this.tweets = data;
          })
        });
    }
}