import React from 'react';
import './App.css';
import { NavLink, Route, Routes } from 'react-router-dom';
import Menu from './components/Menu';

const AuthContext = React.createContext();

const fakeAuth = () =>
  new Promise((resolve) => {
    setTimeout(() => resolve('2342f2f1d131rf12'), 250);
  });

const AuthProvider = ({ children }) => {
  const [token, setToken] = React.useState(null);

  const handleLogin = async () => {
    const token = await fakeAuth();

    setToken(token);
  };

  const handleLogout = () => {
    setToken(null);
  };

  const value = {
    token,
    onLogin: handleLogin,
    onLogout: handleLogout,
  };

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
};

const useAuth = () => {
  return React.useContext(AuthContext);
};

function App() {
  return (
    <AuthProvider>

      <div>
        <h1>Application</h1>

        <Navigation />

        <Routes>
          <Route path="menu" element={<Menu />} />
        </Routes>
      </div>

    </AuthProvider>
  );
}

const Navigation = () => {
  const { token, onLogout } = useAuth();
  return (
    <nav>
      <NavLink to="/menu">Menu</NavLink>
      {token && (
        <button type="button" onClick={onLogout}>
          Sign Out
        </button>
      )}
    </nav>
  );
};

export default App;
