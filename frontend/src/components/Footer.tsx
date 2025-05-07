import { Link } from "react-router";

export function Footer() {
  return (
    <footer className="py-6 bg-gray-800 text-gray-300">
      <div className="max-w-6xl mx-auto px-8 flex flex-col md:flex-row justify-between items-center">
        <span>© {new Date().getFullYear()} AGInterprise. Todos los derechos reservados.</span>
        <div className="space-x-4 mt-4 md:mt-0">
          <Link to="/" className="hover:underline">Inicio</Link>
          <Link to="/login" className="hover:underline">Login</Link>
        </div>
      </div>
    </footer>
  );
}
