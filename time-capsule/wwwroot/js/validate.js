function validateSignupForm() {
    let isValid = true;

    const nameRegex = /^[A-Za-z]{2,}$/;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordMinLength = 6;

    let firstName = document.getElementsByName("FirstName")[0];
    let lastName = document.getElementsByName("LastName")[0];
    let email = document.getElementsByName("Email")[0];
    let password = document.getElementsByName("Password")[0];
    let confirmPassword = document.getElementsByName("ConfirmPassword")[0];

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

    if (!firstName.value || !nameRegex.test(firstName.value)) {
        displayError(firstName, "First name must be at least 2 letters and contain only letters.");
    }

    if (!lastName.value || !nameRegex.test(lastName.value)) {
        displayError(lastName, "Last name must be at least 2 letters and contain only letters.");
    }

    if (!email.value || !emailRegex.test(email.value)) {
        displayError(email, "Invalid email format.");
    }

    if (!password.value || password.value.length < passwordMinLength) {
        displayError(password, "Password must be at least 6 characters.");
    }

    if (!confirmPassword.value || confirmPassword.value !== password.value) {
        displayError(confirmPassword, "Passwords do not match.");
    }

    return isValid;
}

function validateLoginForm() {
    let isValid = true;

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordMinLength = 6;

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

    if (!email.value || !emailRegex.test(email.value)) {
        displayError(email, "Invalid email format.");
    }

    if (!password.value || password.value.length < passwordMinLength) {
        displayError(password, "Password must be at least 6 characters.");
    }

    return isValid;
}

document.addEventListener('DOMContentLoaded', function () {
    const signupForm = document.querySelector('form[asp-action="Signup"]');
    if (signupForm) {
        signupForm.addEventListener('submit', function (e) {
            if (!validateSignupForm()) {
                e.preventDefault();
            } else {
                const submitButton = signupForm.querySelector('button[type="submit"]');
                if (submitButton) {
                    submitButton.disabled = true;
                    submitButton.innerText = "Signing up...";
                }
            }
        });
    }

    const loginForm = document.querySelector('form[asp-action="Login"]');
    if (loginForm) {
        loginForm.addEventListener('submit', function (e) {
            if (!validateLoginForm()) {
                e.preventDefault();
            } else {
                const submitButton = loginForm.querySelector('button[type="submit"]');
                if (submitButton) {
                    submitButton.disabled = true;
                    submitButton.innerText = "Logging in...";
                }
            }
        });
    }
});