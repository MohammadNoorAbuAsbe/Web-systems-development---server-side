function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function formatDate(dateString) {
    if (!dateString) return 'N/A';
    const cleanDate = dateString.split('T')[0];
    const [year, month, day] = cleanDate.split('-');
    return `${day}-${month}-${year}`;
}

function getYearFromDate(dateString) {
    if (!dateString) return 'N/A';
    const cleanDate = dateString.split('T')[0];
    const [year] = cleanDate.split('-');
    return year;
}

const getErrorMessage = (error) => {
    if (error?.responseJSON?.message) return error.responseJSON.message;
    if (error?.status) return `Error ${error.status}: ${error.statusText}`;
    return 'Oops! Something went wrong while processing your request.';
};
