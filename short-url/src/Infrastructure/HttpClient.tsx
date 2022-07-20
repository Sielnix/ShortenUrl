import { getApiUrl } from "./ApiUrlProvider";

const makeRequest = (url: string, method: "GET" | "POST", data?: object) => {
    const apiEndpoint = getApiUrl();

    let headers: HeadersInit = {};
    headers['Content-Type'] = 'application/json';
    headers['accept'] = '*/*';
        
    let requestInit : RequestInit = {
        method: method,
        headers: headers
    }

    if (data) {
        requestInit.body = JSON.stringify(data);
    }

    return fetch(apiEndpoint + url, requestInit);
}

export { makeRequest };