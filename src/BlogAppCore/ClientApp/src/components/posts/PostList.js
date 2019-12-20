import React, { Component } from 'react';
import { Link } from 'react-router';

export default class PostList extends Component {
  static displayName = PostList.name;

  constructor(props) {
    super(props);

    this.state = { posts: [] };
  }

  componentWillMount() {
    fetch('api/Posts/GetList', { method: 'GET' })
      .then((response) => response.json())
      .then((data) => {
        this.setState({
          posts: data,
        });
      });
  }

  render() {
    const { posts } = this.state;

    return (
      <ul className="posts">
        {posts.map((post) => (
          <li key={post.id}>
            <Link to={`/post/${post.slug}`}>{post.title}</Link>
            <h4>{post.category.name}</h4>
            <span>{post.created}</span>
            <p>{post.description}</p>
          </li>
        ))}
      </ul>
    );
  }
}
