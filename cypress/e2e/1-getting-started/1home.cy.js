beforeEach(() => {
  cy.visit('https://localhost:7276/')
  
})
describe("home test",()=> {
  it ("verificar elementos pagina home de carga",()=>{
    cy.get("h2").contains('Conviértete en instructor').should
    cy.get("h2").contains('Cursos más recientes').should
    cy.get("h2").contains("¡Aprende a programar!").should
    cy.get("h2").contains('Habilidades que te ayudan a avanzar').should
  })
})
describe("verificar curso",()=> {
  it ("verificar el contenido de un curso al darle click",()=>{
    cy.get('a[href="/Courses/Details/1"]').click()
     })
 })  




