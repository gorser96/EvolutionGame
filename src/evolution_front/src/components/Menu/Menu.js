import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";
import "./Menu.css";

import { roomActions, userActions } from "../../actions";

const Menu = (props) => {
  let navigation = useNavigate();

  const handleList = async (_) => {
    props.list().then(result => navigation('/room-list', { state: { result } }));
  };

  const handleCreate = async (_) => {
    await props.create(`room of ${props.authentication.user.userName}`);
  };

  const handleLogout = (_) => {
    props.logout();
  };

  return (
    <div className="menu-window text-center">
      <div className="menu-block">
        <span className="menu-btn" onClick={handleList}>Поиск игры</span>
        <span className="menu-btn" onClick={handleCreate}>Создать игру</span>
        <span className="menu-btn">Профиль</span>
        <span className="menu-btn">Настройки</span>
        <span className="menu-btn" onClick={handleLogout}>Выход</span>
      </div>
    </div>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    create: bindActionCreators(roomActions.create, dispatch),
    list: bindActionCreators(roomActions.list, dispatch),
    logout: bindActionCreators(userActions.logout, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Menu);
