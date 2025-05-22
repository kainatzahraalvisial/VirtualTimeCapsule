function validateSignupForm() {
    let isValid = true;

    const form = document.querySelector('form');
    if (!form.checkValidity()) {
        $(form).validate();
        return false; 
    }

    const nameRegex = /^[A-Za-z]{2,}$/;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordMinLength = 6;

    let firstName = document.getElementsByName("FirstName")[0];
    let lastName = document.getElementsByName("LastName")[0];
    let email = document.getElementsByName("Email")[0];
    let password = document.getElementsByName("Password")[0];

    function displayError(element, message) {
        let errorSpan = element.nextElementSibling; 
        if (errorSpan && errorSpan.classList.contains('text-danger')) {
            errorSpan.textContent = message;
        } else {
            errorSpan = document.createElement('span');
            errorSpan.className = 'text-danger';
            errorSpan.textContent = message;
            element.parentNode.appendChild(errorSpan);
        }
        isValid = false;
    }

   
    document.querySelectorAll('.text-danger').forEach(span => {
        if (!span.getAttribute('asp-validation-for')) {
            span.textContent = '';
        }
    });

    if (!nameRegex.test(firstName.value)) {
        displayError(firstName, "First name must be at least 2 letters and contain only letters.");
    }

    if (!nameRegex.test(lastName.value)) {
        displayError(lastName, "Last name must be at least 2 letters and contain only letters.");
    }

    if (!emailRegex.test(email.value)) {
        displayError(email, "Invalid email format.");
    }

    if (password.value.length < passwordMinLength) {
        displayError(password, "Password must be at least 6 characters.");
    }

    return isValid;
}