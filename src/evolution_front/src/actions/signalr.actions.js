import {
  signalRConstants,
  RoomIntegrationType,
} from "../constants";
import { roomActions } from "./room.actions";

const testConnection = () => async (dispatch) =>
  dispatch({ type: signalRConstants.TEST_REQUEST });

const onTestClientReceived = (dispatch) => () => {
  console.log("test client success!");
  dispatch({ type: signalRConstants.TEST_SUCCESS });
};

const onRoomEvent = (dispatch) => (eventData) => {
  switch (eventData.eventType) {
    case RoomIntegrationType.Created:
      dispatch({
        type: signalRConstants.ROOM_CREATED,
        roomUid: eventData.roomUid,
      });
      roomActions.list()(dispatch);
      break;
    case RoomIntegrationType.Modified:
      dispatch({
        type: signalRConstants.ROOM_UPDATED,
        roomUid: eventData.roomUid,
      });
      roomActions.get(eventData.roomUid)(dispatch);
      break;
    case RoomIntegrationType.Removed:
      dispatch({
        type: signalRConstants.ROOM_DELETED,
        roomUid: eventData.roomUid,
      });
      roomActions.list()(dispatch);
      break;
    case RoomIntegrationType.UserJoined:
      dispatch({
        type: signalRConstants.ROOM_USER_JOINED,
        roomUid: eventData.roomUid,
      });
      roomActions.getUsers(eventData.roomUid)(dispatch);
      break;
    case RoomIntegrationType.UserLeft:
      dispatch({
        type: signalRConstants.ROOM_USER_LEFT,
        roomUid: eventData.roomUid,
      });
      roomActions.getUsers(eventData.roomUid)(dispatch);
      break;

    default:
      break;
  }
};

const signalREvents = [
  { name: "TestConnectionClient", action: onTestClientReceived },
  { name: "RoomEvent", action: onRoomEvent },
];

const signalRSendEvents = [
  {
    request_type: signalRConstants.TEST_REQUEST,
    failure_type: signalRConstants.TEST_FAILURE,
    method: "TestConnectionServer",
  },
];

const signalRActions = {
  testConnection,
};

export { signalRActions, signalREvents, signalRSendEvents };
