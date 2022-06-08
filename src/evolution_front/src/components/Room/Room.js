import React, { useEffect } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import "./Room.css";

import { Box } from "@mui/material";
import { ArrowBack } from "@mui/icons-material";
import UsersList from "./UsersList";
import OptionsList from "./OptionsList";
import useSnackbar from "../hooks/SnackbarHook";

const Room = () => {
  let navigation = useNavigate();

  const [snackbar, sendNotification] = useSnackbar();

  const roomEvent = useSelector((state) => state.roomEvent);

  useEffect(() => {
    if (roomEvent.roomDeleted) {
      navigation('/menu');
      sendNotification(`Комната была удалена!`, "success");
    }
  }, [roomEvent, navigation, sendNotification]);

  const handleBack = (_) => {
    navigation(-1);
  };

  return (
    <Box component="div" className="room-window">
    {snackbar}
      <Box component="div" className="content-container">
        <Box component="div" className="players-container">
          <Box component="div" className="list-header">
            <Box component="div" className="icons">
              <ArrowBack onClick={handleBack} />
            </Box>
            <Box component="div" className="list-title">
              Список игроков
            </Box>
          </Box>
          <UsersList />
        </Box>
        <Box component="div" className="options-container">
          <Box component="div" className="list-title">
            Параметры игры
          </Box>
          <OptionsList sendNotification={sendNotification} />
        </Box>
      </Box>
    </Box>
  );
};

export default Room;
