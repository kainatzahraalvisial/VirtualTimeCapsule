function validateSignupForm() {
    let isValid = true;

    const nameRegex = /^[A-Za-z]{2,}$/;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const passwordMinLength = 6;

    let firstName = document.getElementsByName("FirstName")[0];
    let lastName = document.getElementsByName("LastName")[0];
    let email = document.getElementsByName("Email")[0];
    let password = document.getElementsByName("Password")[0];

    if (!nameRegex.test(firstName.value)) {
        alert("Invalid first name");
        isValid = false;
    }

    if (!nameRegex.test(lastName.value)) {
        alert("Invalid last name");
        isValid = false;
    }

    if (!emailRegex.test(email.value)) {
        alert("Invalid email");
        isValid = false;
    }

    if (password.value.length < passwordMinLength) {
        alert("Password must be at least 6 characters");
        isValid = false;
    }

    return isValid;
}
