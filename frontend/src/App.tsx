import { Suspense, lazy, useEffect, type JSX } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { useAuthStore } from './store/useAuthStore';

// Lazy-loaded components
const Home = lazy(() => import('./Pages/Home'));
const Dashboard = lazy(() => import('./Pages/Dashboard'));
const Products = lazy(() => import('./Pages/Products'));
const NotFound = lazy(() => import('./Pages/NotFound'));

// Protege las rutas privadas
const PrivateRoute = ({ children }: { children: JSX.Element }) => {
  const token = useAuthStore((state) => state.token);
  return token ? children : <Navigate to="/" />;
};

function App() {
  useEffect(() => {
    const token = localStorage.getItem('auth_token');
    if (token) {
      useAuthStore.getState().setAuth(token, null);
    }
  }, []);

  return (
    <Router>
      <Suspense fallback={<div className="p-8 text-center text-lg">Cargando...</div>}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route
            path="/dashboard"
            element={
              <PrivateRoute>
                <Dashboard />
              </PrivateRoute>
            }
          />
          <Route path="/products" element={<Products />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Suspense>
    </Router>
  );
}

export default App;
