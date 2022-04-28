import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useParams } from 'react-router-dom';
import "./Room.css"

const Room = (props) => {
  let { uid } = useParams();
  return (
    <div>

    </div>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
  };
};

export default connect(mapState, mapDispatchToProps)(Room);