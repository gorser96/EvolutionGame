import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { registration } from './registration.reducer';
import { roomStates } from './room.reducer';

const rootReducer = combineReducers({
  authentication,
  registration,
  roomStates,
});

export default rootReducer;