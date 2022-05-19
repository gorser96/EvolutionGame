import React, { useState } from "react";
import "./Login.css";
import logo from "../../img/gekkon.png";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { useNavigate } from "react-router-dom";

import { userActions } from "../../actions";
import {
  Box,
  Button,
  FormControl,
  FormHelperText,
  IconButton,
  InputAdornment,
  InputLabel,
  OutlinedInput,
  TextField,
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";

const Login = (props) => {
  let navigation = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
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
    navigation("/register");
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
    <Box className="login-window text-center">
      <FormControl component="form" onSubmit={handleSubmit}>
        <Box className="evo-name-block">
          <img
            className="evo-logo"
            alt="logo"
            src={logo}
            width="72"
            height="72"
          />
          <div className="evo-name">
            EVOLUTION
          </div>
        </Box>
        <TextField
          name="username"
          type="text"
          label="Username"
          placeholder="Enter username"
          variant="outlined"
          value={username}
          onChange={handleChange}
          error={submitted && !username}
          helperText={submitted && !username && "Username is required"}
          size="small"
          sx={{ width: "100%" }}
        />
        <FormControl sx={{ mt: 2, width: "100%" }}>
          <InputLabel htmlFor="outlined-adornment-password" size="small">
            Password
          </InputLabel>
          <OutlinedInput
            id="outlined-adornment-password"
            name="password"
            label="Password"
            type={showPassword ? "text" : "password"}
            value={password}
            onChange={handleChange}
            error={submitted && !password}
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={(e) => setShowPassword(!showPassword)}
                  edge="end"
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            }
            size="small"
            sx={{ width: "100%" }}
          />
          {submitted && !password && (
            <FormHelperText id="password_helper" error>
              Password is required
            </FormHelperText>
          )}
        </FormControl>
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            width: "100%",
            mt: 3,
          }}
        >
          <Button
            className="btn-block"
            variant="contained"
            color="success"
            sx={{ mr: 2 }}
            onClick={handleRegisterBtn}
          >
            Регистрация
          </Button>
          <Button
            type="submit"
            sx={{ ml: 2 }}
            className="btn-block"
            variant="contained"
            color="success"
          >
            Вход
          </Button>
        </Box>
        {loggingIn && (
          <img
            alt="loading"
            style={{ mt: 2 }}
            src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
          />
        )}
      </FormControl>
    </Box>
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
