// src/components/Features.tsx
import React from 'react';
import { motion } from 'framer-motion';

const features = [
  {
    title: "Gestión de Inventario",
    description: "Controla tus productos y almacenes en tiempo real.",
  },
  {
    title: "Facturación Eficiente",
    description: "Genera y administra facturas de manera sencilla.",
  },
  {
    title: "Análisis de Datos",
    description: "Obtén insights para mejorar tu producción.",
  },
];

const Features = () => (
  <div className="py-12 bg-base-100">
    <div className="max-w-7xl mx-auto px-4">
      <div className="text-center mb-12">
        <h2 className="text-3xl font-bold">¿Qué ofrece AGInterprise?</h2>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
        {features.map((feature, index) => (
          <motion.div
            key={index}
            className="card bg-base-200 shadow-xl"
            whileHover={{ scale: 1.05 }}
          >
            <div className="card-body">
              <h2 className="card-title">{feature.title}</h2>
              <p>{feature.description}</p>
            </div>
          </motion.div>
        ))}
      </div>
    </div>
  </div>
);

export default Features;
