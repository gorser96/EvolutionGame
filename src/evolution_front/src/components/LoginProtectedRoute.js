import React from "react";
import { Navigate } from "react-router-dom";

export const LoginProtectedRoute = ({ user, children }) => {
  if (user) {
    return <Navigate to="/menu" replace />;
  }

  return children;
};
