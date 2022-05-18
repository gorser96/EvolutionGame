import { Container } from "@mui/material";
import React from "react";

export default function Layout(props) {
  return (
    <div>
      <Container>{props.children}</Container>
    </div>
  );
}
