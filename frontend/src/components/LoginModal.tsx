// src/components/LoginModal.tsx
import React from 'react';

const LoginModal = () => {
  const closeModal = () => {
    const modal = document.getElementById('login_modal') as HTMLDialogElement;
    if (modal) {
      modal.close();
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    console.log("Ledieron al modal")
    e.preventDefault();
    // Aquí puedes agregar la lógica de autenticación
    closeModal();
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
              type="email"
              placeholder="correo@ejemplo.com"
              className="input input-bordered w-full"
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
              required
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
