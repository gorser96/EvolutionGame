import { roomConstants } from '../constants';
import { roomService } from '../services';

export const roomActions = {
  create,
  enter,
  list,
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
  function success(roomName) { return { type: roomConstants.CREATE_SUCCESS, roomName } }
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

  function request() { return { type: roomConstants.LIST_REQUEST } }
  function success(rooms) { return { type: roomConstants.LIST_SUCCESS, rooms } }
  function failure(error) { return { type: roomConstants.LIST_FAILURE, error } }
}