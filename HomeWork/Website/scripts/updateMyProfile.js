$(document).ready(setupListeners);

function setupListeners() {
    const container = document.getElementById('container');
    const loginForm = document.getElementById('loginForm');
    const updateBtn = document.getElementById('updatePassword');
    const newPasswordLabel = document.getElementById('newPassword');

    const continueAsGuestButtons = document.querySelectorAll('.continue-as-guest');

    continueAsGuestButtons.forEach(button => {
        button.addEventListener('click', () => {
            window.location.href = 'index.html';
        });
    });


    updateBtn.addEventListener('click', () => {
        if (newPasswordLabel.classList.contains('show')) {
            newPasswordLabel.classList.remove('show');
        } else {
            newPasswordLabel.classList.add('show');
            updateBtn.textContent = "X";
        }
    })

    function showRelatedForm(containerRemovedClass, containerAddedClass, textContent, formShown, formHidden) {
        updateBtn.classList.add(containerRemovedClass);
        container.classList.add(containerAddedClass);
        blackCoverText.textContent = textContent;
        formShown.classList.add('show');

        return false;
    }

    $("#loginForm").submit(updateUser);

    function updateUser() {
        const submitButton = $("#loginButton");
        submitButton.prop("disabled", true);

        const name = $("#createName").val();
        const email = $("#createEmail").val();
        const password = $("#createPassword").val();
        const newPassword = $("#newPassword").val();

        const user = {
            id: 0,
            name: name,
            email: email,
            password: password,
            active: true
        };

        HandleUserAction(user, submitButton, "🎉 Registration successful! Redirecting to the homepage...", urls.users.update, true);

        return false;
    }

    function HandleUserAction(data, submitButton, notificationText, apiEndpoint, isRegister = false) {
        ajaxCall("PUT", apiEndpoint, JSON.stringify(data), function (res) {
            showNotification(notificationText, "success");
            if (isRegister) {
                saveUser(data)
            }
            else {
                saveUser(res);
            }

            setTimeout(() => {
                window.location.replace("index.html");
            }, 2000);
        }, function (err) {
            const errorMessage = getErrorMessage(err);
            showNotification(errorMessage, "error");
            submitButton.prop("disabled", false);
        });
    }

    function saveUser(userData) {
        const user = { name: userData.name, email: userData.email };
        localStorage.setItem("user", JSON.stringify(user));
    }
}