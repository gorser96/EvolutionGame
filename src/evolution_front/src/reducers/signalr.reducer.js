import { signalRConstants } from "../constants";

export function signalRState(state = {}, action) {
  switch (action.type) {
    case signalRConstants.TEST_REQUEST:
      return { connecting: true };
    case signalRConstants.TEST_SUCCESS:
      return { connected: true };
    case signalRConstants.TEST_FAILURE:
      return { error: action.error };

    default:
      return state;
  }
}
