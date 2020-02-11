import React from 'react';
import { Route } from 'react-router';
import Layout from '../Layout';
import Home from '../Home';
import { CreateCategory, EditCategory, CategoryTable } from '../categories';
import { TagTable, CreateTag, EditTag } from '../tags';
import {
  CreatePost, PostDetail, EditPost, GetByCategory, GetByTag,
} from '../posts';

const App = () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <Route path="/categories" component={CategoryTable} />
    <Route path="/create-category" component={CreateCategory} />
    <Route path="/edit-category/:id" component={EditCategory} />
    <Route path="/tags" component={TagTable} />
    <Route path="/create-tag" component={CreateTag} />
    <Route path="/edit-tag/:id" component={EditTag} />
    <Route path="/create-post" component={CreatePost} />
    <Route path="/edit-post/:slug" component={EditPost} />
    <Route path="/post/:slug" component={PostDetail} />
    <Route path="/archive/category/:categorySlug" component={GetByCategory} />
    <Route path="/archive/tag/:tagSlug" component={GetByTag} />
  </Layout>
);

export default App;
