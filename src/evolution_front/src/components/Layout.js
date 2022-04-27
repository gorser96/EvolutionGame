import React from "react";
import { Container } from "reactstrap";

export default function Layout(props) {
  return (
    <div>
      <Container>{props.children}</Container>
    </div>
  );
}
