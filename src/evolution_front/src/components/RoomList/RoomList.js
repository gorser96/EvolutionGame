import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import "./RoomList.css";

import { roomActions } from "../../actions";

const RoomList = (props) => {
  useEffect(() => {
    props.list();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const showRooms = () => {
    let rooms = props.roomState.rooms;
    if (rooms === undefined) {
      return "";
    }
    return (
      <div className="list-rooms">
        {rooms.map((room) => {
          console.log(room);
          return (
            <div className="list-rooms-row" key={room.name}>
              <div className="list-rooms-row-prop">{room.name}</div>
              <div className="list-rooms-row-prop">{room.inGameUsers.find((x) => x.isHost).user.userName}</div>
            </div>
          );
        })}
      </div>
    );
  };

  return (
    <div className="room-list-window d-flex flex-row">
      <div className="d-flex flex-column">
        <div className="list-title">Список игр</div>
        {showRooms()}
      </div>
      <div className="list-options"></div>
    </div>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    list: bindActionCreators(roomActions.list, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(RoomList);
