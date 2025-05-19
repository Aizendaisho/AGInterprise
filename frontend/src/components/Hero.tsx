// src/components/Hero.tsx
import React from 'react';
import { motion } from 'framer-motion';

const Hero = () => (
  <motion.div
    className="hero min-h-screen bg-base-200"
    initial={{ opacity: 0 }}
    animate={{ opacity: 1 }}
    transition={{ duration: 1 }}
  >
    <div className="hero-content text-center">
      <div className="max-w-md">
        <h1 className="text-5xl font-bold">AGInterprise</h1>
        <p className="py-6">Transformando la gestión agrícola con tecnología de vanguardia.</p>
        <button className="btn btn-primary">Comenzar</button>
      </div>
    </div>
  </motion.div>
);

export default Hero;
