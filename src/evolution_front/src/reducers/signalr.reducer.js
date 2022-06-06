import { signalRConstants } from "../constants";

export function signalREvent(state = {}, action) {
  switch (action.type) {
    case signalRConstants.TEST_REQUEST:
      return { connecting: true };
    case signalRConstants.TEST_SUCCESS:
      return { connected: true };
    case signalRConstants.TEST_FAILURE:
      return { error: action.error };

    case signalRConstants.ROOM_UPDATED:
      return { roomUpdated: true, roomUid: action.roomUid };

    default:
      return state;
  }
}

export function signalRState(state = {}, action) {
  switch (action.type) {
    case signalRConstants.CONNECTING:
      return { connecting: true };
    case signalRConstants.CONNECTED:
      return { connection: action.connection, connected: true };
    case signalRConstants.CONNECT_FAILURE:
      return { connection: state.connection, error: action.error };

    default:
      return state;
  }
}
