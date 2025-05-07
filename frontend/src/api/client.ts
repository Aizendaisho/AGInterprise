// src/api/client.ts
import { ApiClient } from "./api-client";

export class AuthenticatedApiClient extends ApiClient {
  // override del hook que NSwag genera
  protected async transformOptions(options: RequestInit): Promise<RequestInit> {
    const token = localStorage.getItem("token");
    if (token) {
      options.headers = {
        ...options.headers,
        Authorization: `Bearer ${token}`,
      };
    }
    return options;
  }
}

// Y exportas una instancia
export const api = new AuthenticatedApiClient(
  import.meta.env.VITE_API_BASE
);
