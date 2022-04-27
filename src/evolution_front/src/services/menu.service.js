import { apiUrl } from '../appsettings';
import { authHeader, apiStore } from '../helpers';

export const menuService = {
    create,
    enter
};

async function create(roomName) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: `"${roomName}"`
    };

    const response = await fetch(`${apiUrl}${apiStore.roomCreate}`, requestOptions);
    const user = await handleResponse(response);
    return user;
}

async function enter(roomUid) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' }
    };

    const response = await fetch(`${apiUrl}${apiStore.roomEnter.format(roomUid)}`, requestOptions);
    return handleResponse(response);
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                Location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}

