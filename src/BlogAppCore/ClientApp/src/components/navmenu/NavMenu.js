import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import LoginMenu from '../api-authorization/LoginMenu';
import authService from '../api-authorization/AuthorizeService';
import './NavMenu.scss';

export default class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.state = {
      navLinks: [
        { name: 'Home', to: '/' },
        { name: 'About', to: '/about' },
        { name: 'Contact', to: '/contact' },
      ],
      adminLinks: [
        { name: 'Posts', to: '/posts' },
        { name: 'Categories', to: '/categories' },
        { name: 'Tags', to: '/tags' },
      ],
      isAuthenticated: false,
    };
  }

  componentDidMount() {
    this.populateState();
  }

  async populateState() {
    const isAuthenticated = await authService.isAuthenticated();
    this.setState({
      isAuthenticated,
    });
  }

  render() {
    const { navLinks: links, adminLinks, isAuthenticated } = this.state;

    return (
      <nav className="nav-menu">
        <ul className="nav-menu__list">
          {links.map((link) => (
            <li key={link.name} className="nav-menu__item">
              <Link to={link.to}>{link.name}</Link>
            </li>
          ))}
          {isAuthenticated &&
            adminLinks.map((adminLink) => (
              <li key={adminLink.name} className="nav-menu__item">
                <Link to={adminLink.to}>{adminLink.name}</Link>
              </li>
            ))}
        </ul>
        <LoginMenu />
      </nav>
    );
  }
}
