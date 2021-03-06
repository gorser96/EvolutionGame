import { useEffect } from "react";
import { connect, useSelector } from "react-redux";
import { bindActionCreators } from "redux";
import { useParams, useNavigate } from "react-router-dom";

import { roomActions, systemActions } from "../../actions";
import { NotifySeverity } from "../../constants";
import { Avatar } from "@mui/material";
import { Person } from "@mui/icons-material";
import IsolatedMenu from "./IsolatedMenu";

const UsersList = (props) => {
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
    if (roomUidFromState === uid && users && !users.find((x) => x.user.userName === user.userName)) {
      navigation("/menu");
      props.sendNotification(
        "Вы отсутствуете в списке игроков!",
        NotifySeverity.Error
      );
    }
  }, [navigation, props, roomUidFromState, uid, user.userName, users]);

  const getAvatar = (user) => {
    return <Avatar>{user.userName.charAt(0).toUpperCase()}</Avatar>;
  };

  const isUserHost = () => {
    let inGameUser = users.find((x) => x.user.userName === user.userName);
    if (inGameUser === undefined) {
      return false;
    }
    return inGameUser.isHost;
  };

  const showPlayerButtons = (player) => {
    let components = [];
    if (player.isHost) {
      components.push(<Person key={0} />);
    }

    if (user.userName !== player.user.userName) {
      components.push(
        <IsolatedMenu
          key={1}
          isHost={isUserHost()}
          kick={props.kick}
          get={props.get}
          roomUid={uid}
          userUid={player.user.id}
        />
      );
    }

    return <>{components}</>;
  };

  const showPlayers = () => {
    if (users === undefined) {
      return "";
    }
    return (
      <ul className="players-list">
        {users.map((player) => {
          return (
            <li key={player.user.userName} className="player-row">
              {getAvatar(player.user)}
              <div style={{ marginLeft: "1rem" }}>{player.user.userName}</div>
              <div style={{ marginLeft: "auto" }}>
                {showPlayerButtons(player)}
              </div>
            </li>
          );
        })}
      </ul>
    );
  };

  return <>{showPlayers()}</>;
};

const mapDispatchToProps = (dispatch) => {
  return {
    getUsers: bindActionCreators(roomActions.getUsers, dispatch),
    kick: bindActionCreators(roomActions.kick, dispatch),
    sendNotification: bindActionCreators(systemActions.sendNotification, dispatch),
  };
};

export default connect(null, mapDispatchToProps)(UsersList);
