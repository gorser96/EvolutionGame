import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useParams, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUser, faArrowLeft } from '@fortawesome/free-solid-svg-icons'
import "./Room.css"
import plant_img from '../../img/plant.jpg';
import jellyfish_img from '../../img/jellyfish.jpg';
import pterodactyl_img from '../../img/pterodactyl.jpg';

import { roomActions } from "../../actions";
import {
  Avatar, Select, MenuItem, OutlinedInput,
  Box, Tooltip, InputLabel, FormControl, ListItemText,
  Switch, FormControlLabel, Slider
} from "@mui/material";

const AdditionImgDict = {
  timeToFly: {
    icon: (iconSize) => (
      <Tooltip title="Время летать">
        <img className="rounded-circle" src={pterodactyl_img} alt="pterodactyl" width={iconSize} height={iconSize} />
      </Tooltip>
    ),
    name: "Время летать"
  },
  plants: {
    icon: (iconSize) => (
      <Tooltip title="Растения">
        <img className="rounded-circle" src={plant_img} alt="plant" width={iconSize} height={iconSize} />
      </Tooltip>
    ),
    name: "Растения"
  },
  continents: {
    icon: (iconSize) => (
      <Tooltip title="Континенты">
        <img className="rounded-circle" src={jellyfish_img} alt="jellyfish" width={iconSize} height={iconSize} />
      </Tooltip>
    ),
    name: "Континенты",
  },
};

const Room = (props) => {
  let navigation = useNavigate();
  const { uid } = useParams();
  const [selectedAdditions, setSelectedAdditions] = useState([]);
  let [isPrivate, setIsPrivate] = useState(false);

  useEffect(() => {
    props.get(uid);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleChange = (e) => {
    const { name, value, checked } = e.target;
    if (name === "additions") {
      setSelectedAdditions(value);
    } else if (name === "privateSwitch") {
      setIsPrivate(checked);
    }
  };

  const handleBack = (_) => {
    navigation(-1);
  };

  const getAvatar = (user) => {
    return <Avatar>{user.userName.charAt(0).toUpperCase()}</Avatar>;
  };

  const showIsHost = (isHost) => {
    if (isHost) {
      return <FontAwesomeIcon icon={faUser} />;
    }
    return "";
  };

  const showPlayers = () => {
    let room = props.roomState.room;
    if (room === undefined) {
      return "";
    }
    return (
      <ul className="players-list">
        {room.inGameUsers.map((player) => {
          return (
            <li key={player.user.userName} className="player-row">
              {getAvatar(player.user)}
              <div className="ms-2">{player.user.userName}</div>
              <div className="ms-auto">{showIsHost(player.isHost)}</div>
            </li>
          );
        })}
      </ul>
    );
  };

  const showOptions = () => {
    let room = props.roomState.room;
    if (room === undefined) {
      return "";
    }
    return (
      <ul className="options-list">
        <li className="options-row">
          <FormControl sx={{ width: "100%" }}>
            <InputLabel id="additions-label">Дополнения</InputLabel>
            <Select
              labelId="addition-label"
              name="additions"
              multiple
              value={selectedAdditions}
              onChange={handleChange}
              input={<OutlinedInput label="Дополнения" />}
              renderValue={(selected) => (
                <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                  {selected.map((value) => AdditionImgDict[value].icon(30))}
                </Box>
              )}>
              {Object.keys(AdditionImgDict).map((key) => (
                <MenuItem key={key} value={key}>
                  {AdditionImgDict[key].icon(20)}
                  <ListItemText primary={AdditionImgDict[key].name} className="ms-1" />
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </li>
        <hr />
        <li className="options-row">
          <div>Количество карт: 0</div>
        </li>
        <hr />
        <li className="options-row">
          <FormControlLabel control={<Switch name="privateSwitch" checked={isPrivate} onChange={handleChange} />} label="Закрытая комната" />
        </li>
      </ul>
    );
  };

  return (
    <div className="room-window">
      <div className="content-container">
        <div className="players-container">
          <div className="list-header">
            <div className="icons"><FontAwesomeIcon icon={faArrowLeft} onClick={handleBack} /></div>
            <div className="list-title">Список игроков</div>
          </div>
          {showPlayers()}
        </div>
        <div className="options-container">
          <div className="list-title">Параметры игры</div>
          {showOptions()}
        </div>
      </div>
    </div>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    get: bindActionCreators(roomActions.get, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Room);