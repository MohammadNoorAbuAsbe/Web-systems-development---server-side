$(document).ready(function () {
    $(document.body).prepend("<header><nav id=\"navbar\" class=\"guest\"><a href=\"index.html\">Home</a><a href=\"MyMovies.html\" id=\"myMovies\">My Movies</a><a href=\"addMovie.html\" id=\"addMovie\">Add Movies</a><a href=\"login.html\" id=\"loginLink\">Login</a><h3 id=\"userWelcome\">Welcome</h3><button id=\"logoutBtn\">Logout</button></nav></header>");
    redirectToLogin();
    handleLoggedInWelocme();
    handleLogoutBtn();
});

function redirectToLogin() {
    const links = [$('#myMovies'), $('#addMovie')];
    const user = GetLoggedInUser();
    if (!user) {
        links.forEach(link => {
            link.click(function (event) {
                event.preventDefault();
                window.location.href = 'login.html';
            });
        });
    }
}

function handleLoggedInWelocme() {
    const user = GetLoggedInUser();
    const navbar = $('#navbar');
    const userWelcome = $('#userWelcome');
    if (user) {
        navbar.removeClass('guest');
        userWelcome.text(`Welcome ${user.name}!`);
    }
}

function handleLogoutBtn() {
    const logoutBtn = $('#logoutBtn');
    const user = GetLoggedInUser();
    if (user) {
        logoutBtn.show();
        logoutBtn.click(() => {
            localStorage.removeItem("user");
            window.location.replace("index.html");
        });
    } else {
        logoutBtn.hide();
    }

}