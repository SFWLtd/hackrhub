export class App {
  router: any;
  configureRouter(config, router){
      config.title = 'Hackrhub';		
      config.options.pushState = true;
      config.map([        
          { route: ['','home'],  name: 'home', nav: true ,
            moduleId: './components/home/home', title:'Home' },
          { route: 'ideas',  name: 'ideas', nav: true,
            moduleId: './components/idea-generator/idea-generator', title:'Idea Generator' },
          { route: 'hackrs',  name: 'hackrs', nav: true,
            moduleId: './components/people/people', title:'Hackrs' },
          { route: 'teams',  name: 'teams', nav: true,
            moduleId: './components/teams/teams', title:'Teams' }
      ]);
   }
}
