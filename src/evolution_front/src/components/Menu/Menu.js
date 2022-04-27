import React from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import "./Menu.css";

import { menuActions, userActions } from "../../actions";

const Menu = (props) => {

  const handleEnter = async (_) => {
    await props.enter('42a7248d-b87c-4bcc-989a-4860c5ea0815');
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
        <span className="menu-btn" onClick={handleEnter}>Поиск игры</span>
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
    create: bindActionCreators(menuActions.create, dispatch),
    enter: bindActionCreators(menuActions.enter, dispatch),
    logout: bindActionCreators(userActions.logout, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Menu);
