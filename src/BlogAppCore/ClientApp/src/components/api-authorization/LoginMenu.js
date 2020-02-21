import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export default class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null,
    };
  }

  componentDidMount() {
    this.subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this.subscription);
  }

  authenticatedView = (userName, profilePath, logoutPath) => {
    return (
      <>
        <div>
          <span tag={Link} className="text-dark" to={profilePath}>
            {userName}
          </span>
        </div>
        <div>
          <span tag={Link} className="text-dark" to={logoutPath}>
            Logout
          </span>
        </div>
      </>
    );
  };

  anonymousView = (registerPath, loginPath) => {
    return (
      <>
        <div>
          <span tag={Link} className="text-dark" to={registerPath}>
            Register
          </span>
        </div>
        <div>
          <span tag={Link} className="text-dark" to={loginPath}>
            Login
          </span>
        </div>
      </>
    );
  };

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([
      authService.isAuthenticated(),
      authService.getUser(),
    ]);
    this.setState({
      isAuthenticated,
      userName: user && user.name,
    });
  }

  render() {
    const { isAuthenticated, userName } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    }
    const profilePath = `${ApplicationPaths.Profile}`;
    const logoutPath = {
      pathname: `${ApplicationPaths.LogOut}`,
      state: { local: true },
    };
    return this.authenticatedView(userName, profilePath, logoutPath);
  }
}
