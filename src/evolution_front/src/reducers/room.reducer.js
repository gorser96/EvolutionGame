import { roomConstants } from "../constants";

export function roomState(state = {}, action) {
  switch (action.type) {
    case roomConstants.CREATE_REQUEST:
      return { creating: true };
    case roomConstants.CREATE_SUCCESS:
      return { created: true, room: action.room };
    case roomConstants.CREATE_FAILURE:
      return {};

    case roomConstants.UPDATE_REQUEST:
      return { updating: true, room: action.room };
    case roomConstants.UPDATE_SUCCESS:
      return { updated: true, room: action.room };
    case roomConstants.UPDATE_FAILURE:
      return {};

    case roomConstants.REMOVE_REQUEST:
      return { removing: true };
    case roomConstants.REMOVE_SUCCESS:
      return { removed: true };
    case roomConstants.REMOVE_FAILURE:
      return {};

    case roomConstants.ENTER_REQUEST:
      return { entering: true };
    case roomConstants.ENTER_SUCCESS:
      return { entered: true, room: action.room };
    case roomConstants.ENTER_FAILURE:
      return {};

    case roomConstants.LEAVE_REQUEST:
      return { leaving: true };
    case roomConstants.LEAVE_SUCCESS:
      return { leaved: true, room: action.room };
    case roomConstants.LEAVE_FAILURE:
      return {};

    case roomConstants.START_REQUEST:
      return { starting: true };
    case roomConstants.START_SUCCESS:
      return { started: true };
    case roomConstants.START_FAILURE:
      return {};

    case roomConstants.PAUSE_REQUEST:
      return { pausing: true };
    case roomConstants.PAUSE_SUCCESS:
      return { paused: true };
    case roomConstants.PAUSE_FAILURE:
      return {};

    case roomConstants.RESUME_REQUEST:
      return { resuming: true };
    case roomConstants.RESUME_SUCCESS:
      return { resumed: true };
    case roomConstants.RESUME_FAILURE:
      return {};

    case roomConstants.END_REQUEST:
      return { ending: true };
    case roomConstants.END_SUCCESS:
      return { ended: true };
    case roomConstants.END_FAILURE:
      return {};

    case roomConstants.LIST_REQUEST:
      return { requesting: true };
    case roomConstants.LIST_SUCCESS:
      return { requested: true, rooms: action.rooms };
    case roomConstants.LIST_FAILURE:
      return {};

    case roomConstants.GET_REQUEST:
      return { requesting: true };
    case roomConstants.GET_SUCCESS:
      return { requested: true, room: action.room };
    case roomConstants.GET_FAILURE:
      return {};

    case roomConstants.USER_REQUEST:
      return { requesting: true };
    case roomConstants.USER_SUCCESS:
      return { requested: true, room: action.room };
    case roomConstants.USER_FAILURE:
      return {};

    default:
      return state;
  }
}
