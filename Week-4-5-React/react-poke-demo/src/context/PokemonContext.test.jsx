import { reducer, initialState } from './PokemonContext';
import '@testing-library/jest-dom';


describe('pokemonReducer', () => {

 test('handles FETCH_START action', () => {
  
   const newState = reducer(initialState, { type: 'FETCH_START' });

   expect(newState).toEqual({
     ...initialState,
     loading: true,
     error: null
   });
 });

 test('handles FETCH_LIST_SUCCESS action', () => {
   const mockList = [{ name: 'squirtle' }, { name: 'wartortle' }];
   const newState = reducer(initialState, {
     type: 'FETCH_LIST_SUCCESS',
     payload: mockList
   });

   expect(newState.pokemonList).toEqual(mockList);
   expect(newState.loading).toBe(false);
 });

 test('handles FETCH_ERROR action', () => {
   const error = 'Network error';
   const newState = reducer(initialState, {
     type: 'FETCH_ERROR',
     payload: error
   });

   expect(newState.error).toBe(error);
   expect(newState.loading).toBe(false);
 });

 test
});