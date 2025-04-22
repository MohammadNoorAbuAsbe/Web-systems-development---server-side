$(document).ready(function () {
    initializePage();
});

function initializePage() {
    retriveFromServer();
    setupFilterHandlers();
    handleLoggedInWelocme();
}

function setupFilterHandlers() {
    const filterTitleInput = $('#filterTitle');
    const filterTitleBtn = $('#filterTitleBtn');
    const startDateInput = $('#startDate');
    const endDateInput = $('#endDate');
    const filterDateBtn = $('#filterDateBtn');

    filterTitleBtn.prop('disabled', true);
    filterDateBtn.prop('disabled', true);

    filterTitleInput.on('input', function () {
        filterTitleBtn.prop('disabled', $(this).val().trim() === '');
    });

    startDateInput.add(endDateInput).on('input', function () {
        filterDateBtn.prop('disabled', startDateInput.val() === '' || endDateInput.val() === '');
    });

    filterTitleBtn.click(() => { console.log(filterTitleInput.val()); filterMoviesByTitle(filterTitleInput.val()) });
    filterDateBtn.click(() => filterMoviesByDate(startDateInput.val(), endDateInput.val()));
    $('#clearFilterBtn').click(clearFilter);
}

function renderMovies(movies) {
    const moviesContainer = $('#moviesContainer');
    moviesContainer.empty();

    if (movies.length === 0) {
        showNotification('No movies found.', 'info');
        return;
    }

    renderMovieCards('Delete', deleteMovie, moviesContainer, movies);
}

function setActiveFilter(filterText) {
    const activeFilter = $('#activeFilter');
    activeFilter.html(`
        <span>${filterText}</span>
        <button id="clearFilterBtn">X</button>
    `).show();

    $('#clearFilterBtn').click(clearFilter);
}

function clearActiveFilter() {
    const activeFilter = $('#activeFilter');
    activeFilter.hide().empty();
}

function clearFilter() {
    $('#filterTitle').val('').trigger('input');
    $('#startDate').val('').trigger('input');
    $('#endDate').val('').trigger('input');
    retriveFromServer();
}

function retriveFromServer() {
    clearActiveFilter();
    ajaxCall('GET', urls.movies.getCart, "", renderMovies, handleError);
}

function deleteMovie(movieId) {
    ajaxCall('DELETE', `${urls.movies.delete}/${movieId}`, "", (response) => {
        handleSuccess(
            response,
            '🎉 Success! The movie has been deleted from your list.',
            'ℹ️ No changes were made. The movie might already be deleted.'
        );
        retriveFromServer();
    }, handleError);
}

function filterMoviesByTitle(title) {
    console.log(title);
    ajaxCall('GET', `${urls.movies.filterByTitle}?title=${title}`, "", function (movies) {
        renderMovies(movies);
        setActiveFilter(`🔍 Filter applied: Movies with the title "${title}"`);
    }, handleError);
}

function filterMoviesByDate(startDate, endDate) {
    ajaxCall('GET', `${urls.movies.filterByDate}?startDate=${startDate}&endDate=${endDate}`, "", function (movies) {
        renderMovies(movies);
        setActiveFilter(`📅 Filter applied: Movies released between ${formatDate(startDate)} and ${formatDate(endDate)}`);
    }, handleError);
}
