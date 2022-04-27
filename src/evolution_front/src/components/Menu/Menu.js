import React from "react";
import "./Menu.css";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";

import { menuActions, userActions } from "../../actions";

const Menu = (props) => {

  const handleCreate = async (_) => {
    await props.enter('42a7248d-b87c-4bcc-989a-4860c5ea0815');
    //await props.create(`room of ${props.authentication.user.userName}`);
  };

  const handleLogout = (_) => {
    props.logout();
  };

  return (
  <div className="menuBlock">
    <div>Поиск игры</div>
    <div onClick={handleCreate} style={{cursor: 'pointer'}}>Создать игру</div>
    <div>Профиль</div>
    <div>Настройки</div>
    <div onClick={handleLogout} style={{cursor: 'pointer'}}>Выход</div>
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
