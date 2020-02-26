import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

export default class PostTable extends Component {
  static displayName = PostTable.name;

  constructor(props) {
    super(props);

    this.state = { posts: [] };

    fetch('api/Posts/GetList')
      .then((response) => response.json())
      .then((data) => {
        this.setState({ posts: data });
      });
  }

  handleEdit(slug) {
    const { history } = this.props;
    history.push(`/edit-post/${slug}`);
  }

  renderPostsTable(posts) {
    return (
      <table className="table">
        <thead className="thead-light">
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Title</th>
            <th scope="col">Slug</th>
            <th scope="col">Created</th>
            <th scope="col">Category</th>
            <th scope="col">
              <Link to="/create-post">Create New</Link>
            </th>
          </tr>
        </thead>
        <tbody>
          {posts.map((post) => (
            <tr key={post.id}>
              <td>{post.id}</td>
              <td>{post.title}</td>
              <td>{post.slug}</td>
              <td>{post.created}</td>
              <td>{post.category.name}</td>
              <td>
                <button
                  type="button"
                  onClick={() => this.handleEdit(post.slug)}
                >
                  Edit
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    const { posts } = this.state;

    return (
      <div className="wrapper">
        <h1 className="main-title">Posts</h1>
        <hr className="divider" />
        {this.renderPostsTable(posts)}
      </div>
    );
  }
}

PostTable.propTypes = {
  history: PropTypes.object,
};
