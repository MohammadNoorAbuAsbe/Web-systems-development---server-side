const port = 7246;
const baseURL = `https://localhost:${port}/api/Movies`
const urlAddToCart = baseURL + "/addToCart";
const urlGetCart = baseURL + "/cart";
const urlDelete = baseURL;
const urlFilterByTitle = baseURL + "/getByTitle";
const urlFilterByDate = baseURL + "/filterByDate";

function renderMovieCards(buttonText, buttonFunction, moviesContainer, moviesList)
{
    moviesList.forEach(function (movie)
    {
        const movieCard = $('<div>').addClass('movie-card');
        const movieImage = $('<img>').attr('src', movie.primaryImage).attr('alt', movie.primaryTitle);
        const movieTitle = $('<h3>').text(`${movie.primaryTitle} (${movie.startYear})`);
        const movieDescription = $('<p>').text(movie.description);
        const movieDetails = $('<div>').addClass('movie-details');

        const details =
            [
                { label: 'Release Date', value: movie.releaseDate },
                { label: 'Language', value: movie.language },
                { label: 'Budget', value: movie.budget },
                { label: 'Gross Worldwide', value: movie.grossWorldwide },
                { label: 'Genres', value: Array.isArray(movie.genres) ? movie.genres.join(",") : movie.genres },
                { label: 'Adult', value: movie.isAdult },
                { label: 'Runtime', value: `${movie.runtimeMinutes} minutes` },
                { label: 'Rating', value: movie.averageRating },
                { label: 'Votes', value: movie.numVotes }
            ];

        details.forEach(detail => {
            const detailElement = $('<div>').addClass('movie-detail');
            const detailLabel = $('<strong>').text(detail.label);
            const detailValue = $('<span>').text(detail.value);
            detailElement.append(detailLabel, detailValue);
            movieDetails.append(detailElement);
        });

        const button = $('<button>').addClass('btn').data('id', movie.id).text(buttonText).click(function () {
            buttonFunction(movie.id)
        });

        movieCard.append(movieImage, movieTitle, movieDescription, movieDetails, button);
        moviesContainer.append(movieCard);
    });
}

function showNotification(message, type) {
    const notification = $('#notification');
    notification
        .removeClass('success error info')
        .addClass(type)
        .text(message)
        .fadeIn();

    notification.attr('tabindex', '-1').focus();
    notification[0].scrollIntoView({ behavior: 'smooth', block: 'center' });

    setTimeout(() => {
        notification.fadeOut();
    }, 5000);
}

function handleSuccess(response, successMessage, infoMessage) {
    if (response) {
        showNotification(successMessage, 'success');
    } else {
        showNotification(infoMessage, 'info');
    }
}

function handleError(error) {
    let errorMessage = '❌ Oops! Something went wrong while processing your request.';

    if (error && error.responseJSON && error.responseJSON.message) {
        errorMessage = `❌ Error: ${error.responseJSON.message}`;
    } else if (error && error.status) {
        errorMessage = `❌ Error ${error.status}: ${error.statusText}`;
    }

    showNotification(errorMessage, 'error');
}