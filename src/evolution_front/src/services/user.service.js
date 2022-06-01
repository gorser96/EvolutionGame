import { apiUrl } from "../appsettings";
import { apiStore } from "../helpers";
import { handleResponse } from "./service.base";

export const userService = {
  login,
  logout,
  register,
};

async function login(username, password) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ login: username, password: password }),
  };

  const response = fetch(`${apiUrl}${apiStore.userLogin}`, requestOptions);
  return handleResponse(response).then(
    (result) => {
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.setItem("user", JSON.stringify(result));
      return result;
    },
    (failure) => {
      console.log('reject');
      return Promise.reject(failure)
    }
  );
}

function logout() {
  // remove user from local storage to log user out
  localStorage.removeItem("user");
}

async function register(username, password) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ login: username, password: password }),
  };

  const response = fetch(`${apiUrl}${apiStore.userRegister}`, requestOptions);
  return handleResponse(response);
}
/*
async function update(user) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    const response = await fetch(`${apiUrl}/users/${user.id}`, requestOptions);
    return handleResponse(response);;
}
*/
/*
// prefixed function name with underscore because delete is a reserved word in javascript
async function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    const response = await fetch(`${apiUrl}/users/${id}`, requestOptions);
    return handleResponse(response);
}
*/
