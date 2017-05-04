import {tracking} from 'tracking';

export class SelfieLogin {  
  video: any;
  canvas: any;
  imageUrl: string;
  message: string;
  
  constructor() {
  }

  attached() {
    if(navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {

        var video = this.video;
        navigator.mediaDevices.getUserMedia({ video: true }).then(function(stream) {
            video.src = window.URL.createObjectURL(stream);
            video.play();
        });
    }
  }

  takePhoto() {
    var context = this.canvas.getContext('2d');
    context.drawImage(this.video, 0, 0, 220, 150);
    var data = this.canvas.toDataURL("image/png");
    data = data.replace('data:image/png;base64,', '');

    this.apiPost(data);
  }

  apiPost(data) {

    var hack = this;

    fetch('https://civica-hackathon-api.azurewebsites.net/api/detect', {
        method: 'POST',
        body: JSON.stringify(data),
        headers: this.headers(),
        mode: 'cors'
      }).then(function (response) {
          response.json().then(function(data) {
              try {
                if (data.Name) {
                  localStorage['name'] = data.Name;
                } else {
                  hack.message = hack.errorMessage();
                  return;
                }
                if (data.FaceId) {
                  localStorage['faceid'] = data.FaceId;
                }

                window.location.href='/teams';

              } catch (e) {
                hack.message = hack.errorMessage();
              }
          });
      }).catch(function(err) {
          hack.message = hack.errorMessage();
      });
  };

  headers() {
    return {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    }
  }

  errorMessage() {
    return 'Could not log in :(. Please try again';
  }
}