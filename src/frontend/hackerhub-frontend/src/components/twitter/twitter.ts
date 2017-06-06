export class Twitter {
  tweets: any;
  isLoading: boolean = false;
  constructor() {
  }

  attached() {
      this.authorize();
  }

  authorize() {
    this.isLoading = true;
        fetch('https://civica-hackathon-api.azurewebsites.net/api/twitter/tweets', {
          method: 'GET',
          mode: 'cors',
        }).then(response => {
          response.json().then(data => {
            this.tweets = data;
            this.isLoading = false;
          })
        });
    }
}