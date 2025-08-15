import React from 'react'
import { Link } from 'react-router-dom'

function Navigation() {
    
  return (
    <nav className='nav'>
            <ul>
                <li>
                    <Link to="/">Pokedex</Link>
                </li>
                <li>
                    <Link to='/about'>About</Link>
                </li>
            </ul>
    </nav>
  )
}

export default Navigation