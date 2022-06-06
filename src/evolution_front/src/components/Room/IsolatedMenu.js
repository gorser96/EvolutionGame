import React, { useState, useCallback } from "react";
import { useNavigate } from "react-router-dom";

import { IconButton, Menu, MenuItem } from "@mui/material";
import MoreVertIcon from "@mui/icons-material/MoreVert";

const IsolatedMenu = (props) => {
  let navigation = useNavigate();
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handleProfile = useCallback(
    (uid) => {
      return (e) => {
        navigation(`/user/${uid}`);
      };
    },
    [navigation]
  );

  const handleKick = useCallback(
    (roomUid, userUid) => {
      return async (e) => {
        await props.kick(roomUid, userUid);
        // await props.get(roomUid);
      };
    },
    [props]
  );

  return (
    <React.Fragment>
      <IconButton
        aria-haspopup="true"
        aria-expanded={open ? "true" : undefined}
        onClick={(event) => setAnchorEl(event.currentTarget)}
      >
        <MoreVertIcon />
      </IconButton>
      <Menu
        anchorEl={anchorEl}
        open={open}
        keepMounted
        onClose={() => setAnchorEl(null)}
      >
        <MenuItem onClick={(e) => handleProfile(props.userUid)(e)}>
          Профиль
        </MenuItem>
        {props.isHost && (
          <MenuItem
            onClick={(e) => handleKick(props.roomUid, props.userUid)(e)}
          >
            Выгнать
          </MenuItem>
        )}
      </Menu>
    </React.Fragment>
  );
};

export default IsolatedMenu;
