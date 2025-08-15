import React from 'react'

function PokemonDetails({pokemon}) {
  return (
    <div className='details'>
        <h2>{pokemon.name}</h2>
        <img src={pokemon.sprites.front_default} alt={pokemon.name} />
        <h3>Stats</h3>
        <ul className='stats'>
            {pokemon.stats.map((stat) => (
                <li key = {stat.stat.name}>
                    {stat.stat.name}: {stat.base_stat}
                </li>
            ))}
        </ul>
    </div>
  )
}

export default PokemonDetails