import React, { useState, useEffect } from "react";
import { connect, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { useParams, useNavigate } from "react-router-dom";
import "./Room.css";

import { roomActions, additionActions } from "../../actions";
import {
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
import useSureDialog from "../hooks/SureDialogHook";

const OptionsList = (props) => {
  let navigation = useNavigate();
  const { uid } = useParams();
  
  const room = useSelector((state) => state.roomState.room);
  const additionsSelector = useSelector((state) => state.additionState.additions);
  const user = useSelector((state) => state.authentication.user);

  const [selectedAdditions, setSelectedAdditions] = useState([]);
  const [isPrivate, setIsPrivate] = useState(false);
  const [roomName, setRoomName] = useState("");
  const [cardsCount, setCardsCount] = useState(0);
  const [additions, setAdditions] = useState([]);

  const [sureDialog, showDialog] = useSureDialog();

  useEffect(() => {
    props.get(uid);
    props.additionList();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (room === undefined) {
      return;
    }
    if (additionsSelector !== undefined) {
      setAdditions(additionsSelector);
    }
    setRoomName(room.name);
    setCardsCount(room.numOfCards);
    setIsPrivate(room.isPrivate);
  }, [room, additionsSelector]);

  const getMIMEType = (iconName) => {
    let ext = iconName.split(".")[1];
    if (ext === "png") {
      return "image/png";
    } else if (ext === "jpg" || ext === "jpeg") {
      return "image/jpeg";
    }
  };

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

  const handleLeave = async () => {
    await props
      .leave(room.uid)
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
        navigation('/menu');
      },
      (failure) => {
        props.get(uid);
      }
    );
  };

  const handleRemove = async () => {
    const result = await showDialog();
    if (result) {
      await props.remove(uid).then(
        (result) => {
        },
        (failure) => {
          props.get(uid);
        }
      );
    }
  };

  const handleStart = async () => {
    console.log("Start game?");
  };

  const isUserHost = () => {
    let inGameUser = room.inGameUsers.find(
      (x) => x.user.userName === user.userName
    );
    if (inGameUser === undefined) {
      return false;
    }
    return inGameUser.isHost;
  };

  const showOptions = () => {
    if (room === undefined) {
      return "";
    }
    return (
      <ul className="options-list">
        <li className="options-row">
          <TextField
            disabled={!isUserHost()}
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
              disabled={!isUserHost()}
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
              disabled={!isUserHost()}
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
                disabled={!isUserHost()}
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
            {isUserHost() ? (
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
    <>
      {sureDialog}
      {showOptions()}
    </>
  );
};

const mapDispatchToProps = (dispatch) => {
  return {
    get: bindActionCreators(roomActions.get, dispatch),
    update: bindActionCreators(roomActions.update, dispatch),
    remove: bindActionCreators(roomActions.remove, dispatch),
    leave: bindActionCreators(roomActions.leave, dispatch),
    start: bindActionCreators(roomActions.start, dispatch),
    additionList: bindActionCreators(additionActions.list, dispatch),
  };
};

export default connect(null, mapDispatchToProps)(OptionsList);
