import React from 'react';
import PropTypes from 'prop-types';
// import { NavMenu } from './NavMenu';

const Layout = ({ children }) => (
  <div>
    {/* <NavMenu /> */}
    {children}
  </div>
);

Layout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Layout;
