import { apiUrl } from "../appsettings";
import { authHeader, apiStore } from "../helpers";
import { handleResponse } from "./service.base";

export const additionService = {
  get,
  list,
};

async function get(additionUid) {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
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

  const response = fetch(`${apiUrl}${apiStore.additionList}`, requestOptions);
  const additions = await handleResponse(response);
  return additions;
}
