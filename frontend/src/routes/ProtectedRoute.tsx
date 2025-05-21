import { Navigate } from 'react-router-dom';
import type { JSX } from 'react';
import { useAuthStore } from '../store/useAuthStore';

interface Props {
  children: JSX.Element;
}

export const ProtectedRoute = ({ children }: Props) => {
  const { token, userName, expiresAt, logout } = useAuthStore();

  const isExpired = expiresAt ? Date.now() > new Date(expiresAt).getTime() : true;
console.log("paso por aqui");
  if (!token || !userName || isExpired) {
    logout();
    return <Navigate to="/" />;
  }

  return children;
};
