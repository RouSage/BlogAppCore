import React from 'react';
import PropTypes from 'prop-types';
import NavMenu from './navmenu';
import Sidebar from './sidebar';
import { CategoryList } from './categories';
import { TagList } from './tags';

const Layout = ({ children }) => (
  <>
    <header className="header">
      <NavMenu />
    </header>
    <div className="container">
      <div className="main-content">
        <div className="content">{children}</div>
        <aside className="sidebar-container">
          <Sidebar>
            <CategoryList />
            <hr className="divider" />
            <TagList />
          </Sidebar>
        </aside>
      </div>
    </div>
  </>
);

Layout.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Layout;
