//import { useState } from 'react' //In order to use State, we import useState from React.
import { useEffect } from 'react';
import { usePokemon } from '../../context/PokemonContext'; // Importing our custom usePokemon hook!

import PokemonList from '../PokemonList/PokemonList'
import PokemonDetails from '../PokemonDetails/PokemonDetails';
import '../../App.css'

function Pokedex() {

  //Since we know we will have information that we want to share with child components, we are going
  //to need to use State. In order to leverage State in our react component, we need to introduce a hook.
  //In our case, the useState hook. 

  //Functionality goes inside the rest of the function definition.

  // State management via useState hook.
  //State needs a state variable, and a setter function for it
  //We never want to directly mutate the state variable. 
  //When we call a state setter function, we will trigger a re-render of the component
  // const [pokemonList, setPokemonList] = useState([]); 
  // const [selectedPokemon, setSelectedPokemon] = useState(null);

  //Access the pokemon context, its state values AND it's potential functionality (methods) via our custom usePokemon hook
  const {
    pokemonList,
    selectedPokemon,
    fetchPokemonList,
    selectPokemon,
  } = usePokemon();


  //Use Effect call below = for fetching our list of pokemon from pokeapi
  //This new version (old version preserved below) of our useEffect leverages our context that 
  // we took in with our hook
  useEffect( () => {
    fetchPokemonList();
  }, [fetchPokemonList]); //Safe dependency created and memoized in our context provider

  //RIP handleSelect() - implementation preserved below

    
  //Rendering stuff goes in the return
  return (

    <div className='container'>
      {/* Pokemon List section */}
      <PokemonList
        pokemonListFromApp={pokemonList} //passing in our list from our context (that came in via usePokemon)
        onSelect={selectPokemon} // passing selection handler from context
      />
      {/* Pokemon Details section */}
      <div className='details-container'>
        {/* Conditionally render details component ONLY if a pokemon has been selected */}
        { selectedPokemon && (
          <PokemonDetails
            pokemon={selectedPokemon}
          />
        )}
      </div>
    </div>
  );
}

export default Pokedex




//  //Use Effect call below = for fetching our list of pokemon from pokeapi
//  useEffect( () => {
//   const fetchInitialPokemon = async () => {
//     try{
//       //Fetch first 20 pokemon from pokeApi
//       const response = await fetch("https://pokeapi.co/api/v2/pokemon?limit=20"); //sending fetch
//       const data =  await response.json(); //deserializing my json
//       setPokemonList(data.results); //take the object (list of pokemon), stick it into state
//     } catch (err) {
//       console.log(err);
//     }
//   };
//   fetchInitialPokemon();
// }, []);//UseEffect takes two arguments, the effect (function you want to have run on component mounting)
//       // And an array of dependencies. These can be anything you need them to be. Its super common to have none.
//       // Even when you have none, don't forget the array. The effect triggers forever. 


// async function handleSelect (url) { //This function will ultimately be called from my PokemonListItem
//   const response = await fetch(url); //But I put it here so it can set state inside of app
//   const data = await response.json(); //Alternatively, I could get really creative with my SelectedPokeon setter function
//   setSelectedPokemon(data);
// }

// return (

//   <div className='app'>
//     <h1>Pokedex</h1>
//     <div className='container'>
//       <PokemonList pokemonListFromApp={pokemonList} onSelect={handleSelect}></PokemonList>
//       {/*Alongside my pokemonList component, I want to have details about my selected pokemon*/}
//       <div className='details-container'>
//         {selectedPokemon && <PokemonDetails pokemon={selectedPokemon}/>}
//       </div>
//     </div>
//   </div>

// )