// src/components/LoginModal.tsx
import React, { useState } from 'react';
import { AuthAPI } from '../api/api.client';
import { useAuthStore } from '../store/useAuthStore';
import type { LoginRequest } from '../api/api-types';

const LoginModal = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const setToken = useAuthStore((state) => state.setToken);

  const closeModal = () => {
    const modal = document.getElementById('login_modal') as HTMLDialogElement;
    if (modal) modal.close();
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const request: LoginRequest = {
      username: email,
      password,
    };

    try {
      const response = await AuthAPI.login(request);
      const token = (response.data as { token?: string }).token;

      if (token) {
        setToken(token);
        console.log("Token recibido:", token);
        localStorage.setItem('auth_token', token);
        closeModal();
      } else {
        setError('Token inválido.');
      }
    } catch (err: any) {
      console.error(err);
      setError('Credenciales inválidas o error del servidor.');
    }
  };

  return (
    <dialog id="login_modal" className="modal">
      <div className="modal-box">
        <form method="dialog" className="modal-action absolute right-2 top-2">
          <button className="btn btn-sm btn-circle btn-ghost" onClick={closeModal}>
            ✕
          </button>
        </form>
        <h3 className="font-bold text-lg mb-4">Iniciar Sesión</h3>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="form-control">
            <label className="label">
              <span className="label-text">Correo Electrónico</span>
            </label>
            <input
              type="text"
              placeholder="correo@ejemplo.com"
              className="input input-bordered w-full"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          <div className="form-control">
            <label className="label">
              <span className="label-text">Contraseña</span>
            </label>
            <input
              type="password"
              placeholder="••••••••"
              className="input input-bordered w-full"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          {error && <p className="text-red-500 text-sm">{error}</p>}

          <div className="modal-action">
            <button type="submit" className="btn btn-primary w-full">
              Ingresar
            </button>
          </div>
        </form>
      </div>
    </dialog>
  );
};

export default LoginModal;
