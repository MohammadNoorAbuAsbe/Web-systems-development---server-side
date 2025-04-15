// Configuration Constants
const PORT = 7246;
const ENDPOINTS = {
    BASE: (port) => `https://localhost:${port}/api/Movies`,
    ADD_TO_CART: (port) => `${ENDPOINTS.BASE(port)}/addToCart`,
    CART: (port) => `${ENDPOINTS.BASE(port)}/cart`,
    DELETE: (port) => ENDPOINTS.BASE(port),
    FILTER_BY_TITLE: (port) => `${ENDPOINTS.BASE(port)}/getByTitle`,
    FILTER_BY_DATE: (port) => `${ENDPOINTS.BASE(port)}/filterByDate`,
};

// URL Constants
const baseURL = ENDPOINTS.BASE(PORT);
const urls = {
    addToCart: ENDPOINTS.ADD_TO_CART(PORT),
    getCart: ENDPOINTS.CART(PORT),
    delete: ENDPOINTS.DELETE(PORT),
    filterByTitle: ENDPOINTS.FILTER_BY_TITLE(PORT),
    filterByDate: ENDPOINTS.FILTER_BY_DATE(PORT),
};