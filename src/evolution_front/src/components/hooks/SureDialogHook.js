import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import React from "react";

export default function useSureDialog(onClose, open) {
  const handleClose = () => {
    onClose(false);
  };

  const handleClick = (value) => {
    onClose(value);
  };

  return (
    <Dialog onClose={handleClose} open={open}>
      <DialogTitle>Подтвердите действие</DialogTitle>
      <DialogContent>
        <DialogContentText>Вы уверены?</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={() => handleClick(false)}>Нет</Button>
        <Button onClick={() => handleClick(true)} autoFocus>
          Да
        </Button>
      </DialogActions>
    </Dialog>
  );
}
