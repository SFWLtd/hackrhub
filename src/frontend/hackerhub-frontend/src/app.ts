export class App {
  router: any;
  configureRouter(config, router){
      config.title = 'Aurelia';		
      config.options.pushState = true;
      config.map([        
          { route: ['','home'],  name: 'home', nav: true ,
            moduleId: './components/home/home', title:'Home' },
          { route: 'ideas',  name: 'idea-generator', nav: true,
            moduleId: './components/idea-generator/idea-generator', title:'Idea Generator' }
      ]);
   }
}
