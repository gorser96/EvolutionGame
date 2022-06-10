import { userService } from "./user.service";

export { handleResponse };

function handleResponse(fetchPromise) {
  return fetchPromise
    .then(async (response) => {
      if (!response.ok) {
        if (response.status === 401) {
          userService.logout();
          Location.reload(true);
        }
        const error = `${response.statusText}: ${await response.text()}`;
        throw new Error(error);
      }
      return response.text();
    })
    .then(
      (text) => {
        let resultData = text;
        try {
          const data = resultData && JSON.parse(resultData);
          resultData = data;
        } catch (error) {}

        return resultData;
      },
      (failure) => {
        return Promise.reject(failure);
      }
    );
}
