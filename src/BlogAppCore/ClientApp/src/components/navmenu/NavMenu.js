import React from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.scss';

const NavMenu = () => (
  <nav className="nav-menu">
    <ul className="nav-menu__list">
      <li className="nav-menu__item nav-menu__item_active">
        <Link to="/">Home</Link>
      </li>
      <li className="nav-menu__item">
        <Link to="/about">About</Link>
      </li>
      <li className="nav-menu__item">
        <Link to="/contact">Contact</Link>
      </li>
    </ul>
  </nav>
);

export default NavMenu;
