import {render, screen} from '@testing-library/react'
import '@testing-library/jest-dom';
import React from 'react'
import PokemonDetails from './PokemonDetails'

// The describe function is used to group related tests together into a test suite.
describe('PokemonDetails', () => {

    //Arrange

    // Creating a mock Pokemon object to be used in the test.
    const mockPokemon = {
        name:'charizard',
        sprites: {
            front_default: "my sprite"
        },
        stats: [
            { stat: {name: 'hp'}, base_stat: 78},
            { stat: {name: 'attack'}, base_stat: 84}
        ]
    };


    //Writing test function
    // The test function is used to define a single test case.
    // The first argument is the name of the test, and the second argument is a function that contains the test logic.
    test('renders pokemon details correctly', () => {

        //Act

        // The render function is used to render the component being tested.
        // It renders into a virtual DOM, allowing us to query and interact with the rendered output.
        render(<PokemonDetails pokemon={mockPokemon} />)

        //Assert

        // The screen object is used to query the rendered output.
        // expect is used to make assertions about the rendered output.
        // The toBeInTheDocument matcher is used to check if an element is present in the document.
        expect(screen.getByText('charizard')).toBeInTheDocument();
        
        // The toHaveAttribute matcher is used to check if an element has a specific attribute with a specific value.
        // In this case, we are checking if the image element has the correct src attribute.
        expect(screen.getByAltText('charizard')).toHaveAttribute(
            'src',
            mockPokemon.sprites.front_default
        );

        expect(screen.getByText('hp: 78')).toBeInTheDocument();
        
        expect(screen.getByText('attack: 84')).toBeInTheDocument();

    });

});