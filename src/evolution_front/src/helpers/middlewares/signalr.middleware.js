import {
  HttpTransportType,
  HubConnectionBuilder
} from '@microsoft/signalr';

// action for user authentication and receiving the access_token
import { userConstants } from '../../constants';
import { hubUrl } from "../../appsettings";
import { signalRActions, signalREvents, signalRSendEvents } from "../../actions/signalr.actions"

const startSignalRConnection = (connection, dispatch) => {
  connection.stop();
  connection.start()
    .then(() => {
      console.info('SignalR Connected');
      signalRActions.testConnection()(dispatch);
    })
    .catch(err => console.error('SignalR Connection Error: ', err));
};

const signalRMiddleware = store => next => {
  let connection;

  return async (action) => {
    // register signalR after the user logged in
    let sendEventType = signalRSendEvents.find(x => x.request_type === action.type);
    if (action.type === userConstants.LOGIN_SUCCESS) {
      const connectionHub = `${hubUrl}`;

      // let transport to fall back to to LongPolling if it needs to
      const transport = HttpTransportType.WebSockets;

      const options = {
        transport,
        skipNegotiation: true,
        accessTokenFactory: () => action.user.token
      };

      // create the connection instance
      connection = new HubConnectionBuilder()
        .withUrl(connectionHub, options)
        .withAutomaticReconnect()
        .build();

      // event handlers
      signalREvents.forEach(eventHandler => {
        connection.on(eventHandler.name, eventHandler.action(store.dispatch));
      });

      startSignalRConnection(connection, store.dispatch);
    }
    else if (sendEventType !== undefined) {
      if (action.data !== undefined) {
        connection.invoke(sendEventType.method, action.data)
          .catch(error => store.dispatch({ type: sendEventType.failure_type, error: error.toString() }));
      }
    }

    return next(action);
  }
};

export default signalRMiddleware;