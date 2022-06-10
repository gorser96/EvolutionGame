import { userConstants, NotifySeverity } from "../constants";
import { systemActions } from "./system.actions";
import { userService } from "../services";

export const userActions = {
  login,
  logout,
  register,
};

function login(username, password) {
  return async (dispatch) => {
    dispatch(request({ username }));

    return userService.login(username, password).then(
      (user) => {
        return dispatch(success(user));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(user) {
    return { type: userConstants.LOGIN_REQUEST, user };
  }
  function success(user) {
    return { type: userConstants.LOGIN_SUCCESS, user };
  }
  function failure(error) {
    return { type: userConstants.LOGIN_FAILURE, error };
  }
}

function logout() {
  userService.logout();
  return { type: userConstants.LOGOUT };
}

function register(username, password) {
  return async (dispatch) => {
    dispatch(request(username));

    return userService.register(username, password).then(
      (user) => {
        return dispatch(success(user));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(user) {
    return { type: userConstants.REGISTER_REQUEST, user };
  }
  function success(user) {
    return { type: userConstants.REGISTER_SUCCESS, user };
  }
  function failure(error) {
    return { type: userConstants.REGISTER_FAILURE, error };
  }
}
