import { additionConstants, NotifySeverity } from "../constants";
import { systemActions } from "./system.actions";
import { additionService } from "../services";

export const additionActions = {
  list,
  get,
};

function list() {
  return async (dispatch) => {
    dispatch(request());

    return additionService.list().then(
      (additions) => {
        return dispatch(success(additions));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request() {
    return { type: additionConstants.LIST_REQUEST };
  }
  function success(additions) {
    return { type: additionConstants.LIST_SUCCESS, additions };
  }
  function failure(error) {
    return { type: additionConstants.LIST_FAILURE, error };
  }
}

function get(additionUid) {
  return async (dispatch) => {
    dispatch(request());

    return additionService.get(additionUid).then(
      (addition) => {
        return dispatch(success(addition));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request() {
    return { type: additionConstants.GET_REQUEST };
  }
  function success(addition) {
    return { type: additionConstants.GET_SUCCESS, addition };
  }
  function failure(error) {
    return { type: additionConstants.GET_FAILURE, error };
  }
}
