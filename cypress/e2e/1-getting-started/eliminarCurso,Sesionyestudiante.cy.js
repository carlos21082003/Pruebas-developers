beforeEach(() => {
    cy.visit('https://localhost:7276/')  
})
describe("Eliminar curso",()=> {
    it ("verificar la eliminacion de un curso, una sesion y un estudiante",()=>{
        cy.get('a.nav-link.text-dark[href="/Courses"]').click()
       
        cy.get('a.btn.btn-sm.btn-warning[href="/Courses/Edit/1"]').click();
    })
  })

  describe("Eliminar sesion",()=> {
    it ("verificar la eliminacion de un curso, una sesion y un estudiante",()=>{
     
    })
  })
  describe("Eliminar estudiantes",()=> {
    it ("verificar la eliminacion de un curso, una sesion y un estudiante",()=>{
      
    })
  })