import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate, useLocation } from "react-router-dom";
import "./RoomList.css";

import { roomActions } from "../../actions";
import {
  ArrowBack,
  CalendarMonth,
  CheckCircle,
  MoreHoriz,
  Person,
  Search,
} from "@mui/icons-material";
import { Box, Button } from "@mui/material";
import useSnackbar from "../hooks/SnackbarHook";

const RoomList = (props) => {
  let navigation = useNavigate();
  let location = useLocation();
  const [snackbar, sendNotification] = useSnackbar();

  useEffect(() => {
    props.list();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [location]);

  const handleBack = (_) => {
    navigation("/menu");
  };

  const handleEnter = (roomUid) => {
    props.enter(roomUid).then(
      (result) => {
        navigation(`/room/${result.room.uid}`);
      },
      (error) => {
        sendNotification(error.error, "error");
        props.list();
      }
    );
  };

  const getMIMEType = (iconName) => {
    let ext = iconName.split(".")[1];
    if (ext === "png") {
      return "image/png";
    } else if (ext === "jpg" || ext === "jpeg") {
      return "image/jpeg";
    }
  };

  const addAdditionIcons = (room) => {
    return (
      <div className="profile-image">
        {room.additions.map((item) => (
          <img
            key={item.name}
            alt={item.name}
            className="rounded-circle"
            width={30}
            height={30}
            src={`data:${getMIMEType(item.iconName)};base64,${item.icon}`}
          />
        ))}
      </div>
    );
  };

  const showRooms = () => {
    let rooms = props.roomState.rooms;
    if (rooms === undefined) {
      return "";
    }
    return (
      <ul className="list list-inline">
        {rooms.map((room) => {
          return (
            <li key={room.name}>
              <div className="list-block">
                <CheckCircle className="text-success" />
                <div style={{ marginLeft: "0.75rem" }}>
                  <h4 style={{ marginBottom: "0" }}>{room.name}</h4>
                  <div
                    style={{
                      display: "flex",
                      flexDirection: "row",
                      marginTop: "0.5rem",
                      color: "rgba(0,0,0,.5)!important",
                    }}
                    className="date-time"
                  >
                    <Person />
                    <span style={{ fontSize: "1rem", marginLeft: "0.75rem" }}>
                      {room.inGameUsers.find((x) => x.isHost).user.userName}
                    </span>
                  </div>
                </div>
              </div>
              <div className="list-block">
                Количество карт: {room.numOfCards}
              </div>
              <div className="list-block">
                <Box sx={{ display: "flex", flexDirection: "column", mr: 2 }}>
                  <div className="profile-image">{addAdditionIcons(room)}</div>
                  <span className="date-time">
                    <CalendarMonth sx={{ color: "#6c757d!important", mr: 1 }} />
                    {new Date(room.createdDateTime + "Z").toLocaleString()}
                  </span>
                </Box>
                <Box sx={{ ml: 5 }}>
                  <Button
                    variant="contained"
                    color="success"
                    onClick={(_) => handleEnter(room.uid)}
                  >
                    Присоединиться
                  </Button>
                </Box>
              </div>
            </li>
          );
        })}
      </ul>
    );
  };

  return (
    <Box component="div" className="room-list-window">
      {snackbar}
      <Box component="div" className="list-rooms">
        <Box
          component="div"
          className="list-rooms-header"
        >
          <Box component="div" className="icons">
            <ArrowBack onClick={handleBack} />
          </Box>
          <Box component="div" className="list-title">
            Список игр
          </Box>
          <Box component="div" className="icons">
            <Search className="me-3" />
            <MoreHoriz />
          </Box>
        </Box>
        {showRooms()}
      </Box>
    </Box>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    list: bindActionCreators(roomActions.list, dispatch),
    enter: bindActionCreators(roomActions.enter, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(RoomList);
