beforeEach(() => {
    cy.visit('https://localhost:7276/')  
 })
 describe("Sesiones",()=> {
    it ("verificar la creacion de sesiones",()=>{
        cy.get('a.nav-link.text-dark[href="/Classrooms"]').click()
        cy.get('a.btn.btn-primary').click()
        //seleccionar opciones
        cy.get('span#select2-courseId-container').click()
        cy.get('.select2-results__option').contains('Diseño y Arquitectura').click()
        cy.get('span#select2-trainerId-container').click();
        cy.get('.select2-results__option').contains('Juan Carlos Jimenez Díaz').click()
        cy.get('input#Classroom_Hours').clear().type('3');
        cy.get('div.note-editable').type('Todo esta bien con el curso')
        cy.get('button.btn.btn-success').click()
    })
  })