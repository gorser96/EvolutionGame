import React from "react";
import { connect } from "react-redux";
import { useNavigate } from "react-router-dom";
import "./Room.css";

import { Box } from "@mui/material";
import { ArrowBack } from "@mui/icons-material";
import UsersList from "./UsersList";
import OptionsList from "./OptionsList";

const Room = (props) => {
  let navigation = useNavigate();

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
          <UsersList props />
        </Box>
        <Box component="div" className="options-container">
          <Box component="div" className="list-title">
            Параметры игры
          </Box>
          <OptionsList props />
        </Box>
      </Box>
    </Box>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {};
};

export default connect(mapState, mapDispatchToProps)(Room);
