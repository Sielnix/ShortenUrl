import React, { useState } from "react";

interface ResultUrlProps {
    shortcut: string;
}

const ResultUrl = (props: ResultUrlProps) => {
    const [isCopied, setIsCopied] = useState(false);
    const fullUrl = window.location.origin + "/" + props.shortcut;

    const buttonClick = async () => {
        await navigator.clipboard.writeText(fullUrl);
        setIsCopied(true);
    };

    const copiedToClipboardMessage = isCopied ? (<p>
        Copied to clipboard!
    </p>) : null;

    return (
        <div>
            <p>Your shortcut address is:</p>
            <input type="text" readOnly autoFocus value={fullUrl} size={40} onFocus={ e => e.target.select() }/>
            <button onClick={buttonClick}>
                Copy to clipboard
            </button>
            {copiedToClipboardMessage}
        </div>
    );
};

export default ResultUrl;