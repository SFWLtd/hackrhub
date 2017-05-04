export class IdeaGenerator {
  array1 = ['Uber', 'Github', 'Facial recognition', 'Air BNB', 'Twitter', 'Facebook', 'Email', 'LinkedIn', 'Timesheets', 'PornHub'];
  array2 = ['horses', 'dogs', 'blind people', 'the army', 'the navy', 'cats', 'trees', 'hipsters', 'goths'];
  array3 = ['F#', 'COBOL', 'Prolog', 'SmallTalk', 'FORTRAN', 'Java', 'C++', 'C', 'DogeScript', 'JavaScript', 'another new JS framework', 'assembly', 'PHP'];
  array4 = ['Azure', 'a Raspberry Pi', 'AWS', 'SharePoint', 'an iPad', "your grandma's 486", 'an Amstrad CPC 6128', 'a Casio 808'];
  array5 = ["serverless", "you're not allowed to use the letter 'e'", "display-less", "keyboard-less", "the battery only lasts 2 hours", "you must support Netscape 4", "it's not on the approved software list", "there are no unit tests"];
  item1 = this.array1[Math.floor(Math.random() * this.array1.length)]; 
  item2 = this.array2[Math.floor(Math.random() * this.array2.length)];
  item3 = this.array3[Math.floor(Math.random() * this.array3.length)];
  item4 = this.array4[Math.floor(Math.random() * this.array4.length)];
  item5 = this.array5[Math.floor(Math.random() * this.array5.length)];
  constructor() {
   }
}