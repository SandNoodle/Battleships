import React from 'react';
import ReactDOM from 'react-dom';

// Pages
import MainPage from './pages/MainPage/MainPage';

// Global TailwindCSS style
import './index.css';

ReactDOM.render(
  <React.StrictMode>
    <MainPage />
  </React.StrictMode>,
  document.getElementById('root')
);