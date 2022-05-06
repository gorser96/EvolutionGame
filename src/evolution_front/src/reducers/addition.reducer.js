import { additionConstants } from "../constants";

export function additionState(state = {}, action) {
  switch (action.type) {
    case additionConstants.LIST_REQUEST:
      return { listing: true };
    case additionConstants.LIST_SUCCESS:
      return { listed: true, additions: action.additions };
    case additionConstants.LIST_FAILURE:
      return {};

    case additionConstants.GET_REQUEST:
      return { getting: true };
    case additionConstants.GET_SUCCESS:
      return { getted: true, addition: action.addition };
    case additionConstants.GET_FAILURE:
      return {};

    default:
      return state;
  }
}
