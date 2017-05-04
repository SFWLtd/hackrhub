import {tracking} from 'tracking';

export class SelfieLogin {  
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
    data = data.replace('data:image/png;base64,', '');

    this.postAsBase64(data);
  }

  postAsBlob(data) {
    fetch(data)
      .then(res => res.blob())
      .then(blob => {
        this.apiPost(blob);
    });
  }

  postAsBase64(data) {
    this.apiPost(data);
  }

  apiPost(data) {
    fetch('http://civica-hackathon-api.azurewebsites.net/api/detect', {
        method: 'POST',
        body: JSON.stringify(data),
        headers: this.headers(),
        mode: 'cors'
      }).then(function (response) {
          response.json().then(function(data) {
              localStorage['name'] = data.Name;
              localStorage['faceid'] = data.FaceId;
          });
      });
  };

  headers() {
    return {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    }
  }
}