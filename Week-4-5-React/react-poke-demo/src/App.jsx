import React from 'react'
import Navigation from './components/Navigation/Navigation'
import Pokedex from './components/Pokedex/Pokedex'
import About from './components/About/About'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import { PokemonProvider } from './context/PokemonContext'


function App() {
  return (
    <PokemonProvider>
        <Router> { /* Wrapping our view in a Router component. Everything inside of these router "tags" can use the router */}
            <div className='app'>
                <Navigation/> {/* rendering my navigation component. I want this to be at the top at all times */}
                <Routes> {/* Inside of the Routes element, I define my routes to my different views */}
                    <Route path="" element={ <Pokedex /> } />
                    <Route path="about" element={ <About />} />
                </Routes>
            </div>
        </Router>
    </PokemonProvider>
  )
}

export default App