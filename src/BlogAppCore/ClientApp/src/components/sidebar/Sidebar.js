import React from 'react';
import { CategoryList } from '../categories';
import { TagList } from '../tags';

const Sidebar = () => (
  <div className="sidebar">
    <CategoryList />
    <TagList />
  </div>
);

export default Sidebar;
