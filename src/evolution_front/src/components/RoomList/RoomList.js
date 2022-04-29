import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";
import "./RoomList.css"

import { roomActions } from "../../actions";

const RoomList = (props) => {
  let navigation = useNavigate();

  const [ rooms, setRooms ] = useState(props.rooms);

  useEffect(() => {
    if (rooms === undefined) {
      props.list().then(result => {
        setRooms(result.rooms);
      });
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

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