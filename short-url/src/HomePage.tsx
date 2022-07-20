import React, { useState } from 'react';
import './App.css';
import UrlInput from './Components/UrlInput';
import { makeRequest } from './Infrastructure/HttpClient';
import ShortenUrlCommand from './Data/ShortenUrlCommand';
import ShortUrlItem from './Data/ShortUrlItem';
import ResultUrl from './Components/ResultUrl';

function isValidHttpUrl(str: string): boolean {
    let url;
    
    try {
      url = new URL(str);
    } catch (_) {
      return false;  
    }
  
    return url.protocol === "http:" || url.protocol === "https:";
  }

function HomePage() {
    const [urlAddress, setUrlAddress] = useState("");
    const [validationMessage, setValidationMessage] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [resultShortcut, setResultShortcut] = useState<string>();
    const [isError, setIsError] = useState(false);

    const onButtonClick = async () => {
        if (!urlAddress || urlAddress === "") {
            setValidationMessage("Provide url address to shorten");
            return;
        }

        if(!isValidHttpUrl(urlAddress)){
            setValidationMessage("Provided url is not valid");
            return;
        }

        setIsLoading(true);

        const command: ShortenUrlCommand = {
            url: urlAddress
        };

        try
        {
            const result = await makeRequest("ShortUrl", "POST", command);
            const resultData = await result.json() as ShortUrlItem;
    
            setResultShortcut(resultData.shortcut);
        } catch {
            setIsError(true);
        } finally {
            setIsLoading(false);
        }
    }

    if (resultShortcut) {
        return (<ResultUrl shortcut={resultShortcut} />);
    }

    if (isError) {
        return (<p>Something went wrong...</p>)
    }

    const onUrlChange = (newUrl: string) => {
        setUrlAddress(newUrl);
        if (newUrl) {
            setValidationMessage("");
        }
    }

    const message = validationMessage ? (
        <p>{validationMessage}</p>
    ) : null;

    return (
        <>
            <form onSubmit={onButtonClick}>
                <p>
                    Welcome to Short Url!
                    Paste your URL below to shorten it.
                </p>

                <UrlInput onChange={onUrlChange} value={urlAddress} disabled={isLoading} />
                <div className="validation-message">
                    {message}
                </div>

                <button type="submit" onClick={onButtonClick} disabled={isLoading}>
                    Shorten
                </button>
            </form>
        </>
    );
}

export default HomePage;
