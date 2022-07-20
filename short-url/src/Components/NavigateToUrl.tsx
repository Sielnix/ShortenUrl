import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import ShortUrlItem from "../Data/ShortUrlItem";
import { makeRequest } from "../Infrastructure/HttpClient";

const NavigateToUrl = () => {
    const params = useParams();

    useEffect(() => {
        (async () => {
            const result = await makeRequest("ShortUrl/" + params.shortenedId, "GET");
            const resultData = await result.json() as ShortUrlItem;
    
            window.location = resultData.url as any;
        })();
    }, []);

    console.log(params);

    return (<div>Redirecting...</div>);
}

export default NavigateToUrl;