import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { registration } from './registration.reducer';
import { roomState } from './room.reducer';
import { additionState } from './addition.reducer';
import { signalRState, signalREvent } from './signalr.reducer';

const rootReducer = combineReducers({
  authentication,
  registration,
  roomState,
  additionState,
  signalRState,
  signalREvent,
});

export default rootReducer;