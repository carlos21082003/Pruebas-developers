beforeEach(() => {
    cy.visit('https://localhost:7276/')
    
  })
describe("Registrarse e iniciar sesion",()=>{
    it("verificar el registro de usuario",()=>{
      //registrar usuario
      cy.get('a#register').click()
      cy.get('input#Input_LastName').type('García')
      cy.get('input#Input_FirstName').type('Juan')
      cy.get('input#Input_PhoneNumber').type('845672391')
      cy.get('input#Input_Email').type('Juan@gmail.com')
      cy.get('input#Input_Password').type('Usuario123*')
      cy.get('input#Input_ConfirmPassword').type('Usuario123*')
      cy.get('select').select('Estudiante')
      cy.get('button#registerSubmit').click()
      cy.get('a#login').click()
      //iniciar sesion 
      cy.get('input#Input_Email').type('Juan@gmail.com')
      cy.get('input#Input_Password').type('Usuario123*')
      cy.get('button#login-submit').click()
      //verficar el ingreso del usuario
      cy.get('a#manage').contains('¡Bienvenido! Juan@gmail.com!').should('exist')
    })
  })

  describe("crear curso",()=> {
    it ("verificar la creacion de nuevos cursos",()=>{
      //ir al apartado de cursos para crear uno nuevo
       cy.get('a.nav-link.text-dark[href="/Courses"]').click()
       cy.get('a.btn.btn-primary[href="/Courses/Create"]').click()
      //Creacion del nuevo curso 
       cy.get('input#Name').type('Robotica')
       cy.get('textarea#Description').type("Aprenderás temas avanzados y básicos de robótica ")
       cy.get('input#Hours').clear().type('5')
       cy.get('textarea#Theme').type('Programación en Arduino ')
       cy.get('input#Price').type('25')
       cy.get('button.btn.btn-success').click()
       cy.get('a.nav-link.text-dark[href="/"]').click()
       cy.get('a[href="/Courses/Details/5"]').contains('< img class="card-img-top img-resize" alt="Robotica">').should
       })
   })  
  
   
  