import { useNavigate } from "react-router-dom";
import { useAuthStore } from "../store/useAuthStore";
import LightDarkMode from "./LightDarkMode";

const Navbar = () => {
  const token = useAuthStore((state) => state.token);
  const logout = useAuthStore((state) => state.logout);
  const navigate = useNavigate()
    const handleLogout = () => {
    logout()
    navigate('/')
  }

  return (
    <div className="navbar bg-base-100">
      <div className="flex-1">
        <a className="btn btn-ghost text-xl">AGInterprise</a>
      </div>
      <div className="flex-none">
        <LightDarkMode />
        {!token && (
          <button
            className="btn btn-outline"
            onClick={() => (document.getElementById('login_modal') as HTMLDialogElement)?.showModal()}
          >
            Login
          </button>
        )}
                {token && (
          <button
            className="btn btn-outline"
            onClick={() => handleLogout()}
          >
            Logout
          </button>
        )}
      </div>
    </div>
  );
};

export default Navbar;
