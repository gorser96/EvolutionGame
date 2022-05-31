import { apiUrl } from "../appsettings";
import { authHeader, apiStore } from "../helpers";
import { handleResponse } from "./service.base";

export const roomService = {
  user,
  get,
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
  const room = await handleResponse(response);
  return room;
}

async function list() {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(`${apiUrl}${apiStore.roomList}`, requestOptions);
  const rooms = await handleResponse(response);
  return rooms;
}

async function user() {
  const requestOptions = {
    method: "GET",
    headers: { ...authHeader(), "Content-Type": "application/json" },
  };

  const response = fetch(`${apiUrl}${apiStore.roomUser}`, requestOptions);
  const room = await handleResponse(response);
  return room;
}

async function create(roomName) {
  const requestOptions = {
    method: "POST",
    headers: { ...authHeader(), "Content-Type": "application/json" },
    body: `"${roomName}"`,
  };

  const response = fetch(
    `${apiUrl}${apiStore.roomCreate}`,
    requestOptions
  );
  const room = await handleResponse(response);
  return room;
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
  const room = await handleResponse(response);
  return room;
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
  const room = await handleResponse(response);
  return room;
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
  const room = await handleResponse(response);
  return room;
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
  const room = await handleResponse(response);
  return room;
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
