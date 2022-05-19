import { additionConstants } from "../constants";
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
        return Promise.reject(failure(error.toString()));
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
        return Promise.reject(failure(error.toString()));
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