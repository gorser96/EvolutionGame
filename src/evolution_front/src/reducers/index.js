import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { registration } from './registration.reducer';
import { roomState } from './room.reducer';

const rootReducer = combineReducers({
  authentication,
  registration,
  roomState,
});

export default rootReducer;