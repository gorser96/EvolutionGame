import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useParams, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser, faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import "./Room.css";

import { roomActions, additionActions } from "../../actions";
import {
  Avatar,
  Select,
  MenuItem,
  OutlinedInput,
  Box,
  Tooltip,
  InputLabel,
  FormControl,
  ListItemText,
  Switch,
  FormControlLabel,
  TextField,
} from "@mui/material";

const Room = (props) => {
  let navigation = useNavigate();
  const { uid } = useParams();

  const [selectedAdditions, setSelectedAdditions] = useState([]);
  const [isPrivate, setIsPrivate] = useState(false);
  const [roomName, setRoomName] = useState("");
  const [cardsCount, setCardsCount] = useState(0);

  const [additions, setAdditions] = useState([]);

  const user = props.authentication.user;

  useEffect(() => {
    props.get(uid);
    props.additionList();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (props.roomState.room === undefined) {
      return;
    }
    if (props.additionState.additions !== undefined) {
      setAdditions(props.additionState.additions.filter((x) => !x.isBase));
    }
    setRoomName(props.roomState.room.name);
    setCardsCount(props.roomState.room.numOfCards);
  }, [props]);

  const handleChange = (e) => {
    const { name, value, checked } = e.target;
    if (name === "additions") {
      setSelectedAdditions(value);
    } else if (name === "privateSwitch") {
      setIsPrivate(checked);
    } else if (name === "nameText") {
      setRoomName(value);
    } else if (name === "cardsCount") {
      let num = Number(value);
      if (Number.isInteger(num) && num >= 0) {
        setCardsCount(value);
      }
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

  const isUserHost = (room) => {
    let inGameUser = room.inGameUsers.find(
      (x) => x.user.userName === user.userName
    );
    return inGameUser.isHost;
  };

  const getMIMEType = (iconName) => {
    let ext = iconName.split(".")[1];
    if (ext === "png") {
      return "image/png";
    } else if (ext === "jpg" || ext === "jpeg") {
      return "image/jpeg";
    }
  };

  const showOptions = () => {
    let room = props.roomState.room;
    if (room === undefined) {
      return "";
    }
    return (
      <ul className="options-list">
        <li className="options-row">
          <TextField
            disabled={!isUserHost(room)}
            name="nameText"
            label="Название"
            value={roomName}
            onChange={handleChange}
            variant="standard"
          />
        </li>
        <li className="options-row">
          <FormControl sx={{ width: "100%" }}>
            <InputLabel id="additions-label">Дополнения</InputLabel>
            <Select
              disabled={!isUserHost(room)}
              labelId="addition-label"
              name="additions"
              multiple
              value={selectedAdditions}
              onChange={handleChange}
              input={<OutlinedInput label="Дополнения" />}
              renderValue={(selected) => (
                <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                  {selected.map((value) => {
                    let addition = additions.find((x) => x.uid === value);
                    return (
                      <Tooltip title={addition.name} key={addition.uid}>
                        <img
                          className="rounded-circle"
                          src={`data:${getMIMEType(addition.iconName)};base64,${
                            addition.icon
                          }`}
                          alt={addition.iconName}
                          width={30}
                          height={30}
                        />
                      </Tooltip>
                    );
                  })}
                </Box>
              )}
            >
              {additions.map((item) => (
                <MenuItem key={item.uid} value={item.uid}>
                  <Tooltip title={item.name} key={item.uid}>
                    <img
                      className="rounded-circle"
                      src={`data:${getMIMEType(item.iconName)};base64,${
                        item.icon
                      }`}
                      alt={item.iconName}
                      width={20}
                      height={20}
                    />
                  </Tooltip>
                  <ListItemText primary={item.name} className="ms-1" />
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </li>
        <hr />
        <li className="options-row">
          <div className="me-2">Количество карт:</div>
          <TextField
            disabled={!isUserHost(room)}
            name="cardsCount"
            value={cardsCount}
            onChange={handleChange}
            type="number"
            InputLabelProps={{ shrink: true }}
            InputProps={{ inputProps: { min: 0, max: 100 } }}
            helperText="от 1 до "
            variant="standard"
          />
        </li>
        <hr />
        <li className="options-row">
          <FormControlLabel
            control={
              <Switch
                disabled
                name="privateSwitch"
                checked={isPrivate}
                onChange={handleChange}
              />
            }
            label="Закрытая комната"
          />
        </li>
      </ul>
    );
  };

  return (
    <div className="room-window">
      <div className="content-container">
        <div className="players-container">
          <div className="list-header">
            <div className="icons">
              <FontAwesomeIcon icon={faArrowLeft} onClick={handleBack} />
            </div>
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
    additionList: bindActionCreators(additionActions.list, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Room);
