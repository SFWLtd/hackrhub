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
    this.imageUrl = this.canvas.toDataURL();
  }
}