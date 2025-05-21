import React, { useState } from 'react';

import { useAuthStore } from '../store/useAuthStore';
import { useNavigate } from 'react-router-dom';
import { AuthAPI } from '../api/auth';

const LoginModal = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const setAuth = useAuthStore((state) => state.setAuth);
  const navigate = useNavigate();

  const closeModal = () => {
    const modal = document.getElementById('login_modal') as HTMLDialogElement;
    if (modal) {
      modal.close();
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await AuthAPI.login({ username: email, password });
      const token = response.data?.token;
      const userName = response.data?.userName;
      const expiresAt = response.data?.expiresAt;

      if (token) {
        // ✅ Guardar token en store y localStorage
        setAuth(token, userName, expiresAt); // puedes usar /me luego para obtener el usuario
        localStorage.setItem('auth_token', token);
        closeModal();
        navigate('/dashboard');
      } else {
        console.error('Token no recibido');
      }
    } catch (error) {
      console.error('Error al iniciar sesión', error);
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
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
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
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>
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
