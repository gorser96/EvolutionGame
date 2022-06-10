import { apiUrl } from "../appsettings";
import { authHeader, apiStore } from "../helpers";
import { handleResponse } from "./service.base";

export const roomService = {
  user,
  get,
  getUsers,
  list,
  create,
  update,
  remove,
  enter,
  leave,
  kick,
  start,
  pause,
  resume,
  end,
};

async function get(roomUid) {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomGet.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function getUsers(roomUid) {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomGetUsers.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function list() {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(`${apiUrl}${apiStore.roomList}`, requestOptions);

  return handleResponse(response);
}

async function user() {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(`${apiUrl}${apiStore.roomUser}`, requestOptions);

  return handleResponse(response);
}

async function create(roomName) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: `"${roomName}"`,
  };

  const response = fetch(`${apiUrl}${apiStore.roomCreate}`, requestOptions);

  return handleResponse(response);
}

async function update(roomUid, roomModel) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: JSON.stringify(roomModel),
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomUpdate.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function remove(roomUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomRemove.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function enter(roomUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomEnter.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function leave(roomUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomLeave.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function kick(roomUid, userUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomKick.format(roomUid, userUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function start(roomUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomStart.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function pause(roomUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomPause.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function resume(roomUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomResume.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}

async function end(roomUid) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomEnd.format(roomUid)}`,
    requestOptions
  );

  return handleResponse(response);
}
