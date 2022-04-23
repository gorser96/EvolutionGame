import React, { Component } from 'react';
import './Login.css'
import logo from '../../img/gekkon.png'

export class Login extends Component {  
  render () {
    return (
      <div className='fill-window text-center'>
        <form>
          <div className='evo-name-block'>
            <img className='mb-4 evo-logo me-3 ms-4' src={logo} width="72" height="72" />
            <div className='evo-name'>EVOLUTION</div>
          </div>
          <input type="text" className="form-control"  aria-describedby="loginHelp" placeholder="Enter login" />
          <input type="password" className="mt-1 form-control" placeholder="Password" />
          <button type="submit" className="btn btn-lg btn-success btn-block mt-3">Вход</button>
        </form>
      </div>
    );
  }
}