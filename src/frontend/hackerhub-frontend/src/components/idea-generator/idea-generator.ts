export class IdeaGenerator {
  array1 = ['Uber', 'Github', 'Facial recognition', 'Airbnb', 'Twitter', 'Facebook', 'Email', 'LinkedIn', 'Timesheets', 'Hackathon idea generator', 'Tinder', 'Blockchain', 'IoT', 'Cyber', 'Chatbot', 'Regex', 'Social network', 'XML', 'Deep learning', 'Mobile app'];  
  array2 = ['horses', 'dogs', 'the army', 'the navy', 'cats', 'trees', 'hipsters', 'goths', 'Civica directors', 'musicians', 'geeks', 'clowns', 'data scientists', 'bankers', 'politicians', 'teachers', 'clown fish'];
  array3 = ['F#', 'COBOL', 'Prolog', 'SmallTalk', 'FORTRAN', 'Java', 'C++', 'C', 'DogeScript', 'JavaScript', 'a random JS framework', 'assembly', 'PHP', 'Ruby', 'R', 'Python', 'VB6', 'Perl', 'Objective-C', 'Swift', 'MATLAB', 'Go', 'Turbo Pascal', 'POP-11']; 
  array4 = ['Azure', 'a Raspberry Pi', 'AWS', 'SharePoint', 'an iPad', "your grandma's 486", 'an Amstrad CPC 6128', 'a Casio 808', 'Android', 'iOS', 'a mainframe', 'Sun SPARC', 'Spectrum 48K', 'Vic-20', 'an embedded microprocessor'];  
  array5 = ["serverless", "you're not allowed to use the letter 'e'", "display-less", "keyboard-less", "the battery only lasts 2 hours", "you must support Netscape 4", "it's not on the approved software list", "there are no unit tests", "everything is in base 64", "you can't use strings", "meeting WCAG-AAA", "waterproof to 50 feet", "it looks good at 800x600", "only using your left hand"]; 
  item1 = this.array1[Math.floor(Math.random() * this.array1.length)]; 
  item2 = this.array2[Math.floor(Math.random() * this.array2.length)];
  item3 = this.array3[Math.floor(Math.random() * this.array3.length)];
  item4 = this.array4[Math.floor(Math.random() * this.array4.length)];
  item5 = this.array5[Math.floor(Math.random() * this.array5.length)];
  constructor() {
   }

   refresh() {
    this.item1 = this.array1[Math.floor(Math.random() * this.array1.length)]; 
    this.item2 = this.array2[Math.floor(Math.random() * this.array2.length)];
    this.item3 = this.array3[Math.floor(Math.random() * this.array3.length)];
    this.item4 = this.array4[Math.floor(Math.random() * this.array4.length)];
    this.item5 = this.array5[Math.floor(Math.random() * this.array5.length)];
   }
}