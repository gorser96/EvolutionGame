import React, { useState } from "react";
import "./Login.css";
import logo from "../../img/gekkon.png";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";

import { userActions } from "../../actions";

const Login = (props) => {
  let navigation = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [submitted, setSubmitted] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "username") {
      setUsername(value);
    } else if (name === "password") {
      setPassword(value);
    }
  };

  const handleRegisterBtn = (e) => {
    navigation('/register');
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    setSubmitted(true);
    if (username && password) {
      props.login(username, password).then((_) => {
        navigation("/menu");
      });
    }
  };
  const { loggingIn } = props;

  return (
    <div className="login-window text-center">
      <form onSubmit={handleSubmit}>
        <div className="evo-name-block">
          <img
            className="mb-4 evo-logo me-3 ms-4"
            alt="logo"
            src={logo}
            width="72"
            height="72"
          />
          <div className="evo-name">EVOLUTION</div>
        </div>
        <input
          name="username"
          type="text"
          className="form-control"
          value={username}
          onChange={handleChange}
          placeholder="Enter username"
        />
        {submitted && !username && (
          <div className="help-block">Username is required</div>
        )}
        <input
          name="password"
          type="password"
          className="mt-1 form-control"
          placeholder="Password"
          value={password}
          onChange={handleChange}
        />
        {submitted && !password && (
          <div className="help-block">Password is required</div>
        )}
        <div className="d-flex justify-content-between w-100 mt-3">
          <button type="button" className="btn btn-lg btn-success btn-block me-2" onClick={handleRegisterBtn}>Регистрация</button>
          <button type="submit" className="btn btn-lg btn-success btn-block ms-2">Вход</button>
        </div>
        {loggingIn && (
          <img
            alt="loading"
            src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
          />
        )}
      </form>
    </div>
  );
};

const mapState = (state) => {
  const { loggingIn } = state.authentication;
  return { loggingIn };
};

const mapDispatchToProps = (dispatch) => {
  return {
    login: bindActionCreators(userActions.login, dispatch),
  };
};

export default connect(mapState, mapDispatchToProps)(Login);
