import { ApiClient } from './api-client';
import { fetchWithAuth } from './fetchWithAuth';

export const api = new ApiClient(import.meta.env.VITE_API_URL, { fetch: fetchWithAuth });
