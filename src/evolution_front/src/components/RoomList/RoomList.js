import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";
import "./RoomList.css"

import { roomActions } from "../../actions";

const RoomList = (props) => {
  let navigation = useNavigate();

  const rooms = props.roomStates.rooms;

  useEffect(() => {
    if (rooms === undefined) {
      props.list().then(result => {
        rooms = result;
      });
    }
  });

  console.log(rooms);

  return (
    <div>
      test
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