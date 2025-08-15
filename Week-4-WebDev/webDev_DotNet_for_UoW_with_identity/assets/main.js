document.querySelector('#loginBtn').addEventListener("click", login);

document.querySelector('#newAccBtn').addEventListener("click", createAcc);

// this login function is expecting an HttpOnly cookie
// this will be 100% protected from XSS attacks as no javascript
// will be able to find the JWT in localstorage/session storage as-is with the alternative solution
// This way DOES open the possibility of CSRF attacks, but if we return the cookie 
// from .net with SameSite=Strict then we won't have to worry about this. 
// HttpOnly cookies take a little more setup, but are the most secure 
// async function login() {

//   let userEmail = document.querySelector('#email').value;
//   let password = document.querySelector('#password').value;

//   let options = {
//     method: 'POST',
//     headers: {
//       'Content-Type': 'application/json' // Specify the content type as JSON
//     },
//     body: JSON.stringify({ // Serialize the body to JSON
//       "Email": userEmail,
//       "Password": password
//     })
//   };

//   let res = await fetch(`https://localhost:5062/auth/login`, options);


//   let parsedRes = await res.json();
//   console.log(parsedRes);
  
// }

// this login function is expecting our JWT to be returned directly 
// we will place it in our local storage 
// .net automatically encodes transmitted variables, so we have *SOME* 
// mitigation from XSS attacks. We are totally protected from CSRF attacks, as 
// those only effect cookies.
// Doing it this way, we don't *need* to setup or worry about CORS (but we still probably should anyway)
async function login() {

  let userEmail = document.querySelector('#email').value;
  let password = document.querySelector('#password').value;

 let response = await fetch('https://localhost:5062/auth/login', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({
        Email: userEmail,
        Password: password
    })
  });

  if (response.ok) {
      let data = await response.json();
      localStorage.setItem('AuthToken', data.message); // Store the token in localStorage
  } else {
      console.error('Login failed:', response.statusText);
  } 
  
}

// this function create an account record in our database (not a user account!!! account != user)
// this function includes credentials automatically - it will automatically look for 
// all cookies for the domain specified in the fetch request. it will then send 
// all of those cookies along with the request within the request header
// async function createAcc() {
//   let accName = document.querySelector('#accName').value;

//   let options = {
//     method: 'POST',
//     headers: {
//       'Content-Type': 'application/json' // Specify the content type as JSON
//     },
//     body: JSON.stringify({ // Serialize the body to JSON
//       Name: accName
//     }),
//     credentials: 'include'
//   };

//   let res = await fetch(`https://localhost:5062/app/account`, options);


//   let parsedRes = await res.json();
//   console.log(parsedRes); 
// }


// this function creates an account record in our database (not a user account!!! account != user)
// this function does NOT include credentials implicitly. If we store our token in local storage, 
// this is how we use it:
async function createAcc() {
  let accName = document.querySelector('#accName').value;

  // Retrieve the token from local storage
  let token = localStorage.getItem('AuthToken');

  if (!token) {
    console.error('No authentication token found. Please log in first.');
    return;
  }

  let options = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json', // Specify the content type as JSON
      'Authorization': `Bearer ${token}` // Include the token in the Authorization header
    },
    body: JSON.stringify({ // Serialize the body to JSON
      Name: accName
    })
  };

  try {
    let res = await fetch(`https://localhost:5062/app/account`, options);

    if (res.ok) {
      let parsedRes = await res.json();
      console.log(parsedRes);
    } else {
      console.error('Failed to create account:', res.statusText);
    }
  } catch (error) {
    console.error('Error during account creation:', error);
  }
}