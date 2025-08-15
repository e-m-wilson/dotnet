import React from "react";
import PokemonListItem from "../PokemonListItem/PokemonListItem";

function PokemonList({ pokemonListFromApp, onSelect }) {

  return (
    <div className="list-container">
      <h2>Pokemon List</h2>
      <ul>
        {pokemonListFromApp.map((pokemonInList) => (
          <PokemonListItem
            key={pokemonInList.name}
            pokemon={pokemonInList}
            onSelect={() => onSelect(pokemonInList.url)}
          />
        ))}
      </ul>
    </div>
  );
}

export default PokemonList;

