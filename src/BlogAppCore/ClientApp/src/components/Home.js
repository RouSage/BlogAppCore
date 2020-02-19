import React, { Component } from 'react';
import PostList from './posts/PostList';

export default class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);

    this.state = { latestPosts: [] };
  }

  componentWillMount() {
    fetch('api/Posts/GetList', { method: 'GET' })
      .then((response) => response.json())
      .then((data) => {
        this.setState({ latestPosts: data });
      });
  }

  render() {
    const { latestPosts } = this.state;

    return (
      <div className="postsList">
        <h1 className="main-title">Latest Posts</h1>
        <hr className="divider" />
        {latestPosts.length > 0 ? (
          <PostList posts={latestPosts} />
        ) : (
          <h1 className="main-title">There&apos;re no posts yet!</h1>
        )}
      </div>
    );
  }
}
