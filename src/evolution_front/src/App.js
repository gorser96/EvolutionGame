import React from "react";
import { Route, Routes } from "react-router";
import { ProtectedRoute } from "./components/ProtectedRoute";
import Layout from "./components/Layout";
import Menu from "./components/Menu/Menu";
import Login from "./components/Login/Login";
import { connect } from "react-redux";
import "./App.css";

const App = (props) => {
  return (
    <Layout>
      <Routes>
        <Route exact path="/" element={<Login />} />
        <Route
          path="/menu"
          element={
            <ProtectedRoute user={localStorage.getItem("user")}>
              <Menu />
            </ProtectedRoute>
          }
        />
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
