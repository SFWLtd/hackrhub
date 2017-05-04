import {tracking} from 'tracking';

export class Home {    
  message: string;
  video: any;
  canvas: any;
  imageUrl: string;
  
  constructor() {
  }

  takePhoto() {
    var context = this.canvas.getContext('2d');
    context.drawImage(this.video, 0, 0, 220, 150);
    this.imageUrl = this.canvas.toDataURL();
  }

  attached() {
    // Get access to the camera!
    if(navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {

        var video = this.video;

        // Not adding `{ audio: true }` since we only want video now
        navigator.mediaDevices.getUserMedia({ video: true }).then(function(stream) {
            video.src = window.URL.createObjectURL(stream);
            video.play();
        });
    }
  }

  // attached() {
  //   let context: any = this.canvas.getContext('2d');
  //   let tracker: tracking.ObjectTracker = new tracking.ObjectTracker('face');
  //   tracker.setInitialScale(4);
  //   tracker.setStepSize(2);
  //   tracker.setEdgesDensity(0.1);
  //   tracking.track('#video', tracker, { camera: true });
  //   tracker.on('track', function(event) {
  //     context.clearRect(0, 0, this.canvas.width, this.canvas.height);
  //     event.data.forEach(function(rect) {
  //       context.strokeStyle = '#a64ceb';
  //       context.strokeRect(rect.x, rect.y, rect.width, rect.height);
  //       context.font = '11px Helvetica';
  //       context.fillStyle = "#fff";
  //       context.fillText('x: ' + rect.x + 'px', rect.x + rect.width + 5, rect.y + 11);
  //       context.fillText('y: ' + rect.y + 'px', rect.x + rect.width + 5, rect.y + 22);
  //     });
  //   });
  // }
}