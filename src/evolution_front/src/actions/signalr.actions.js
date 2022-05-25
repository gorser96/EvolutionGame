import { signalRConstants } from "../constants";

const testConnection = () => async (dispatch) => dispatch({ type: signalRConstants.TEST_REQUEST });

const onTestClientReceived = (dispatch) => result => {
  console.log("test client success!");
  dispatch({ type: signalRConstants.TEST_SUCCESS });
};

const signalREvents = [
  { name: 'TestConnectionClient', action: onTestClientReceived }
];

const signalRActions = {
  testConnection
};

export { signalRActions, signalREvents };
