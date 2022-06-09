import { signalRConstants, systemConstants } from "../constants";
import { roomActions } from "./room.actions";

const testConnection = () => async (dispatch) => dispatch({ type: signalRConstants.TEST_REQUEST });

const onTestClientReceived = (dispatch) => result => {
  console.log("test client success!");
  dispatch({ type: signalRConstants.TEST_SUCCESS });
};

const onUpdatedRoom = (dispatch) => result => {
  dispatch({ type: signalRConstants.ROOM_UPDATED, roomUid: result[0] });
  roomActions.getUsers(result[0])(dispatch);
};

const onDeletedRoom = (dispatch) => result => {
  dispatch({ type: signalRConstants.ROOM_DELETED, roomUid: result[0] });
  dispatch({
    type: systemConstants.SNACK_NOTIFY,
    notify: {
      message: "Комната была удалена!",
      severity: "success",
    },
  });
};

const signalREvents = [
  { name: 'TestConnectionClient', action: onTestClientReceived },
  { name: 'UpdatedRoom', action: onUpdatedRoom },
  { name: 'DeletedRoom', action: onDeletedRoom },
];

const signalRSendEvents = [
  { request_type: signalRConstants.TEST_REQUEST, failure_type: signalRConstants.TEST_FAILURE, method: 'TestConnectionServer' },
];

const signalRActions = {
  testConnection
};

export { signalRActions, signalREvents, signalRSendEvents };
