export class App {
  router: any;
  configureRouter(config, router){
      config.title = 'Aurelia';		
      config.options.pushState = true;
      config.map([        
          { route: ['','home'],  name: 'home', nav: true ,
            moduleId: './components/home/home', title:'Home' },
          { route: 'ideas',  name: 'ideas', nav: true,
            moduleId: './components/idea-generator/idea-generator', title:'Idea Generator' },
          { route: 'people',  name: 'people', nav: true,
            moduleId: './components/people/people', title:'People' }
      ]);
   }
}
