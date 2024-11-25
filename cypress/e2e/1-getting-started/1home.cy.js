beforeEach(() => {
  cy.visit('https://localhost:7276/')
  
})
describe("home test",()=> {
  it ("verificar elementos pagina home de carga",()=>{
    cy.get("h2").contains('Conviértete en instructor').should('exist');
    cy.get("h2").contains('Cursos más recientes').should('exist');
    cy.get("h2").contains("¡Aprende a programar!").should('exist');
    cy.get("h2").contains('Habilidades que te ayudan a avanzar').should('exist');
  })
})
describe("verificar curso",()=> {
  it ("verificar el contenido de un curso al darle click",()=>{
    cy.get('a[href="/Courses/Details/1"]').click()
     })
 })  
 describe("verificar carousel",()=> {
  it ("verificar el contenido de un curso al darle click",()=>{
    cy.get('.carousel-button.next').click();
     })
 })




