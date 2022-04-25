import { menuConstants } from '../constants';
import { menuService } from '../services';

export const menuActions = {
};

function create(roomName) {
  return dispatch => {
    dispatch(request({ username }));
//TODO
    userService.login(username, password)
      .then(
        user => {
          dispatch(success(user));
          //history.push('/');
        },
        error => {
          dispatch(failure(error.toString()));
          //dispatch(alertActions.error(error.toString()));
        }
      );
  };

  function request(user) { return { type: menuConstants.LOGIN_REQUEST, user } }
  function success(user) { return { type: menuConstants.LOGIN_SUCCESS, user } }
  function failure(error) { return { type: menuConstants.LOGIN_FAILURE, error } }
}


function enter(roomUid) {
  return dispatch => {
    dispatch(request(user));

    userService.register(user)
      .then(
        user => {
          dispatch(success());
        },
        error => {
          dispatch(failure(error.toString()));
        }
      );
  };

  function request(user) { return { type: menuConstants.REGISTER_REQUEST, user } }
  function success(user) { return { type: menuConstants.REGISTER_SUCCESS, user } }
  function failure(error) { return { type: menuConstants.REGISTER_FAILURE, error } }
}