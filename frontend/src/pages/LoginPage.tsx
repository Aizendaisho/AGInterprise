// src/pages/LoginPage.tsx
import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";

export function LoginPage() {
  const { login } = useAuth();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log(username, password);
    try {
      await login(username, password);
      // redirige, p.ej. router.navigate("/dashboard")
    } catch (err: any) {
      setError(err.message);
    }
  };

  return (
    <form onSubmit={onSubmit} className="space-y-4">
      {error && <div className="text-red-500">{error}</div>}
      <input
        className="w-full p-2 border"
        placeholder="Usuario"
        value={username}
        onChange={e => setUsername(e.target.value)}
      />
      <input
        className="w-full p-2 border"
        type="password"
        placeholder="Contraseña"
        value={password}
        onChange={e => setPassword(e.target.value)}
      />
      <button className="px-4 py-2 bg-blue-600 text-white">Entrar</button>
    </form>
  );
}
