import { Alert, Snackbar } from "@mui/material";
import React from "react";

export default function useSnackbar() {
  const [open, setOpen] = React.useState(false);
  const [msg, setMsg] = React.useState("");
  const [severity, setSeverity] = React.useState("success");

  const sendNotification = (msg, severity = "success") => {
    setMsg(msg);
    setSeverity(severity);
    setOpen(true);
  };

  return [
    <Snackbar
      open={open}
      autoHideDuration={5000}
      onClose={() => setOpen(!open)}
      sx={{ minWidth: 400, maxWidth: 600 }}
      anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
    >
      <Alert
        onClose={() => setOpen(!open)}
        severity={severity}
        sx={{ width: "100%" }}
      >
        {msg}
      </Alert>
    </Snackbar>,
    sendNotification,
  ];
}
