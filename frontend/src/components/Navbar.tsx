// src/components/Navbar.tsx
import React from 'react';
import LightDarkMode from './LightDarkMode';

const Navbar = () => {
  const openModal = () => {
    const modal = document.getElementById('login_modal') as HTMLDialogElement;
    if (modal) {
      modal.showModal();
    }
  };

  return (
    <div className="navbar bg-base-100">
      <div className="flex-1">
        <a className="btn btn-ghost normal-case text-xl">AGInterprise</a>
      </div>
      <div className="flex-none">
        <LightDarkMode />
        <button className="btn btn-primary" onClick={openModal}>
          Login
        </button>
      </div>
    </div>
  );
};

export default Navbar;
