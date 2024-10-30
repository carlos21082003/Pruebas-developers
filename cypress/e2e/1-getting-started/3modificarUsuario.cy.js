beforeEach(() => {
    cy.visit('https://localhost:7276/')
    
  })

describe("modificar usuario",()=> {
    it ("verificar y editar los datos del usuario creado",()=>{
        //modificar perfil
        cy.get('a#register').click()
        cy.get('a#login').click()
        cy.get('input#Input_Email').type('Juan@gmail.com')
        cy.get('input#Input_Password').type('Usuario123*')
        cy.get('button#login-submit').click()
        cy.get('a#manage').click()
        cy.get('input#Input_PhoneNumber').clear().type('123456987')
        cy.get('button#update-profile-button').click()
        //modificar contraseña 
        cy.get('a#change-password').click()
        cy.get('input#Input_OldPassword').clear().type('Usuario123*')
        cy.get('input#Input_NewPassword').clear().type('Usuario1234*')
        cy.get('input#Input_ConfirmPassword').clear().type('Usuario1234*')
        cy.contains('button', 'Update password').click();
        //descargar datos personales
        cy.get('a#personal-data').click()
        cy.contains('button', 'Download').click();

    })
})  