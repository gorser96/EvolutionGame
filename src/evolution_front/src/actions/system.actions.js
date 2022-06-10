import { systemConstants } from "../constants";

export const systemActions = {
  sendNotification,
};

function sendNotification(message, severity) {
  return (dispatch) => {
    dispatch({
      type: systemConstants.SNACK_NOTIFY,
      notify: {
        message: message,
        severity: severity,
      },
    });
  };
}
