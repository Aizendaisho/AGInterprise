// src/components/Footer.tsx
import React from 'react';

const Footer = () => (
  <footer className="footer p-10 bg-base-300 text-base-content">
    <div>
      <span className="footer-title">AGInterprise</span>
      <a className="link link-hover">Acerca de</a>
      <a className="link link-hover">Contacto</a>
      <a className="link link-hover">Privacidad</a>
    </div>
    <div>
      <span className="footer-title">Redes Sociales</span>
      <a className="link link-hover">Twitter</a>
      <a className="link link-hover">Facebook</a>
      <a className="link link-hover">Instagram</a>
    </div>
  </footer>
);

export default Footer;
