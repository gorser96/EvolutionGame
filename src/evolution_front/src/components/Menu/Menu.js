import React, { useState } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";
import { Modal } from "@mui/material";
import { Box } from "@mui/system";
import "./Menu.css";

import { roomActions, userActions } from "../../actions";

const Menu = (props) => {
  let navigation = useNavigate();
  const [modalWindowOpened, setModalWindowOpened] = useState(false);
  const [roomName, setRoomName] = useState('');
  const [submitted, setSubmitted] = useState(false);

  const handleList = async (_) => {
    navigation('/room-list');
  };

  const handleCreateSubmit = async (_) => {
    setSubmitted(true);
    if (roomName) {
      props.create(roomName).then(
        result => navigation(`/room/${result.room.uid}`),
        failure => setRoomName(''));
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

  return (
    <div className="menu-window text-center">
      <div className="menu-block">
        <span className="menu-btn" onClick={handleList}>Поиск игры</span>
        <span className="menu-btn" onClick={handleOpenModal}>Создать игру</span>
        <span className="menu-btn">Профиль</span>
        <span className="menu-btn">Настройки</span>
        <span className="menu-btn" onClick={handleLogout}>Выход</span>
      </div>
      <Modal
        open={modalWindowOpened}
        onClose={handleCloseModal}
        aria-labelledby="modal-modal-title" aria-describedby="modal-modal-description">
        <Box className="modal-box">
          <div>
            <h4 className="mb-3">Введите название игры</h4>
            <input
              name="roomName"
              type="text"
              className="form-control mb-2"
              value={roomName}
              onChange={handleNameChange}
              placeholder="Название игры"
            />
            {submitted && !roomName && (
              <div className="h6 help-block">Room name is required</div>
            )}
            <button type="submit" className="btn btn-lg btn-success btn-block" onClick={handleCreateSubmit}>Создать</button>
          </div>
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
    logout: bindActionCreators(userActions.logout, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Menu);
