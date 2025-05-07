// src/contexts/AuthContext.tsx
import { createContext, useContext, useState, type ReactNode } from "react";
import { login as loginApi } from "../utils/auth";

interface AuthContextValue {
  token: string | null;
  login: (username: string, password: string) => Promise<void>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [token, setToken] = useState<string | null>(
    () => localStorage.getItem("token")
  );

  const login = async (username: string, password: string) => {
    const tok = await loginApi(username, password);
    setToken(tok);
    // después de guardar el token, todas las llamadas con `api` llevarán la cabecera
  };

  const logout = () => {
    setToken(null);
    localStorage.removeItem("token");
  };

  return (
    <AuthContext.Provider value={{ token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth must be used within AuthProvider");
  return ctx;
}
