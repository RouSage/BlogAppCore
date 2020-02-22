import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import LoginMenu from '../api-authorization/LoginMenu';
import './NavMenu.scss';

export default class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.state = {
      links: [
        { name: 'Home', to: '/' },
        { name: 'About', to: '/about' },
        { name: 'Contact', to: '/contact' },
      ],
    };
  }

  render() {
    const { links } = this.state;

    return (
      <nav className="nav-menu">
        <ul className="nav-menu__list">
          {links.map((link) => (
            <li key={link.name} className="nav-menu__item">
              <Link to={link.to}>{link.name}</Link>
            </li>
          ))}
        </ul>
        <LoginMenu></LoginMenu>
      </nav>
    );
  }
}
