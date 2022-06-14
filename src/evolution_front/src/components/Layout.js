import { Container, Alert, Snackbar } from "@mui/material";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";

export default function Layout(props) {
  const [open, setOpen] = useState(false);
  const [msg, setMsg] = useState("");
  const [severity, setSeverity] = useState("success");

  const notify = useSelector((state) => state.notificationState.notify);

  useEffect(() => {
    if (notify !== undefined) {
      setMsg(notify.message);
      setSeverity(notify.severity);
      setOpen(true);
    }
  }, [notify, setMsg, setSeverity, setOpen]);

  return (
    <div style={{ height: "100%" }}>
      <Snackbar
        open={open}
        autoHideDuration={5000}
        onClose={() => setOpen(false)}
        sx={{ minWidth: 400, maxWidth: 600 }}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert
          onClose={() => setOpen(false)}
          severity={severity}
          sx={{ width: "100%" }}
        >
          {msg}
        </Alert>
      </Snackbar>
      <Container sx={{ height: "100%" }}>{props.children}</Container>
    </div>
  );
}
