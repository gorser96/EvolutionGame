import { menuConstants } from "../constants";

export function menu(state = {}, action) {
  switch (action.type) {
    case menuConstants.CREATE_REQUEST:
      return { registering: true };
    case menuConstants.CREATE_SUCCESS:
      return {};
    case menuConstants.CREATE_FAILURE:
      return {};
    case menuConstants.ENTER_REQUEST:
      return { entering: true };
    case menuConstants.ENTER_SUCCESS:
      return {};
    case menuConstants.ENTER_FAILURE:
      return {};
    default:
      return state;
  }
}
