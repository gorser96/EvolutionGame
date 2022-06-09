import { systemConstants } from "../constants";

export function notificationState(state = {}, action) {
  switch (action.type) {
    case systemConstants.SNACK_NOTIFY:
      return { notify: action.notify };

    default:
      return state;
  }
}