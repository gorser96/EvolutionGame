import {
  HttpTransportType,
  HubConnectionBuilder,
  HubConnectionState,
} from "@microsoft/signalr";

// action for user authentication and receiving the access_token
import { userConstants, signalRConstants } from "../../constants";
import { hubUrl } from "../../appsettings";
import {
  signalRActions,
  signalREvents,
  signalRSendEvents,
} from "../../actions/signalr.actions";

const startSignalRConnection = (connection, dispatch) => {
  connection.stop();
  connection
    .start()
    .then(() => {
      console.info("SignalR Connected");
      dispatch({ type: signalRConstants.CONNECTED, connection: connection });
      signalRActions.testConnection()(dispatch);
    })
    .catch((err) => {
      console.error("SignalR Connection Error: ", err);
      connection = undefined;

      dispatch({
        type: signalRConstants.CONNECT_FAILURE,
        connection: connection,
        error: err,
      });
    });
};

const createConnection = (authData, dispatch) => {
  const connectionHub = `${hubUrl}`;

  // let transport to fall back to to LongPolling if it needs to
  const transport = HttpTransportType.WebSockets;

  const options = {
    transport,
    skipNegotiation: true,
    accessTokenFactory: () => authData.user.token,
  };

  // create the connection instance
  const connection = new HubConnectionBuilder()
    .withUrl(connectionHub, options)
    .withAutomaticReconnect()
    .build();

  // event handlers
  signalREvents.forEach((eventHandler) => {
    connection.on(eventHandler.name, eventHandler.action(dispatch));
  });

  startSignalRConnection(connection, dispatch);
};

const signalRMiddleware = (store) => (next) => {
  return async (action) => {
    // register signalR after the user logged in
    let sendEventType = signalRSendEvents.find(
      (x) => x.request_type === action.type
    );
    const state = store.getState();
    const authData = state.authentication;
    const signalRState = state.signalRState;

    if (action.type === userConstants.LOGIN_SUCCESS) {
      next(action);
      store.dispatch({ type: signalRConstants.CONNECTING });
      return;
    } else if (action.type === signalRConstants.CONNECTING) {
      if (signalRState.connection !== undefined) {
        signalRState.connection.stop();
      }
      createConnection(authData, store.dispatch);
    } else {
      if (
        authData.user &&
        authData.user.token &&
        !signalRState.connecting &&
        (!signalRState.connection ||
          signalRState.connection.state === HubConnectionState.Disconnected)
      ) {
        store.dispatch({ type: signalRConstants.CONNECTING });
      }
    }

    if (sendEventType !== undefined) {
      action.args = action.args ? action.args : [];
      signalRState.connection
        .invoke(sendEventType.method, ...action.args)
        .catch((error) =>
          store.dispatch({
            type: sendEventType.failure_type,
            error: error.toString(),
          })
        );
    }

    return next(action);
  };
};

export default signalRMiddleware;
