import { userConstants } from '../constants';
import { userService } from '../services';

export const userActions = {
    login,
    logout,
    register,
    //delete: _delete
};

function login(username, password) {
    return async dispatch => {
        dispatch(request({ username }));

        return userService.login(username, password)
            .then(
                user => {
                    return dispatch(success(user));
                },
                error => {
                    return Promise.reject(dispatch(failure(error)));
                }
            );
    };

    function request(user) { return { type: userConstants.LOGIN_REQUEST, user } }
    function success(user) { return { type: userConstants.LOGIN_SUCCESS, user } }
    function failure(error) { return { type: userConstants.LOGIN_FAILURE, error } }
}

function logout() {
    userService.logout();
    return { type: userConstants.LOGOUT };
}

function register(username, password) {
    return async dispatch => {
        dispatch(request(username));

        return userService.register(username, password)
            .then(
                user => { 
                    return dispatch(success(user));
                },
                error => {
                    return Promise.reject(dispatch(failure(error)));
                }
            );
    };

    function request(user) { return { type: userConstants.REGISTER_REQUEST, user } }
    function success(user) { return { type: userConstants.REGISTER_SUCCESS, user } }
    function failure(error) { return { type: userConstants.REGISTER_FAILURE, error } }
}
/*
// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    return dispatch => {
        dispatch(request(id));

        userService.delete(id)
            .then(
                user => dispatch(success(id)),
                error => dispatch(failure(id, error.toString()))
            );
    };

    function request(id) { return { type: userConstants.DELETE_REQUEST, id } }
    function success(id) { return { type: userConstants.DELETE_SUCCESS, id } }
    function failure(id, error) { return { type: userConstants.DELETE_FAILURE, id, error } }
}
*/