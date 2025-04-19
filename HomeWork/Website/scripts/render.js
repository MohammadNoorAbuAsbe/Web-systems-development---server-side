function renderMovieCards(
    buttonText,
    onButtonClick,
    container,
    movies
) {
    const user = GetLogedInUser();
    // Use a document fragment to improve performance by minimizing DOM reflows
    const fragment = document.createDocumentFragment();

    movies.forEach(movie => {
        const movieCard = $('<div>').addClass('movie-card');
        const movieImage = $('<img>')
            .attr('src', movie.primaryImage)
            .attr('alt', movie.primaryTitle);

        const movieTitle = $('<h3>').text(`${movie.primaryTitle} (${getYearFromDate(movie.releaseDate)})`);
        const movieDescription = $('<p>').text(movie.description);

        const ratingBadge = createMovieBadge(movie.averageRating, 'rating-badge', '⭐');
        const votesBadge = createMovieBadge(`${numberWithCommas(parseInt(movie.numVotes))} Votes`, 'votes-badge');
        const runtimeBadge = createMovieBadge(`${movie.runtimeMinutes} min`, 'runtime-badge');

        const genresContainer = $('<div>').addClass('genres-container')
            .append(createGenreChips(movie.genres));

        const primaryContent = $('<div>').addClass('primary-content').append(
            movieImage,
            movieTitle,
            ratingBadge,
            genresContainer,
            runtimeBadge,
            votesBadge
        );

        const secondaryContent = $('<div>').addClass('secondary-info').append(
            movieDescription,
            createDetailElement('Language', movie.language),
            createDetailElement('For Adults?', movie.isAdult ? 'Yes' : 'No'),
            createDetailElement('Release Date', movie.releaseDate ? formatDate(movie.releaseDate) : 'N/A'),
            createDetailElement('Budget', movie.budget && movie.budget !== -1 ? numberWithCommas(parseInt(movie.budget)) : 'N/A'),
            createDetailElement('Gross', movie.grossWorldwide && movie.grossWorldwide !== -1 ? numberWithCommas(parseInt(movie.grossWorldwide)) : 'N/A')
        );

        const actionButton = $('<button>').addClass('btn')
            .text(buttonText)
            .on('click', () => {
                if (!user) {
                    window.location.href = 'login.html';
                } else {
                    onButtonClick(movie.id);
                }
            });
    
        movieCard.append(primaryContent, secondaryContent, actionButton);
        fragment.appendChild(movieCard[0]);
    });

    // Append all movie cards to the container in one operation for better performance
    $(container).append(fragment);
}