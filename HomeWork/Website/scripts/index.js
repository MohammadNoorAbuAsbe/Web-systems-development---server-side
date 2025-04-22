$(document).ready(SetupPage);

function SetupPage()
{
    renderMovies();
}

function renderMovies() {
    const moviesContainer = $('#moviesContainer');
    const title = $('#title');

    $('#loadBtn').click(function () {
        $(this).hide();
        title.text("All Movies");
        moviesContainer.empty();
        renderMovieCards('Add to Cart', addToCart, moviesContainer, movies);
    });
}

function addToCart(movieId) {
    const movie = movies.find(movie => movie.id === movieId);
    const movieToSend = {
        id: parseInt(movie.id.slice(2), 10),
        url: movie.url,
        primaryTitle: movie.primaryTitle,
        description: movie.description,
        primaryImage: movie.primaryImage,
        year: movie.startYear,
        releaseDate: movie.releaseDate,
        language: movie.language,
        budget: movie.budget || -1,
        grossWorldwide: movie.grossWorldwide || -1,
        genres: Array.isArray(movie.genres) ? movie.genres.join(",") : movie.genres,
        isAdult: movie.isAdult,
        runtimeMinutes: movie.runtimeMinutes,
        averageRating: movie.averageRating,
        numVotes: movie.numVotes
    };
    sendToServer(movieToSend);
}

function sendToServer(movie) {
    ajaxCall(
        'POST',
        urls.movies.addToCart,
        JSON.stringify(movie),
        (response) => handleSuccess(
            response,
            '🎉 Success! The movie has been added to your cart. You can view it in "My Movies".',
            'ℹ️ This movie is already in your cart. No duplicate entries allowed.'
        ),
        (error) => handleError(
            error
        )
    );
}