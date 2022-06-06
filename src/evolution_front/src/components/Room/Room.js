import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useParams, useNavigate } from "react-router-dom";
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
  Button,
} from "@mui/material";
import { ArrowBack, Person } from "@mui/icons-material";
import useSureDialog from "../hooks/SureDialogHook";
import useSnackbar from "../hooks/SnackbarHook";
import IsolatedMenu from "./IsolatedMenu";

const Room = (props) => {
  let navigation = useNavigate();
  const { uid } = useParams();
  const [snackbar, sendNotification] = useSnackbar();

  const [selectedAdditions, setSelectedAdditions] = useState([]);
  const [isPrivate, setIsPrivate] = useState(false);
  const [roomName, setRoomName] = useState("");
  const [cardsCount, setCardsCount] = useState(0);
  const [additions, setAdditions] = useState([]);

  const [sureDialog, showDialog] = useSureDialog();

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
      setAdditions(props.additionState.additions);
    }
    setRoomName(props.roomState.room.name);
    setCardsCount(props.roomState.room.numOfCards);
    setIsPrivate(props.roomState.room.isPrivate);
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
        setCardsCount(num);
      }
    }
  };

  const handleBack = (_) => {
    navigation(-1);
  };

  const getAvatar = (user) => {
    return <Avatar>{user.userName.charAt(0).toUpperCase()}</Avatar>;
  };

  const isUserHost = (room) => {
    let inGameUser = room.inGameUsers.find(
      (x) => x.user.userName === user.userName
    );
    if (inGameUser === undefined) {
      return false;
    }
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

  const handleLeave = async () => {
    await props
      .leave(props.roomState.room.uid)
      .then((result) => navigation("/room-list"));
  };

  const handleSave = async () => {
    const room = {
      name: roomName,
      maxTimeLeft: undefined,
      additions: additions.map((x) => x.uid),
      isPrivate: isPrivate,
      numOfCards: cardsCount,
    };
    await props.update(uid, room).then(
      (result) => {
        sendNotification(`Комната ${result.room.name} сохранена!`, "success");
      },
      (failure) => {
        sendNotification(failure.error, "error");
        props.get(uid);
      }
    );
  };

  const handleRemove = async () => {
    const result = await showDialog();
    if (result) {
      await props.remove(uid).then(
        (result) => {
          sendNotification(`Комната удалена!`, "success");
          navigation(-1);
        },
        (failure) => {
          sendNotification(failure.error, "error");
          props.get(uid);
        }
      );
    }
  };

  const handleStart = async () => {
    console.log("Start game?");
  };

  const showPlayerButtons = (player, room) => {
    let components = [];
    if (player.isHost) {
      components.push(<Person key={0} />);
    }

    if (user.userName !== player.user.userName) {
      components.push(
        <IsolatedMenu
          key={1}
          isHost={isUserHost(room)}
          kick={props.kick}
          get={props.get}
          roomUid={room.uid}
          userUid={player.user.id}
        />
      );
    }

    return <>{components}</>;
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
              <div style={{ marginLeft: "1rem" }}>{player.user.userName}</div>
              <div style={{ marginLeft: "auto" }}>
                {showPlayerButtons(player, room)}
              </div>
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
              {additions
                .filter((x) => !x.isBase)
                .map((item) => (
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
          <FormControl>
            <TextField
              disabled={!isUserHost(room)}
              name="cardsCount"
              label="Количество карт"
              value={cardsCount}
              onChange={handleChange}
              type="number"
              InputLabelProps={{ shrink: true }}
              InputProps={{ inputProps: { min: 0, max: 100 } }}
              helperText={`от 1 до ${
                room.additions
                  .map((a) => a.cards.length)
                  .reduce((ps, count) => ps + count, 0) || 0
              }`}
              variant="standard"
              sx={{ minWidth: 100, maxWidth: 250 }}
            />
          </FormControl>
        </li>
        <hr />
        <li className="options-row">
          <FormControlLabel
            control={
              <Switch
                disabled={!isUserHost(room)}
                name="privateSwitch"
                checked={isPrivate}
                onChange={handleChange}
              />
            }
            label="Закрытая комната"
          />
        </li>
        <hr />
        <li className="options-row">
          <FormControl
            sx={{
              mt: 2,
              display: "flex",
              flexDirection: "row",
              justifyContent: "center",
              width: "100%",
            }}
          >
            {isUserHost(room) ? (
              <Box
                sx={{ display: "flex", flexDirection: "column", width: "100%" }}
              >
                <Box
                  sx={{ display: "flex", flexDirection: "row-reverse", mb: 2 }}
                >
                  <Button
                    variant="contained"
                    color="error"
                    onClick={handleRemove}
                  >
                    Удалить комнату
                  </Button>
                </Box>
                <Box
                  sx={{ display: "flex", flexDirection: "row-reverse", mb: 2 }}
                >
                  <Button
                    variant="contained"
                    color="success"
                    onClick={handleStart}
                    sx={{ ml: 2 }}
                  >
                    Начать игру
                  </Button>
                  <Button
                    variant="contained"
                    color="primary"
                    onClick={handleSave}
                  >
                    Сохранить
                  </Button>
                </Box>
              </Box>
            ) : (
              <>
                <Button variant="contained" color="error" onClick={handleLeave}>
                  Покинуть комнату
                </Button>
              </>
            )}
          </FormControl>
        </li>
      </ul>
    );
  };

  return (
    <Box component="div" className="room-window">
      {sureDialog}
      {snackbar}
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
          {showPlayers()}
        </Box>
        <Box component="div" className="options-container">
          <Box component="div" className="list-title">
            Параметры игры
          </Box>
          {showOptions()}
        </Box>
      </Box>
    </Box>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    get: bindActionCreators(roomActions.get, dispatch),
    update: bindActionCreators(roomActions.update, dispatch),
    remove: bindActionCreators(roomActions.remove, dispatch),
    leave: bindActionCreators(roomActions.leave, dispatch),
    kick: bindActionCreators(roomActions.kick, dispatch),
    start: bindActionCreators(roomActions.start, dispatch),
    additionList: bindActionCreators(additionActions.list, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Room);
