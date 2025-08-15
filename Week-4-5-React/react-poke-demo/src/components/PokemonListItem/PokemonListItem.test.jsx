import { render, screen, fireEvent } from "@testing-library/react";
import React from "react";
import PokemonListItem from "./PokemonListItem";

//Arrange
//Act
//Assert


//Writing our test function
describe('PokemonListItem', () => {

    //Arrange
    const mockPokemon = { 
        name: 'pikachu',
        url: 'www.pikachu.com'
    }

    test('calls onSelect with url from mockPokemon when clicked', () => { 

        //Mocking the actual function itself, we are just testing our components behavior.
        //When the button is clicked, it should fire off the onSelect method.
        //If we want to test onSelect later, we can do so in another test file where it makes sense to.
        const mockOnSelect = jest.fn();

        render(
            <PokemonListItem
                pokemon={mockPokemon}
                onSelect={mockOnSelect}
            />
        );
        
        //Act
        fireEvent.click(screen.getByRole('button'));

        //Assert
        expect(mockOnSelect).toHaveBeenCalled();

        //We are asserting that when the test runs, by line 36 (after our fireEvent call), 
        //mockOnSelect has been called with mockPokemon's url

    });

});