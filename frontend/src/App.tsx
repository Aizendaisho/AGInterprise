import { BrowserRouter, Route, Routes } from 'react-router';
import './App.css'
import { LoginPage } from './pages/LoginPage';
import HomePage from './pages/HomePage';

function App() {
  


  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomePage />} index />
        <Route path="/login" element={<LoginPage />} />
        {/* aquí tus demás rutas protegidas */}
      </Routes>
    </BrowserRouter>
  )
}

export default App
