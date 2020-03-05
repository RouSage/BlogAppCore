import React from 'react';
import { Route } from 'react-router';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faEdit, faTrashAlt, faPlus } from '@fortawesome/free-solid-svg-icons';
import Layout from '../Layout';
import Home from '../Home';
import { CreateCategory, EditCategory, CategoryTable } from '../categories';
import { TagTable, CreateTag, EditTag } from '../tags';
import {
  CreatePost,
  PostDetail,
  EditPost,
  GetByCategory,
  GetByTag,
  PostTable
} from '../posts';
import AuthorizeRoute from '../api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from '../api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';

library.add(faEdit, faTrashAlt, faPlus);

const App = () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <AuthorizeRoute path="/categories" component={CategoryTable} />
    <AuthorizeRoute path="/create-category" component={CreateCategory} />
    <AuthorizeRoute path="/edit-category/:id" component={EditCategory} />
    <AuthorizeRoute path="/tags" component={TagTable} />
    <AuthorizeRoute path="/create-tag" component={CreateTag} />
    <AuthorizeRoute path="/edit-tag/:id" component={EditTag} />
    <AuthorizeRoute path="/posts" component={PostTable} />
    <AuthorizeRoute path="/create-post" component={CreatePost} />
    <AuthorizeRoute path="/edit-post/:slug" component={EditPost} />
    <Route path="/post/:slug" component={PostDetail} />
    <Route path="/archive/category/:categorySlug" component={GetByCategory} />
    <Route path="/archive/tag/:tagSlug" component={GetByTag} />
    <Route
      path={ApplicationPaths.ApiAuthorizationPrefix}
      component={ApiAuthorizationRoutes}
    />
  </Layout>
);

export default App;
