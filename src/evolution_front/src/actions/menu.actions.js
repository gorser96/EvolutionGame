import { menuConstants } from '../constants';
import { menuService } from '../services';

export const menuActions = {
  create,
  enter
};

function create(roomName) {
  return dispatch => {
    dispatch(request({ roomName }));
    menuService.create(roomName)
      .then(
        room => {
          dispatch(success(room));
        },
        error => {
          dispatch(failure(error.toString()));
        }
      );
  };

  function request(roomName) { return { type: menuConstants.CREATE_REQUEST, roomName } }
  function success(roomName) { return { type: menuConstants.CREATE_SUCCESS, roomName } }
  function failure(error) { return { type: menuConstants.CREATE_FAILURE, error } }
}


function enter(roomUid) {
  return dispatch => {
    dispatch(request(roomUid));

    menuService.enter(roomUid)
      .then(
        room => {
          dispatch(success(room));
        },
        error => {
          dispatch(failure(error.toString()));
        }
      );
  };

  function request(roomName) { return { type: menuConstants.ENTER_REQUEST, roomName } }
  function success(roomName) { return { type: menuConstants.ENTER_SUCCESS, roomName } }
  function failure(error) { return { type: menuConstants.ENTER_FAILURE, error } }
}