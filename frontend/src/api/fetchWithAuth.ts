// src/api/fetchWithAuth.ts
export async function fetchWithAuth(input: RequestInfo, init?: RequestInit) {
    const token = localStorage.getItem('jwt');
    const headers = {
      ...(init?.headers || {}),
      Authorization: token ? `Bearer ${token}` : ''
    };
    return fetch(input, { ...init, headers });
  }
  