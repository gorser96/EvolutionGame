import { combineReducers } from 'redux';

import { authentication } from './authentication.reducer';
import { registration } from './registration.reducer';
import { menu } from './menu.reducer';

const rootReducer = combineReducers({
  authentication,
  registration,
  menu
});

export default rootReducer;