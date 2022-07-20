import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import NavigateToUrl from './Components/NavigateToUrl';
import HomePage from './HomePage';
import logo from './logo.svg';

function App() {

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <Router>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/:shortenedId" element={<NavigateToUrl />} />
          </Routes>
        </Router>
      </header>
    </div>

  );

}

export default App;
