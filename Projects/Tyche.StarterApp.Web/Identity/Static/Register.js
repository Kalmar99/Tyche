

class Register {
    constructor(formElementId, errorElementId) {
        this.formElement = document.getElementById(formElementId);
        this.formElement.addEventListener("onsubmit", this.onSubmit)
        this.errorElement = document.getElementById(errorElementId);
        this.registerEndpoint = '/api/identity/register';
        console.log("init:", this.formElement)
    }
    
    renderError(message) {
        this.formElement.children[0].innerText = message;
        this.formElement.classList.toggle('visible')
    }
    
    async register(dto) {
        const response = await fetch(registerEndpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dto)
        })
        
        if(response.status !== 204) {
            this.renderError("Failed to register, please try again later")
        }
    }
    
    async onSubmit(event) {
        const name = ""
        const email = ""
        const password = ""
        
        event.preventDefault();
        
        console.log(event);
    }
}