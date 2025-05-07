// src/utils/auth.ts
const API_BASE = import.meta.env.VITE_API_URL; // p. ej. http://localhost:3000

export async function login(username: string, password: string): Promise<string> {
  const res = await fetch(`${API_BASE}/api/Auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password }),
  });

  if (!res.ok) {
    const err = await res.text();
    throw new Error(err || "Login failed");
  }

  // El body es el token JWT en texto plano
  const token = await res.text();
  // Guárdalo en localStorage
  localStorage.setItem("token", token);
  return token;
}
