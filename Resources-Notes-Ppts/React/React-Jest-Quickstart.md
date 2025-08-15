# React Jest testing quickstart

[Pulling from this guide.](https://dev.to/griseduardo/setup-jest-babel-e-testing-library-for-unit-testing-in-react-4pim)

## 1. Install Packages

Babel packages

```bash
    npm install @babel/core @babel/preset-env @babel/preset-react babel-jest --save-dev
```

Jest packages

```bash
    npm install jest jest-environment-jsdom --save-dev
```

React testing packages

```bash
    npm install @testing-library/react @testing-library/jest-dom @testing-library/user-event --save-dev
```

## 2. Create config files

jest.config.js

```js
export default {
    testEnvironment: "jsdom",
};
```

babel.config.js

```js
export default {
    presets: [
      "@babel/preset-env",
      "@babel/preset-react",
    ],
};
```

## 3. Add script in package.json

```json
"scripts": {
    "test": "jest --coverage"
}
```

Sample abbreviated package.json below showing position.

```js
{
    "name": "react-poke-demo",
    "private": true,
    "version": "0.0.0",
    "type": "module",
    "scripts": {
        "dev": "vite",
        "build": "vite build",
        "lint": "eslint .",
        "preview": "vite preview",
        //Add this line --coverage changes the cli output to be more verbose
        "test": "jest --coverage"
    },
    "dependencies": {
        "jest-dom": "^4.0.0",
        "jest-environment-jsdom": "^29.7.0",
        "react": "^19.0.0",
        "react-dom": "^19.0.0",
        "react-router-dom": "^7.5.3"
    },
    //...rest of this file
}
```

## 4. Create your tests

The general format is [NameOfComponentBeingTested].test.jsx.

So a file for testing my navigation component would be ```Navigation.test.jsx```.

Below is a sample .test.jsx file from demo. [Checkout the official react testing library docs for more info!](https://testing-library.com/docs/react-testing-library/example-intro)

```jsx
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
    // The first argument is the name of the test, and the second argument is a function 
    // that contains the test logic.
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
```

## 5. Run your tests

Use the command ```npm test``` to run your tests. It should pick up all your tests accross your application and run them. Since we used the ```--coverage``` flag inside of our package.json script alias, we will get more information about our tests when they run. It also creates a ```coverage``` directory in the root of your project! Check it out!

```bash
> react-poke-demo@0.0.0 test
> jest --coverage

 PASS  src/components/PokemonListItem/PokemonListItem.test.jsx                                                                           
 PASS  src/context/PokemonContext.test.jsx
 PASS  src/components/PokemonDetails/PokemonDetails.test.jsx
----------------------------|---------|----------|---------|---------|-------------------------
File                        | % Stmts | % Branch | % Funcs | % Lines | Uncovered Line #s       
----------------------------|---------|----------|---------|---------|-------------------------
All files                   |      30 |    27.27 |      50 |   31.57 |                         
 components/PokemonDetails  |     100 |      100 |     100 |     100 |                         
  PokemonDetails.jsx        |     100 |      100 |     100 |     100 |                         
 components/PokemonListItem |     100 |      100 |     100 |     100 |                         
  PokemonListItem.jsx       |     100 |      100 |     100 |     100 | 
 context                    |   24.32 |    27.27 |      20 |   25.71 | 
  PokemonContext.jsx        |   24.32 |    27.27 |      20 |   25.71 | 36-37,45,57-117,132-138
----------------------------|---------|----------|---------|---------|-------------------------

Test Suites: 3 passed, 3 total
Tests:       5 passed, 5 total
Snapshots:   0 total
Time:        3.009 s
Ran all test suites.
```
