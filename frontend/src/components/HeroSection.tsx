import { Button } from "../components/ui/button";
import { motion } from "framer-motion";

export function HeroSection() {
  return (
    <section className="flex flex-col items-center justify-center text-center py-24 bg-gray-50">
      <motion.h1
        className="text-5xl font-extrabold mb-4"
        initial={{ y: -30, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ duration: 0.6 }}
      >
        Bienvenido a AGInterprise
      </motion.h1>
      <motion.p
        className="text-lg text-gray-600 mb-8 max-w-xl"
        initial={{ y: 30, opacity: 0 }}
        animate={{ y: 0, opacity: 1 }}
        transition={{ duration: 0.6, delay: 0.2 }}
      >
        La plataforma todo-en-uno para gestionar inventarios, facturación y ventas de
        manera sencilla y eficiente.
      </motion.p>
      <motion.div
        initial={{ scale: 0.9, opacity: 0 }}
        animate={{ scale: 1, opacity: 1 }}
        transition={{ duration: 0.6, delay: 0.4 }}
      >
        <Button size="lg">Comenzar ahora</Button>
      </motion.div>
    </section>
  );
}
