beforeEach(() => {
    cy.visit('https://localhost:7276/')
    
  })
describe("Editar un curso",()=> {
    it ("verificar la creacion de nuevos cursos",()=>{
      //ir al apartado de cursos para editar uno nuevo
       cy.get('a.nav-link.text-dark[href="/Courses"]').click()
      //Edicion del curso 
      cy.get('a.btn.btn-sm.btn-success.text-white[href="/Courses/Edit/1"]').click()
      cy.get('input#Name').clear().type('Diseño y Arquitectura')
      cy.get('button.btn.btn-success').click()
       })
   })  