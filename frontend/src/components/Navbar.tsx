import { useNavigate } from "react-router-dom";
import { useAuthStore } from "../store/useAuthStore";
import LightDarkMode from "./LightDarkMode";
import imagen from "../assets/login-user-name-1.png";

const Navbar = () => {
  const token = useAuthStore((state) => state.token);
  const logout = useAuthStore((state) => state.logout);
  const userName = useAuthStore((state) => state.userName);
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
        {token && (
          <div className="dropdown dropdown-end">
            <label tabIndex={0} className="btn btn-ghost btn-circle avatar bg-white">
              <div className="w-10 rounded-full">
                <img src={imagen}  />
              </div>
            </label>
            <ul
              tabIndex={0}
              className="mt-3 p-2 shadow menu menu-sm dropdown-content bg-base-100 rounded-box w-52"
            >
              <li>
                <a>{userName}</a>
              </li>
            </ul>
          </div>
        )}
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
