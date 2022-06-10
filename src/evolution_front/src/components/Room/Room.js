import React, { useEffect } from "react";
import { connect, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate, useParams } from "react-router-dom";
import "./Room.css";

import { Box } from "@mui/material";
import { ArrowBack } from "@mui/icons-material";
import { systemActions } from "../../actions";
import { NotifySeverity } from "../../constants";
import UsersList from "./UsersList";
import OptionsList from "./OptionsList";

const Room = (props) => {
  const { uid } = useParams();
  const navigation = useNavigate();

  const roomEvent = useSelector((state) => state.roomEvent);

  useEffect(() => {
    if (roomEvent.roomDeleted && roomEvent.roomUid === uid) {
      navigation("/menu");
      props.sendNotification("Комната была удалена!", NotifySeverity.Warning);
    }
  }, [roomEvent, navigation, uid, props]);

  const handleBack = (_) => {
    navigation(-1);
  };

  return (
    <Box component="div" className="room-window">
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
          <OptionsList />
        </Box>
      </Box>
    </Box>
  );
};

const mapDispatchToProps = (dispatch) => {
  return {
    sendNotification: bindActionCreators(
      systemActions.sendNotification,
      dispatch
    ),
  };
};

export default connect(null, mapDispatchToProps)(Room);
