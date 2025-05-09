$(document).ready(function () {
    renderUsers();

    $('#search-input').on('input', function () {
        renderUsers(this.value);
    });
});

function renderUsers(searchText = '') {
    const activeUsersList = document.getElementById('active-users-list');
    const inactiveUsersList = document.getElementById('inactive-users-list');
    const userItemTemplate = document.getElementById('user-item-template');

    activeUsersList.innerHTML = '';
    inactiveUsersList.innerHTML = '';

    ajaxCall("GET", "https://localhost:7246/api/Users", null, function (users) {
        users
            .filter(user => user.name.toLowerCase().includes(searchText.toLowerCase()))
            .sort((a, b) => a.name.localeCompare(b.name))
            .forEach(user => {
                const templateClone = userItemTemplate.content.cloneNode(true);
                const userNameSpan = templateClone.querySelector('.user-name');
                const actionButton = templateClone.querySelector('.user-action-button');

                userNameSpan.textContent = user.name;
                actionButton.dataset.userId = user.id;

                console.log("User:", user.name, "| isActive:", user.active, "| typeof:", typeof user.isActive);

                if (user.active) {
                    actionButton.textContent = 'Deactivate';
                    actionButton.className = 'user-action-button deactivate-btn';
                    activeUsersList.appendChild(templateClone);
                } else {
                    actionButton.textContent = 'Activate';
                    actionButton.className = 'user-action-button activate-btn';
                    inactiveUsersList.appendChild(templateClone);
                }

             

                actionButton.addEventListener('click', function () {
                    const spinner = document.createElement('span');
                    spinner.classList.add('loading-spinner');
                    actionButton.appendChild(spinner);
                    actionButton.disabled = true;
                    const newStatus = !user.active;
                    ajaxCall(
                        "PUT",
                        `https://localhost:7246/api/Users/Update/${user.id}`,
                        JSON.stringify({ ...user, active: newStatus }),
                        function () {
                            renderUsers(searchText); 
                        },
                        function () {
                            alert("Failed to update user status");
                            actionButton.disabled = false;
                            spinner.remove();
                        }
                    );
                });
            });
    }, function () {
        alert("Failed to fetch users");
    });
}



