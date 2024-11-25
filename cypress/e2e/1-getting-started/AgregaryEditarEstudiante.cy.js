beforeEach(() => {
    cy.visit('https://localhost:7276/')  
})
describe("Agregar un estudiante",()=> {
    it ("verificar la creacion y edicion de los estudiantes",()=>{
        cy.get('a.nav-link.text-dark[href="/Students"]').click()
        cy.get('a.btn.btn-primary[href="/Students/Create"]').click()
        cy.get('input#Dni').type('60035357')
        cy.get('input#FirstName').type('Andrea Yamile')
        cy.get('input#LastName').type('Collasos Grandes')
        cy.get('input#Email').type('Andrea@gmail.com')
        cy.get('input#PhoneNumber').type('923723164')
        cy.get('button.btn.btn-success').click();
    })
  })

  describe("Editar un estudiante",()=> {
    it("Editar un estudiante",()=>{
        cy.get('a.nav-link.text-dark[href="/Students"]').click()
        cy.get('a.btn.btn-sm.btn-success.text-white[href="/Students/Edit/1"]').click();
        cy.get('input#Dni').clear().type('165498732')
        cy.get('input#FirstName').clear().type('daniela')
        cy.get('input#LastName').clear().type('Caipani Gonzales')
        cy.get('input#Email').clear().type('Daniela@gmail.com')
        cy.get('input#PhoneNumber').clear().type('159632847')
        cy.get('button.btn.btn-success').click();
    })
  })

