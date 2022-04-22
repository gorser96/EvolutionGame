import React, { Component } from 'react';
import { Route, Routes } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Login } from './components/Login/Login';
import './App.css';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Routes>
          <Route exact path='/' element={<Login />} />
          <Route path='/home' element={<Home />} />
        </Routes>
      </Layout>
    );
  }
}
