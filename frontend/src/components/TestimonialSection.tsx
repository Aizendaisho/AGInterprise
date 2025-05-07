import { motion } from "framer-motion";

export function TestimonialSection() {
  return (
    <section className="py-20 bg-gray-50">
      <div className="max-w-2xl mx-auto px-8 text-center">
        <motion.p
          className="text-xl italic text-gray-700 mb-4"
          initial={{ y: 20, opacity: 0 }}
          whileInView={{ y: 0, opacity: 1 }}
          viewport={{ once: true }}
          transition={{ duration: 0.6 }}
        >
          “AGInterprise ha transformado por completo nuestra forma de trabajar.  
          Nunca fue tan fácil saber qué tenemos en stock y facturar al instante.”
        </motion.p>
        <motion.p
          className="font-semibold text-gray-900"
          initial={{ opacity: 0 }}
          whileInView={{ opacity: 1 }}
          viewport={{ once: true }}
          transition={{ delay: 0.3, duration: 0.6 }}
        >
          — Ana López, CEO de Distribuciones López
        </motion.p>
      </div>
    </section>
  );
}
