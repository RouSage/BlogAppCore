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
      <ul className="nav-menu__list">
        <li className="nav-menu__item">
          <Link className="text-dark" to={profilePath}>
            {userName}
          </Link>
        </li>
        <li className="nav-menu__item">
          <Link className="text-dark" to={logoutPath}>
            Logout
          </Link>
        </li>
      </ul>
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
      // const loginPath = `${ApplicationPaths.Login}`;
      // return this.anonymousView(loginPath);
      return null;
    }
    const profilePath = `${ApplicationPaths.Profile}`;
    const logoutPath = {
      pathname: `${ApplicationPaths.LogOut}`,
      state: { local: true },
    };
    return this.authenticatedView(userName, profilePath, logoutPath);
  }
}
