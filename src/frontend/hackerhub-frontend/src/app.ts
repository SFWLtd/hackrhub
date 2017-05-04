
export class App {
  router: any;
  configureRouter(config, router){
      config.title = 'Hackrhub';		
      config.options.pushState = true;
      config.map([        
          { route: ['','home'],  name: 'home', nav: true ,
            moduleId: './components/home/home', title:'Home' },
          { route: ['selfie-login'],  name: 'selfie-login', nav: true ,
            moduleId: './components/selfie-login/selfie-login', title:'Selfie-Login' },
          { route: 'ideas',  name: 'ideas', nav: true,
            moduleId: './components/idea-generator/idea-generator', title:'Idea Generator' },
          { route: 'hackrs',  name: 'hackrs', nav: true,
            moduleId: './components/people/people', title:'Hackrs' },
          { route: 'teams',  name: 'teams', nav: true,
            moduleId: './components/teams/teams', title:'Teams' },
          { route: 'vote',  name: 'vote', nav: true,
            moduleId: './components/vote/vote', title:'Vote' },
          { route: 'photos',  name: 'photos', nav: true,
            moduleId: './components/photos/photos', title:'#civicadigihack17' },
          { route: 'results',  name: 'results', nav: true,
            moduleId: './components/vote-results/vote-results', title:'Results' }
      ]);
   }
}
