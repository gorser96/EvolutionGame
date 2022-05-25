import {
  HttpTransportType,
  HubConnectionBuilder
} from '@microsoft/signalr';

// action for user authentication and receiving the access_token
import { userConstants, signalRConstants } from '../../constants';
import { hubUrl } from "../../appsettings";
import { signalRActions, signalREvents } from "../../actions/signalr.actions"

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
    switch (action.type) {
      case userConstants.LOGIN_SUCCESS:
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
        break;
      case signalRConstants.TEST_REQUEST:
        connection.invoke('TestConnectionServer')
          .catch(error => store.dispatch({ type: signalRConstants.TEST_FAILURE, error: error.toString() }));
        break;
      default:
        return next(action);
    }
  }
};

export default signalRMiddleware;