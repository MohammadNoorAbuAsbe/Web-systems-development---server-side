$(document).ready(setupListeners);

function setupListeners() {
    const container = document.getElementById('container');
    const openRegister = document.getElementById('openRegister');
    const openLogin = document.getElementById('openLogin');
    const blackCoverText = document.querySelector('.black-cover h1');
    const loginForm = document.getElementById('loginForm');
    const registerForm = document.getElementById('registerForm');
    const continueAsGuestButtons = document.querySelectorAll('.continue-as-guest');

    continueAsGuestButtons.forEach(button => {
        button.addEventListener('click', () => {
            window.location.href = 'index.html';
        });
    });

    openRegister.addEventListener('click', (e) => {
        e.preventDefault();
        container.classList.remove('shift-left');
        container.classList.add('shift-right');
        blackCoverText.textContent = 'Join Us and Start Your Journey!';
        registerForm.classList.add('show');
        loginForm.classList.remove('show');
    });

    openLogin.addEventListener('click', (e) => {
        e.preventDefault();
        container.classList.remove('shift-right');
        container.classList.add('shift-left');
        blackCoverText.textContent = 'Welcome Back, We Missed You!';
        loginForm.classList.add('show');
        registerForm.classList.remove('show');
    });

    $("#loginForm").submit(checkUser);

    $("#registerForm").submit(registerUser);

    // Function to validate and log in the user
    function checkUser(e)
    {
        e.preventDefault();

        const submitButton = $("#loginForm button[type='submit']");
        submitButton.prop("disabled", true); // Disable the button

        const email = $("#loginEmail").val();
        const password = $("#loginPassword").val();
        const url = "https://localhost:7246/api/Users";

        ajaxCall("GET", url, "", function (res) {
            const user = res.find(u => u.name === email && u.password === password);
            if (user) {
                showNotification(`Login successful. Welcome ${user.name}!`, "success");
                $("#loginError").text("");
                window.location.href = "index.html";
            } else {
                showNotification("Invalid Email or Password.", "error");
            }
            submitButton.prop("disabled", false); // Re-enable the button
        }, function (err) {
            console.log(err);
            showNotification("Error contacting server.", "error");
            submitButton.prop("disabled", false); // Re-enable the button
        });

        return false;
    }

    function registerUser()
    {
        const submitButton = $("#registerForm button[type='submit']");
        submitButton.prop("disabled", true);

        const id = $("#createId").val();
        const name = $("#createName").val();
        const email = $("#createEmail").val();
        const password = $("#createPassword").val();

        const user = {
            id: id,
            name: name,
            email: email,
            password: password,
            active: true
        };

        ajaxCall("POST", urls.users.register, JSON.stringify(user), function (res) {
            window.location.replace("index.html");
        }, function (err) {
            const errorMessage = getErrorMessage(err);
            showNotification(errorMessage, "error");
            submitButton.prop("disabled", false);
        });

        return false;
    }
}