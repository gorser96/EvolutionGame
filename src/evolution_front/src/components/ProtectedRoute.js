import React, { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import { HubConnectionBuilder, HttpTransportType } from "@microsoft/signalr";
import { hubUrl } from "../appsettings";

export const ProtectedRoute = ({ user, children }) => {
  const [connection, setConnection] = useState(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(hubUrl, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, [user]);

  useEffect(() => {
    if (connection) {
      console.log("test connection... started...");

      connection
        .start()
        .then(() => {
          console.log("Connected!");

          connection.on("TestConnectionClient", () => {
            console.log("test connection success!");
          });

          connection.invoke("TestConnectionServer");
        })
        .catch((e) => console.log("Connection failed: ", e));

      console.log("test connection... ended...");
    }
  }, [connection]);

  if (!user) {
    return <Navigate to="/" replace />;
  }

  return children;
};
