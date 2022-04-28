import { roomConstants } from "../constants";

export function roomStates(state = {}, action) {
  switch (action.type) {
    case roomConstants.CREATE_REQUEST:
      return { creating: true };
    case roomConstants.CREATE_SUCCESS:
      return { created: true, room: action.room };
    case roomConstants.CREATE_FAILURE:
      return {};
    case roomConstants.ENTER_REQUEST:
      return { entering: true };
    case roomConstants.ENTER_SUCCESS:
      return { entered: true, room: action.room };
    case roomConstants.ENTER_FAILURE:
      return {};
    case roomConstants.START_REQUEST:
      return { starting: true };
    case roomConstants.START_SUCCESS:
      return { started: true, room: action.room };
    case roomConstants.START_FAILURE:
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
    case roomConstants.LIST_REQUEST:
      return {};
    case roomConstants.LIST_SUCCESS:
      return { rooms: action.rooms };
    case roomConstants.LIST_FAILURE:
      return {};
    default:
      return state;
  }
}
