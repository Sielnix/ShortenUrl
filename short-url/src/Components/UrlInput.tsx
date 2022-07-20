import React from "react";

interface UrlInputProps {
    value: string;
    onChange?: (newUrl: string) => void;
    disabled?:boolean;
}

const UrlInput = (props: UrlInputProps) => {
    const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if(props.onChange) {
            props.onChange(e.target.value);
        }
    }

    return (
        <input 
            type="url"
            value={props.value}
            onChange={ onChange }
            placeholder="https://example.com"
            required
            autoFocus
            size={40}
            disabled={props.disabled}/>
    );
}

export default UrlInput;