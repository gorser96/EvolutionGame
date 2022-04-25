import { configureStore } from '@reduxjs/toolkit'
import { createLogger } from 'redux-logger';
import thunkMiddleware from 'redux-thunk';
import rootReducer from '../reducers';

const loggerMiddleware = createLogger();

export default configureStore({
  reducer: rootReducer,
  middleware: [thunkMiddleware, loggerMiddleware]
})