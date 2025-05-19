// src/components/Testimonials.tsx
import { motion } from 'framer-motion';

const testimonials = [
  {
    name: "Juan Pérez",
    feedback: "AGInterprise ha revolucionado la forma en que gestiono mi finca.",
  },
  {
    name: "María Gómez",
    feedback: "La interfaz es intuitiva y me ha ahorrado mucho tiempo.",
  },
  {
    name: "Carlos Rodríguez",
    feedback: "Gracias a AGInterprise, mis ventas han aumentado significativamente.",
  },
];

const Testimonials = () => (
  <div className="py-12 bg-base-200">
    <div className="max-w-7xl mx-auto px-4">
      <div className="text-center mb-12">
        <h2 className="text-3xl font-bold">Testimonios</h2>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
        {testimonials.map((testimonial, index) => (
          <motion.div
            key={index}
            className="card bg-base-100 shadow-xl"
            whileHover={{ scale: 1.05 }}
          >
            <div className="card-body">
              <p>"{testimonial.feedback}"</p>
              <h3 className="mt-4 font-bold">{testimonial.name}</h3>
            </div>
          </motion.div>
        ))}
      </div>
    </div>
  </div>
);

export default Testimonials;
