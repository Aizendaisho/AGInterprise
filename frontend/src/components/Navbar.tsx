import { Button } from "../components/ui/button";
import { motion } from "framer-motion";
import { Link } from "react-router";

export function Navbar() {
  return (
    <nav className="w-full px-8 py-4 flex justify-between items-center bg-secondary shadow-sm">
      <motion.div
        initial={{ x: -50, opacity: 0 }}
        animate={{ x: 0, opacity: 1 }}
        transition={{ duration: 0.4 }}
      >
        <Link to="/">
          {/* sustituye /logo.png por tu logo real */}
          <img src="/logo.png" alt="AGInterprise Logo" className="h-8 rounded-md" />
        </Link>
      </motion.div>
      <motion.div
        initial={{ x: 50, opacity: 0 }}
        animate={{ x: 0, opacity: 1 }}
        transition={{ duration: 0.4, delay: 0.1 }}
      >
        <Link to="/login">
          <Button variant="outline">Login</Button>
        </Link>
      </motion.div>
    </nav>
  );
}
