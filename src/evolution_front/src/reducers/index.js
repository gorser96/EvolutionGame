import { combineReducers } from "redux";

import { authentication } from "./authentication.reducer";
import { registration } from "./registration.reducer";
import { roomState, roomUsers, roomGame } from "./room.reducer";
import { additionState } from "./addition.reducer";
import { signalRState, signalREvent, roomEvent } from "./signalr.reducer";

const rootReducer = combineReducers({
  authentication,
  registration,
  roomState,
  roomUsers,
  roomGame,
  additionState,
  signalRState,
  signalREvent,
  roomEvent,
});

export default rootReducer;
