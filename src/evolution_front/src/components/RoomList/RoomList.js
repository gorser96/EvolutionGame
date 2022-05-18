import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";
import "./RoomList.css";

import { roomActions } from "../../actions";
import { ArrowBack, CalendarMonth, CheckCircle, MoreHoriz, Person, Search } from "@mui/icons-material";

const RoomList = (props) => {
  let navigation = useNavigate();

  useEffect(() => {
    props.list();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleBack = (_) => {
    navigation("/menu");
  };

  const handleEnter = (roomUid) => {
    console.log(roomUid);
    props.enter(roomUid).then((room) => navigation(`/room/${room.uid}`));
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
            <li className="d-flex justify-content-between" key={room.name}>
              <div className="d-flex flex-row align-items-center">
                <CheckCircle
                  className="text-success"
                />
                <div className="ms-2">
                  <h4 className="mb-0">{room.name}</h4>
                  <div className="d-flex flex-row mt-1 text-black-50 date-time">
                    <div>
                      <Person />
                      <span className="h6 ms-2">
                        {room.inGameUsers.find((x) => x.isHost).user.userName}
                      </span>
                    </div>
                  </div>
                </div>
              </div>
              <div className="d-flex flex-row align-items-center">
                Количество карт: {room.numOfCards}
              </div>
              <div className="d-flex flex-row align-items-center">
                <div className="d-flex flex-column me-2">
                  <div className="profile-image">{addAdditionIcons(room)}</div>
                  <span className="date-time">
                    <CalendarMonth
                      className="text-secondary me-1"
                    />
                    {new Date(room.createdDateTime).toLocaleString()}
                  </span>
                </div>
                <div className="ms-5">
                  <span
                    className="btn btn-success"
                    onClick={(_) => handleEnter(room.uid)}
                  >
                    Присоединиться
                  </span>
                </div>
              </div>
            </li>
          );
        })}
      </ul>
    );
  };

  return (
    <div className="room-list-window">
      <div className="list-rooms">
        <div className="d-flex justify-content-between align-items-center activity">
          <div className="icons">
            <ArrowBack onClick={handleBack} />
          </div>
          <div className="list-title">
            <span className="activity-done">Список игр</span>
          </div>
          <div className="icons">
            <Search className="me-3" />
            <MoreHoriz />
          </div>
        </div>
        {showRooms()}
      </div>
    </div>
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
