import React from 'react'
import Footer from '../Components/Footer'
import Hero from '../Components/Hero'
import Navbar from '../Components/Navbar'
import Testimonials from '../Components/Testimonials'
import Features from '../Components/Features'
import LoginModal from '../Components/LoginModal'

export default function Home() {
  return (
    <div>
      <Navbar />
      <LoginModal />
      <Hero />
      <Features />
      <Testimonials />
      <Footer />
    </div>
  )
}
