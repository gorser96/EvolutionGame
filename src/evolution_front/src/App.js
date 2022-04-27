import React from "react";
import { Route, Routes } from "react-router";
import { ProtectedRoute } from "./components/ProtectedRoute";
import { LoginProtectedRoute } from "./components/LoginProtectedRoute";
import Layout from "./components/Layout";
import Menu from "./components/Menu/Menu";
import Login from "./components/Login/Login";
import Register from "./components/Register/Register";
import { connect } from "react-redux";
import "./App.css";

const App = (props) => {
  return (
    <Layout>
      <Routes>
        <Route exact path="/" element={<LoginProtectedRoute user={localStorage.getItem("user")}><Login /></LoginProtectedRoute>} />
        <Route path="/register" element={<LoginProtectedRoute user={localStorage.getItem("user")}><Register /></LoginProtectedRoute>} />
        <Route path="/menu" element={<ProtectedRoute user={localStorage.getItem("user")}><Menu /></ProtectedRoute>} />
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
