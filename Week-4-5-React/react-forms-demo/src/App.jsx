import { useReducer } from 'react'
import axios from 'axios'
import "./App.css"


  //Setting my initial state, for use with my reducer
  //This is either best practice or atleast a very good idea. 
  const initialState = {
    pokemon: null,
    loading: false,
    error: null,
    searchTerm: ""
  }

  //Here is my reducer to handle all my state updates
  function pokemonReducer(state, action) {

    //switch to handle reducer actions
    switch (action.type) {
      
      case "FETCH_START":
        return {
          ...state,
          loading: true,
          error: null,
          pokemon: null
        };

      case "FETCH_SUCCESS":
        return {
          ...state,
          loading: false,
          pokemon: action.payload
        };

      case "FETCH_ERROR":
        return {
          ...state,
          loading: false,
          error: action.payload
        }

      case "SET_SEARCH_TERM":
        return {
          ...state,
          searchTerm: action.payload
        }

      default:
        return state;
    }
  }


function App() {

  //Using our reducer for state management. 
  const [state, dispatch] = useReducer(pokemonReducer, initialState);

  // Deconstructing state
  // This is a common pattern to avoid repeated/repetitive state access.
  // We set a series of consts inside of our component to whatever state is when the component renders
  const { pokemon, loading, error, searchTerm } = state;

  //Handler for form submission
  //"e" in our case, is the form element itself
  async function fetchPokemon(e) {

    //Prevent the default form submission behavior
    e.preventDefault();

    //If our searchbar is empty (or filled with blank spaces)
    //just exit this function. Don't make a request, just exit. 
    if (!searchTerm.trim()) return;

    try {

      //Resetting state for a new search
      dispatch({ type: "FETCH_START"});

      // Axios GET request to PokeAPI
      const response = await axios.get(
        `https://pokeapi.co/api/v2/pokemon/${searchTerm.toLowerCase()}`
      );

      //Transform my API response in a simpler, smaller format
      const pokemonData = {
        name: response.data.name,
        image: response.data.sprites.other["official-artwork"].front_default,
        types: response.data.types.map((t) => t.type.name),
        stats: response.data.stats.map((s) => ({
          name: s.stat.name,
          value: s.base_stat
        }))
      };

      console.log(pokemonData);

      // If we got this far, we've succeeded. Update state accordingly. 
      dispatch( {type: "FETCH_SUCCESS", payload: pokemonData} )

    } catch (err) {
      //If we get any errors, we want to dispatch the FETCH_ERROR reducer action here
      dispatch({
        type: "FETCH_ERROR",
        payload: "Error fetching pokemon"
      });
    }
  };

  
  return (
    <div className='app'>
      <h1>Pokemon Finder</h1>
      {/* Search form with user defined input */}
      <form onSubmit={fetchPokemon}>
        <input 
          type="text" 
          value={searchTerm}
          onChange={(e) => 
            dispatch( { type: "SET_SEARCH_TERM", payload: e.target.value } )
          }
          placeholder="Enter name or ID"
        />
        <button type="submit" disabled={loading}>
          {loading ? "Searching..." : "Search"}
        </button>
      </form>

      {/* Error display */}
      {error && <p className='error'>{error}</p>}
      
      {/* Results display: When we first load our site this wont render, because the user hasn't made a request */}
      {pokemon && (
        <div className='pokemon-card'>
          <h2>{pokemon.name}</h2>
          <img src={pokemon.image} alt={pokemon.name}/>
          <div className='types'>Types: {pokemon.types.join(", ")}</div>
          <div className='stats'>
            <h3>Stats:</h3>
              <ul>
                {pokemon.stats.map((stat) => (
                  <li key={stat.name}>
                    {stat.name}: {stat.value}
                  </li>
                ))}
              </ul>
          </div>
        </div>
      )}
    </div>
  );
}

export default App
