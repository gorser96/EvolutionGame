import React, { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import { HubConnectionBuilder } from '@microsoft/signalr';
import { apiStore } from '../helpers';
import { apiUrl } from '../appsettings';

export const ProtectedRoute = ({ user, children }) => {
  const [connection, setConnection] = useState(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(`${apiUrl}${apiStore.hubApi}`, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, [user]);

  useEffect(() => {
    if (connection) {
      console.log('test connection... started...');

      connection.start({ withCredentials: false })
        .then(() => {
          console.log('Connected!');

          connection.on("TestConnectionClient", () => {
            console.log('test connection success!');
          });

          connection.invoke("TestConnectionServer");
        })
        .catch(e => console.log('Connection failed: ', e));

      console.log('test connection... ended...');
    }
  }, [connection]);

  if (!user) {
    return <Navigate to="/" replace />;
  }

  return children;
};
