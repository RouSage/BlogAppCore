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
    <Route path="/category/new" component={CreateCategory} />
    <Route path="/category/edit/:id" component={EditCategory} />
    <Route path="/tags" component={TagTable} />
    <Route path="/tag/new" component={CreateTag} />
    <Route path="/tag/edit/:id" component={EditTag} />
    <Route path="/post/new" component={CreatePost} />
    <Route path="/post/:slug" component={PostDetail} />
    <Route path="/post/edit/:slug" component={EditPost} />
    <Route path="/archive/category/:categorySlug" component={GetByCategory} />
    <Route path="/archive/tag/:tagSlug" component={GetByTag} />
  </Layout>
);

export default App;
