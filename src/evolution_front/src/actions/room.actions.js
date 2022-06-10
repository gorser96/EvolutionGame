import { roomConstants, NotifySeverity } from "../constants";
import { systemActions } from "./system.actions";
import { roomService } from "../services";

export const roomActions = {
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
  list,
  get,
  user,
  getUsers,
};

function create(roomName) {
  return async (dispatch) => {
    dispatch(request(roomName));
    return roomService.create(roomName).then(
      (room) => {
        return dispatch(success(room));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomName) {
    return { type: roomConstants.CREATE_REQUEST, roomName };
  }
  function success(room) {
    return { type: roomConstants.CREATE_SUCCESS, room };
  }
  function failure(error) {
    return { type: roomConstants.CREATE_FAILURE, error };
  }
}

function update(roomUid, roomModel) {
  return async (dispatch) => {
    dispatch(request(roomUid, roomModel));
    return roomService.update(roomUid, roomModel).then(
      (room) => {
        return dispatch(success(room));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid, roomModel) {
    return { type: roomConstants.UPDATE_REQUEST, roomUid, roomModel };
  }
  function success(room) {
    return { type: roomConstants.UPDATE_SUCCESS, room };
  }
  function failure(error) {
    return { type: roomConstants.UPDATE_FAILURE, error };
  }
}

function remove(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));
    return roomService.remove(roomUid).then(
      () => {
        systemActions.sendNotification(
          "Комната была удалена!",
          NotifySeverity.Success
        )(dispatch);
        return dispatch(success());
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.REMOVE_REQUEST, roomUid };
  }
  function success() {
    return { type: roomConstants.REMOVE_SUCCESS };
  }
  function failure(error) {
    return { type: roomConstants.REMOVE_FAILURE, error };
  }
}

function enter(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));

    return roomService.enter(roomUid).then(
      (room) => {
        return dispatch(success(room));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.ENTER_REQUEST, roomUid };
  }
  function success(room) {
    return { type: roomConstants.ENTER_SUCCESS, room };
  }
  function failure(error) {
    return { type: roomConstants.ENTER_FAILURE, error };
  }
}

function leave(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));

    return roomService.leave(roomUid).then(
      (room) => {
        return dispatch(success(room));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.LEAVE_REQUEST, roomUid };
  }
  function success(room) {
    return { type: roomConstants.LEAVE_SUCCESS, room };
  }
  function failure(error) {
    return { type: roomConstants.LEAVE_FAILURE, error };
  }
}

function kick(roomUid, userUid) {
  return async (dispatch) => {
    dispatch(request(roomUid, userUid));

    return roomService.kick(roomUid, userUid).then(
      (room) => {
        return dispatch(success(room));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid, userUid) {
    return { type: roomConstants.KICK_REQUEST, roomUid, userUid };
  }
  function success(room) {
    return { type: roomConstants.KICK_SUCCESS, room };
  }
  function failure(error) {
    return { type: roomConstants.KICK_FAILURE, error };
  }
}

function start(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));

    return roomService.start(roomUid).then(
      () => {
        return dispatch(success());
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.START_REQUEST, roomUid };
  }
  function success() {
    return { type: roomConstants.START_SUCCESS };
  }
  function failure(error) {
    return { type: roomConstants.START_FAILURE, error };
  }
}

function pause(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));

    return roomService.pause(roomUid).then(
      () => {
        return dispatch(success());
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.PAUSE_REQUEST, roomUid };
  }
  function success() {
    return { type: roomConstants.PAUSE_SUCCESS };
  }
  function failure(error) {
    return { type: roomConstants.PAUSE_FAILURE, error };
  }
}

function resume(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));

    return roomService.resume(roomUid).then(
      () => {
        return dispatch(success());
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.RESUME_REQUEST, roomUid };
  }
  function success() {
    return { type: roomConstants.RESUME_SUCCESS };
  }
  function failure(error) {
    return { type: roomConstants.RESUME_FAILURE, error };
  }
}

function end(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));

    return roomService.end(roomUid).then(
      () => {
        return dispatch(success());
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.END_REQUEST, roomUid };
  }
  function success() {
    return { type: roomConstants.END_SUCCESS };
  }
  function failure(error) {
    return { type: roomConstants.END_FAILURE, error };
  }
}

function list() {
  return async (dispatch) => {
    dispatch(request());

    return roomService.list().then(
      (rooms) => {
        return dispatch(success(rooms));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request() {
    return { type: roomConstants.LIST_REQUEST, rooms: [] };
  }
  function success(rooms) {
    return { type: roomConstants.LIST_SUCCESS, rooms };
  }
  function failure(error) {
    return { type: roomConstants.LIST_FAILURE, error };
  }
}

function get(roomUid) {
  return async (dispatch) => {
    dispatch(request());

    return roomService.get(roomUid).then(
      (room) => {
        return dispatch(success(room));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request() {
    return { type: roomConstants.GET_REQUEST };
  }
  function success(room) {
    return { type: roomConstants.GET_SUCCESS, room };
  }
  function failure(error) {
    return { type: roomConstants.GET_FAILURE, error };
  }
}

function user() {
  return async (dispatch) => {
    dispatch(request());

    return roomService.user().then(
      (room) => {
        return dispatch(success(room));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request() {
    return { type: roomConstants.USER_REQUEST };
  }
  function success(room) {
    return { type: roomConstants.USER_SUCCESS, room };
  }
  function failure(error) {
    return { type: roomConstants.USER_FAILURE, error };
  }
}

function getUsers(roomUid) {
  return async (dispatch) => {
    dispatch(request(roomUid));

    return roomService.getUsers(roomUid).then(
      (users) => {
        return dispatch(success(users));
      },
      (error) => {
        systemActions.sendNotification(error.message, NotifySeverity.Error)(dispatch);
        return Promise.reject(dispatch(failure(error)));
      }
    );
  };

  function request(roomUid) {
    return { type: roomConstants.GET_USERS_REQUEST, roomUid };
  }
  function success(users) {
    return { type: roomConstants.GET_USERS_SUCCESS, users };
  }
  function failure(error) {
    return { type: roomConstants.GET_USERS_FAILURE, error };
  }
}
