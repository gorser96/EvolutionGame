import React from "react";
import "./Menu.css";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";

import { menuActions } from "../../actions";

const Menu = (props) => {
  return <div>Hello menu</div>;
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {
    create: bindActionCreators(menuActions.create, dispatch),
    enter: bindActionCreators(menuActions.enter, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Menu);
