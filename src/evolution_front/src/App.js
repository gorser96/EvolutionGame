import React from "react";
import { Route, Routes } from "react-router";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { LoginProtectedRoute } from "./components/LoginProtectedRoute";
import Layout from "./components/Layout";
import Menu from "./components/Menu/Menu";
import Room from "./components/Room/Room";
import RoomList from "./components/RoomList/RoomList";
import Login from "./components/Login/Login";
import Register from "./components/Login/Register";
import { connect } from "react-redux";
import "./App.css";

const App = (props) => {
  return (
    <Layout>
      <Routes>
        <Route exact path="/" element={<LoginProtectedRoute user={localStorage.getItem("user")}><Login /></LoginProtectedRoute>} />
        <Route path="/register" element={<LoginProtectedRoute user={localStorage.getItem("user")}><Register /></LoginProtectedRoute>} />
        <Route path="/menu" element={<ProtectedRoute user={localStorage.getItem("user")}><Menu /></ProtectedRoute>} />
        <Route path="/room-list" element={<ProtectedRoute user={localStorage.getItem("user")}><RoomList /></ProtectedRoute>} />
        <Route path="/room/:uid" element={<ProtectedRoute user={localStorage.getItem("user")}><Room /></ProtectedRoute>} />
        <Route path="*" element={<h1>404</h1>} />
      </Routes>
    </Layout>
  );
};

const mapState = (state) => {
  return state;
};

const mapDispatchToProps = (dispatch) => {
  return {};
};

export default connect(mapState, mapDispatchToProps)(App);
