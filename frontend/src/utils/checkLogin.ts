import { useNavigate } from "react-router-dom";
import { useAuthStore } from "../store/useAuthStore";

function checkTokenExpiration() {
      const navigate = useNavigate();
  const expiresAt = localStorage.getItem('auth_expiresAt');
  if (expiresAt && Date.now() > new Date(expiresAt).getTime()) {
    useAuthStore.getState().logout();
    // Redirige al login o pantalla principal
    navigate('/');
  }
}
export default checkTokenExpiration;