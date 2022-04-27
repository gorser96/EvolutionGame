import { menuConstants } from '../constants';
import { menuService } from '../services';

export const menuActions = {
  create,
  enter
};

function create(roomName) {
  return async dispatch => {
    dispatch(request({ roomName }));
    return menuService.create(roomName)
      .then(
        room => {
          return dispatch(success(room));
        },
        error => {
          return dispatch(failure(error.toString()));
        }
      );
  };

  function request(roomName) { return { type: menuConstants.CREATE_REQUEST, roomName } }
  function success(roomName) { return { type: menuConstants.CREATE_SUCCESS, roomName } }
  function failure(error) { return { type: menuConstants.CREATE_FAILURE, error } }
}


function enter(roomUid) {
  return async dispatch => {
    dispatch(request(roomUid));

    return menuService.enter(roomUid)
      .then(
        room => {
          return dispatch(success(room));
        },
        error => {
          return dispatch(failure(error.toString()));
        }
      );
  };

  function request(roomName) { return { type: menuConstants.ENTER_REQUEST, roomName } }
  function success(roomName) { return { type: menuConstants.ENTER_SUCCESS, roomName } }
  function failure(error) { return { type: menuConstants.ENTER_FAILURE, error } }
}