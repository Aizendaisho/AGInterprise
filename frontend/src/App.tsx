import  { Suspense, lazy, useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { useAuthStore } from './store/useAuthStore';





function App() {
  useEffect(() => {
  const token = localStorage.getItem('auth_token');
  if (token) {
    useAuthStore.getState().setAuth(token, null); // podrías luego obtener el usuario con /me
  }
}, []);
const Home = lazy(() => import('./Pages/Home'));
const Dashboard = lazy(() => import('./Pages/Dashboard'));
const Products = lazy(() => import('./Pages/Products'));
const NotFound = lazy(() => import('./Pages/NotFound'));
  return (
    <Router>
      <Suspense fallback={<div className="flex justify-center items-center h-screen">Cargando...</div>}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/products" element={<Products />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Suspense>
    </Router>
  );
}




export default App;
