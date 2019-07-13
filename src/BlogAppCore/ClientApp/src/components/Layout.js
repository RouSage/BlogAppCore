import React from 'react';
import PropTypes from 'prop-types';
import NavMenu from './navmenu';
import Sidebar from './sidebar';
import { CategoryList } from './categories';
import { TagList } from './tags';

const Layout = ({ children }) => (
  <div className="container">
    <NavMenu />
    {children}
    <Sidebar>
      <CategoryList />
      <TagList />
    </Sidebar>
  </div>
);

Layout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Layout;
