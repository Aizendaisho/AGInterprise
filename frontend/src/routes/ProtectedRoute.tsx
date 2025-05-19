import { Navigate } from 'react-router-dom';

import type { JSX } from 'react';
import { useAuthStore } from '../store/useAuthStore';

interface Props {
  children: JSX.Element;
}

export const ProtectedRoute = ({ children }: Props) => {
  const token = useAuthStore((state) => state.token);
  return token ? children : <Navigate to="/" />;
};
