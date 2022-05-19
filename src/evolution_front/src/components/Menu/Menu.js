import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";
import { Button, FormControl, Modal, TextField } from "@mui/material";
import { Box } from "@mui/system";
import "./Menu.css";

import { roomActions, userActions } from "../../actions";

const Menu = (props) => {
  let navigation = useNavigate();
  const [modalWindowOpened, setModalWindowOpened] = useState(false);
  const [roomName, setRoomName] = useState("");
  const [submitted, setSubmitted] = useState(false);

  useEffect(() => {
    props.user();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleList = async (_) => {
    navigation("/room-list");
  };

  const handleCreateSubmit = async (_) => {
    setSubmitted(true);
    if (roomName) {
      props.create(roomName).then(
        (result) => navigation(`/room/${result.room.uid}`),
        (failure) => setRoomName("")
      );
    }
  };

  const handleNameChange = (e) => {
    const { name, value } = e.target;
    if (name === "roomName") {
      setRoomName(value);
    }
  };

  const handleOpenModal = (_) => {
    setModalWindowOpened(true);
  };
  const handleCloseModal = (_) => {
    setModalWindowOpened(false);
  };

  const handleLogout = (_) => {
    props.logout();
  };

  const handleEnter = (_) => {
    navigation(`/room/${props.roomState.room.uid}`);
  };

  const showPlayButtons = () => {
    let room = props.roomState.room;
    if (room === undefined || room === "") {
      return (
        <>
          <span className="menu-btn" onClick={handleList}>
            Поиск игры
          </span>
          <span className="menu-btn" onClick={handleOpenModal}>
            Создать игру
          </span>
        </>
      );
    }
    return (
      <>
        <span className="menu-btn" onClick={handleEnter}>
          Моя игра
        </span>
        <span className="menu-btn" onClick={handleList}>
          Список игр
        </span>
      </>
    );
  };

  return (
    <div className="menu-window text-center">
      <div className="menu-block">
        {showPlayButtons()}
        <span className="menu-btn">Профиль</span>
        <span className="menu-btn">Настройки</span>
        <span className="menu-btn" onClick={handleLogout}>
          Выход
        </span>
      </div>
      <Modal
        open={modalWindowOpened}
        onClose={handleCloseModal}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box className="modal-box">
          <FormControl component="div">
            <h4 style={{ marginBottom: "1rem" }}>Введите название игры</h4>
            <TextField
              name="roomName"
              type="text"
              style={{ marginBottom: "1rem" }}
              value={roomName}
              onChange={handleNameChange}
              placeholder="Название игры"
              label="Название игры"
              size="small"
              error={submitted && !roomName}
              helperText={submitted && !roomName && "Room name is required"}
            />
            <Button
              type="submit"
              className="btn-block"
              variant="contained"
              color="success"
              onClick={handleCreateSubmit}
            >
              Создать
            </Button>
          </FormControl>
        </Box>
      </Modal>
    </div>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    create: bindActionCreators(roomActions.create, dispatch),
    user: bindActionCreators(roomActions.user, dispatch),
    logout: bindActionCreators(userActions.logout, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Menu);
