import { apiUrl } from "../appsettings";
import { authHeader, apiStore } from "../helpers";

export const additionService = {
  get,
  list,
};

async function get(additionUid) {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = await fetch(
    `${apiUrl}${apiStore.additionGet.format(additionUid)}`,
    requestOptions
  );
  const addition = await handleResponse(response);
  return addition;
}

async function list() {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = await fetch(`${apiUrl}${apiStore.additionList}`, requestOptions);
  const additions = await handleResponse(response);
  return additions;
}

function handleResponse(response) {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        Location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
