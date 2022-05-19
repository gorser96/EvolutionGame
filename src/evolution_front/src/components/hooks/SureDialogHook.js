import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import React, { useState, useRef } from "react";

export default function useSureDialog() {
  const [open, setOpen] = useState(false);
  const resolveFunc = useRef(undefined);

  const handleResult = (value) => {
    setOpen(false);
    if (resolveFunc.current !== undefined) {
      resolveFunc.current(value);
    }
  };

  const showDialog = () => {
    setOpen(true);
    return new Promise((resolve, reject) => {
      resolveFunc.current = resolve;
    });
  };

  return [
    <Dialog onClose={() => handleResult(false)} open={open}>
      <DialogTitle>Подтвердите действие</DialogTitle>
      <DialogContent>
        <DialogContentText>Вы уверены?</DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={() => handleResult(false)}>Нет</Button>
        <Button onClick={() => handleResult(true)} autoFocus>
          Да
        </Button>
      </DialogActions>
    </Dialog>,
    showDialog,
  ];
}
