import { apiUrl } from '../appsettings';
import { authHeader, apiStore } from '../helpers';

export const roomService = {
    list,
    create,
    enter,
    get,
};

async function get(roomUid) {
    const requestOptions = {
        method: 'GET',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
    };

    const response = await fetch(`${apiUrl}${apiStore.roomGet.format(roomUid)}`, requestOptions);
    const room = await handleResponse(response);
    return room;
}

async function list() {
    const requestOptions = {
        method: 'GET',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
    };

    const response = await fetch(`${apiUrl}${apiStore.roomList}`, requestOptions);
    const rooms = await handleResponse(response);
    return rooms;
}

async function create(roomName) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: `"${roomName}"`
    };

    const response = await fetch(`${apiUrl}${apiStore.roomCreate}`, requestOptions);
    const room = await handleResponse(response);
    return room;
}

async function enter(roomUid) {
    const requestOptions = {
        method: 'POST',
        headers: { ...authHeader(), 'Content-Type': 'application/json' }
    };

    const response = await fetch(`${apiUrl}${apiStore.roomEnter.format(roomUid)}`, requestOptions);
    const room = handleResponse(response);
    return room
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

