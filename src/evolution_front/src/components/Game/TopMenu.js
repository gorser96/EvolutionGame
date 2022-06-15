import { useEffect } from "react";
import { connect, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { useParams, useNavigate } from "react-router-dom";

import { roomActions, systemActions } from "../../actions";
import { NotifySeverity } from "../../constants";
import { AppBar, IconButton, Toolbar } from "@mui/material";
import { ArrowBack } from "@mui/icons-material";

const TopMenu = (props) => {
  const navigation = useNavigate();
  const { uid } = useParams();
  const roomUidFromState = useSelector((state) => state.roomUsers.roomUid);
  const users = useSelector((state) => state.roomUsers.users);
  const user = useSelector((state) => state.authentication.user);

  useEffect(() => {
    props.getUsers(uid);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (
      roomUidFromState === uid &&
      users &&
      !users.find((x) => x.user.userName === user.userName)
    ) {
      navigation("/menu");
      props.sendNotification(
        "Вы отсутствуете в списке игроков!",
        NotifySeverity.Error
      );
    }
  }, [navigation, props, roomUidFromState, uid, user.userName, users]);

  const handleBack = (_) => {
    navigation("/menu");
  };

  const showPlayers = () => {
    if (users === undefined) {
      return "";
    }
    return (
      <ul>
        {users.map((player) => {
          return (
            <li key={player.user.userName}>
              <div>{player.user.userName}</div>
            </li>
          );
        })}
      </ul>
    );
  };

  return (
    <AppBar position="static" color="success">
      <Toolbar>
        <IconButton>
          <ArrowBack onClick={handleBack} />
        </IconButton>
        {showPlayers()}
      </Toolbar>
    </AppBar>
  );
};

const mapDispatchToProps = (dispatch) => {
  return {
    getUsers: bindActionCreators(roomActions.getUsers, dispatch),
    kick: bindActionCreators(roomActions.kick, dispatch),
    sendNotification: bindActionCreators(
      systemActions.sendNotification,
      dispatch
    ),
  };
};

export default connect(null, mapDispatchToProps)(TopMenu);
