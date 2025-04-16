// Configuration Constants
const CONFIG = {
    localhost: {
        baseURL: `https://localhost:${7246}/api/Movies`,
    },
    production: {
        baseURL: 'https://proj.ruppin.ac.il/cgroup10/test2/tar1/api/',
    },
};

const isLocalHost = ["localhost", "127.0.0.1"].includes(location.hostname);
const SERVER_PATH = isLocalHost ? CONFIG.localhost.baseURL : CONFIG.production.baseURL;


const ENDPOINTS = {
    BASE: () => `${SERVER_PATH}`,
    ADD_TO_CART: () => `${ENDPOINTS.BASE()}/addToCart`,
    CART: () => `${ENDPOINTS.BASE()}/cart`,
    DELETE: () => ENDPOINTS.BASE(),
    FILTER_BY_TITLE: () => `${ENDPOINTS.BASE()}/getByTitle`,
    FILTER_BY_DATE: () => `${ENDPOINTS.BASE()}/filterByDate`,
};

// URL Constants
const baseURL = ENDPOINTS.BASE();
const urls = {
    addToCart: ENDPOINTS.ADD_TO_CART(),
    getCart: ENDPOINTS.CART(),
    delete: ENDPOINTS.DELETE(),
    filterByTitle: ENDPOINTS.FILTER_BY_TITLE(),
    filterByDate: ENDPOINTS.FILTER_BY_DATE(),
};