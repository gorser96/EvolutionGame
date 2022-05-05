import { roomConstants } from '../constants';
import { roomService } from '../services';

export const roomActions = {
  create,
  enter,
  list,
  get,
  user,
};

function create(roomName) {
  return async dispatch => {
    dispatch(request({ roomName }));
    return roomService.create(roomName)
      .then(
        room => {
          return dispatch(success(room));
        },
        error => {
          return dispatch(failure(error.toString()));
        }
      );
  };

  function request(roomName) { return { type: roomConstants.CREATE_REQUEST, roomName } }
  function success(room) { return { type: roomConstants.CREATE_SUCCESS, room } }
  function failure(error) { return { type: roomConstants.CREATE_FAILURE, error } }
}

function enter(roomUid) {
  return async dispatch => {
    dispatch(request(roomUid));

    return roomService.enter(roomUid)
      .then(
        room => {
          return dispatch(success(room));
        },
        error => {
          return dispatch(failure(error.toString()));
        }
      );
  };

  function request(roomUid) { return { type: roomConstants.ENTER_REQUEST, roomUid } }
  function success(room) { return { type: roomConstants.ENTER_SUCCESS, room } }
  function failure(error) { return { type: roomConstants.ENTER_FAILURE, error } }
}

function list() {
  return async dispatch => {
    dispatch(request());

    return roomService.list()
      .then(
        rooms => {
          return dispatch(success(rooms));
        },
        error => {
          return dispatch(failure(error.toString()));
        }
      );
  };

  function request() { return { type: roomConstants.LIST_REQUEST, rooms: [] } }
  function success(rooms) { return { type: roomConstants.LIST_SUCCESS, rooms } }
  function failure(error) { return { type: roomConstants.LIST_FAILURE, error } }
}

function get(roomUid) {
  return async dispatch => {
    dispatch(request());

    return roomService.get(roomUid)
      .then(
        room => {
          return dispatch(success(room));
        },
        error => {
          return dispatch(failure(error.toString()));
        }
      );
  };

  function request() { return { type: roomConstants.GET_REQUEST } }
  function success(room) { return { type: roomConstants.GET_SUCCESS, room } }
  function failure(error) { return { type: roomConstants.GET_FAILURE, error } }
}

function user() {
  return async dispatch => {
    dispatch(request());

    return roomService.user()
      .then(
        room => {
          return dispatch(success(room));
        },
        error => {
          return dispatch(failure(error.toString()));
        }
      );
  };

  function request() { return { type: roomConstants.USER_REQUEST } }
  function success(room) { return { type: roomConstants.USER_SUCCESS, room } }
  function failure(error) { return { type: roomConstants.USER_FAILURE, error } }
}