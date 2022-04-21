import React from 'react';
import useAuth from '../App'

export default function Menu() {
  const { onLogin } = useAuth();
  
  return(
    <>
      <h2>Menu</h2>

      <button type="button" onClick={onLogin}>Sign In</button>
    </>
  );
}