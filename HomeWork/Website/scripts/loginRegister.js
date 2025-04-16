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
}