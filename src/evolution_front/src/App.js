import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Menu from 'components/Menu';

function App() {
  return (
    <div className="wrapper">
      <h1>Application</h1>
      <BrowserRouter>
        <Switch>
          <Route path="/menu">
            <Menu />
          </Route>
        </Switch>
      </BrowserRouter>
    </div>
  );
}

export default App;
