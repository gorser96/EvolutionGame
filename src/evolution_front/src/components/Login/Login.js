import React, { Component } from 'react';
import './Login.css'

export class Login extends Component {  
  render () {
    return (
      <div className='fill-window text-center'>
        <form>
          <h1 className='h3 mb-3 font-weight-normal'>Please sign in</h1>
          <label className='sr-only'>Login</label>
          <input type="email" className="form-control"  aria-describedby="loginHelp" placeholder="Enter login" />
          <label className='sr-only'>Password</label>
          <input type="password" className="form-control" placeholder="Password" />
          <button type="submit" className="btn btn-lg btn-primary btn-block mt-3">Login</button>
        </form>
      </div>
    );
  }
}