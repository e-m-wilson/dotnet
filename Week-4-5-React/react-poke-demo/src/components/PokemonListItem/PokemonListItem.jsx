import React from 'react'

function PokemonListItem( {pokemon, onSelect} ) {
  return (
   <li className='pokemon-list-item'>
        <button onClick={onSelect}>{pokemon.name}</button>
   </li>
  )
}

export default PokemonListItem