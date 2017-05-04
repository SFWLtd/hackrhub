import {tracking} from 'tracking';

export class SelfieLogin {  
  message: string;
  video: any;
  canvas: any;
  imageUrl: string;
  
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
    this.message = data;

    this.apiPost('http://civica-hackathon-api.azurewebsites.net/api/detect', data, function(response) {
      alert(response);
    });
  }

  apiPost(url, data, callback) {
    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: headers,
        credentials: 'include'
      }).then(function (response) {
          callback(response);
      });
  };
}