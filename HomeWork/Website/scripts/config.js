// Configuration Constants
const CONFIG = {
    localhost: {
        moviesBaseURL: `https://localhost:${7246}/api/Movies`,
        usersBaseURL: `https://localhost:${7246}/api/Users`,
    },
    production: {
        moviesBaseURL: 'https://proj.ruppin.ac.il/cgroup10/test2/tar1/api/Movies',
        usersBaseURL: 'https://proj.ruppin.ac.il/cgroup10/test2/tar1/api/Users',
    },
};

const isLocalHost = ["localhost", "127.0.0.1"].includes(location.hostname);
const MOVIES_SERVER_PATH = isLocalHost ? CONFIG.localhost.moviesBaseURL : CONFIG.production.moviesBaseURL;
const USERS_SERVER_PATH = isLocalHost ? CONFIG.localhost.usersBaseURL : CONFIG.production.usersBaseURL;

// Movies Endpoints
const MOVIES_ENDPOINTS = {
    BASE: () => `${MOVIES_SERVER_PATH}`,
    ADD_TO_CART: () => `${MOVIES_ENDPOINTS.BASE()}/addToCart`,
    CART: () => `${MOVIES_ENDPOINTS.BASE()}/cart`,
    DELETE: () => MOVIES_ENDPOINTS.BASE(),
    FILTER_BY_TITLE: () => `${MOVIES_ENDPOINTS.BASE()}/getByTitle`,
    FILTER_BY_DATE: () => `${MOVIES_ENDPOINTS.BASE()}/filterByDate`,
};

// User Endpoints
const USER_ENDPOINTS = {
    BASE: () => `${USERS_SERVER_PATH}`,
    REGISTER: () => `${USER_ENDPOINTS.BASE()}/register`,
    LOGIN: () => `${USER_ENDPOINTS.BASE()}/login`
};

// URL Constants
const urls = {
    movies: {
        base: MOVIES_ENDPOINTS.BASE(),
        addToCart: MOVIES_ENDPOINTS.ADD_TO_CART(),
        getCart: MOVIES_ENDPOINTS.CART(),
        delete: MOVIES_ENDPOINTS.DELETE(),
        filterByTitle: MOVIES_ENDPOINTS.FILTER_BY_TITLE(),
        filterByDate: MOVIES_ENDPOINTS.FILTER_BY_DATE(),
    },
    users: {
        base: USER_ENDPOINTS.BASE(),
        register: USER_ENDPOINTS.REGISTER(),
        login: USER_ENDPOINTS.LOGIN()
    },
};
