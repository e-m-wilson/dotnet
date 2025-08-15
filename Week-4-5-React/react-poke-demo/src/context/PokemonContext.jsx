
import { createContext, useReducer, useContext, act, useCallback } from "react";

//Initial shape and values for our Pokemon state data - this is for our context
const initialState = {
    pokemonList: [], //Stores the list of pokemon names/urls from pokeapi return
    selectedPokemon: null, //Stores details about my currently selected pokemon from when a user clicks its button 
    loading: false, //Tracks if my async operation is still in progress (i.e. am I still loading in data?)
    error: null // Stores error messages if anything goes wrong
}


// Setting up possible actions that can update our state, this is for our Reducer
const ActionTypes = {
    FETCH_START: "FETCH_START", //Marks the start of my fetch request
    FETCH_LIST_SUCCESS: "FETCH_LIST_SUCCESS", // Handles successful fetching of pokemon list from pokeapi
    FETCH_POKEMON_SUCCESS: "FETCH_POKEMON_SUCCESS", // Handles successful fetching of pokemon details from pokeapi
    FETCH_ERROR: "FETCH_ERROR" // Handles API request Errors
}

//Here we are writing our reducer, think of it as a switch statement for possible states
const reducer = (state, action) => {

    switch (action.type) {

        case ActionTypes.FETCH_START:
            //Start loading and clear any previous errors
            return { ...state, loading: true, error: null};
        
        case ActionTypes.FETCH_LIST_SUCCESS:
            //Update the pokemon list, and indicate we are no longer loading
            return {...state, loading: false, pokemonList: action.payload};

        case ActionTypes.FETCH_POKEMON_SUCCESS:
            //Update the selected pokemon and finish loading after the user clicks on its button to select it 
            console.log(action.payload);
            return {... state, loading: false, selectedPokemon: action.payload};

        case ActionTypes.FETCH_ERROR:
            // Store error message for display later (potentially) and finish loading
            return {...state, loading: false, error: action.payload};

        default: 
            // If we somehow hit this, just return state as it is and don't update it.
            return state;
    }       
}

//Creating a context object that holds our state and associated functions
const PokemonContext = createContext();

//Defining a context Provider component that manages state and provides data to all children nested inside of it

export function PokemonProvider( {children} ) {

    // Initialize state management with useReducer
    const [ state, dispatch] = useReducer( reducer , initialState); //We need to pass in our reducer function and the initial state value

    //We want to write a function to fetch our pokemon list
    //we will use another react hook, called useCallback to prevent this function from firing when not needed
    //This will prevent infinite re-render loops
    //Technically, useCallback "memoizes" the call of this function.
    const fetchPokemonList = useCallback(async () => {

        //Essentially what we are doing, is wrapping the actual logic of our function
        //Inside of usecallback
        dispatch({type: ActionTypes.FETCH_START}); //Leveraging our reducer, and calling our first action
        
        //Below, we will actually make our fetch request as well as handle any errors 
        try {
            const response = await fetch("https://pokeapi.co/api/v2/pokemon?limit=20"); //sending fetch

            //If for some reason my response is not a success code, I will manually throw an error to make sure
            //I hit that FETCH_ERROR state in my reducer
            if (!response.ok) throw new Error("Failed to get Pokemon list from API");

            const data =  await response.json(); //deserializing my json

            //If we got this far, we have our name+url list, we call dispatch to update state
            //We pass it the appropriate action type, and set the payload to our deserialized js object with our list
            dispatch({type: ActionTypes.FETCH_LIST_SUCCESS, payload: data.results});
        } catch (error) {
            //If something goes wrong, update our state to essentially the "error state"
            dispatch({type: ActionTypes.FETCH_ERROR, payload:error.message});
        }

    }, []); //Empty dependency array to avoid infinite loops, same as useEffect()

    //Function to fetch the details for a specific pokemon (i.e. once the user has clicked it's button in the UI)
    const selectPokemon = useCallback(async (url) => {

        dispatch({type: ActionTypes.FETCH_START}); //Leveraging our reducer, and calling our first action

        try {
            const response = await fetch(url);
            if (!response.ok) throw new Error("Failed to get Pokemon details");

            const data =  await response.json(); //deserializing my json

            console.log(data)

            //If we got this far, we have our detailed pokemon object
            //We pass it the appropriate action type, and set the payload to our deserialized js object with selected pokemon
            dispatch({type: ActionTypes.FETCH_POKEMON_SUCCESS, payload: data});


        } catch (error) {
            //If something goes wrong, update our state to essentially the "error state"
            dispatch({type: ActionTypes.FETCH_ERROR, payload:error.message});
        }

    }, []); //Empty dependency array to avoid infinite loops, same as useEffect()

    //Since this is a functional component, we need a return
    //Remember that "prop" called children that the context takes in
    //We are going to use it here
    return (
        <PokemonContext.Provider 
            value={{
                //Spread the current state with the ... spread operator
                ...state,
                fetchPokemonList,
                selectPokemon,
            }}>
                {children}
        </PokemonContext.Provider>
    );
}

//Creating a custom hook, for easy access to the Pokemon Context
export const usePokemon = () => {
    const context = useContext(PokemonContext);

    if(!context) {
        throw new Error("usePokemon must be used within a PokemonProvider");
    }

    return context;
};

export {
    reducer,
    initialState,
    ActionTypes
}