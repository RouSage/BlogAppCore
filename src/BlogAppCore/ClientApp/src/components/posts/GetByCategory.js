import React, { Component } from 'react';
import PropTypes from 'prop-types';
import PostList from './PostList';

export default class GetByCategory extends Component {
  static displayName = GetByCategory.name;

  constructor(props) {
    super(props);

    this.state = { posts: [] };
  }

  componentWillMount() {
    const {
      match: {
        params: { categorySlug },
      },
    } = this.props;

    this.getPosts(categorySlug);
  }

  componentWillReceiveProps(newProps) {
    const {
      match: {
        params: { categorySlug },
      },
    } = this.props;

    if (categorySlug !== newProps.match.params.categorySlug) {
      this.getPosts(newProps.match.params.categorySlug);
    }
  }

  getPosts = (categorySlug) => {
    fetch(`api/Posts/GetByCategory/${categorySlug}`, { method: 'GET' })
      .then((response) => response.json())
      .then((data) => {
        this.setState({
          posts: data,
        });
      });
  };

  render() {
    const { posts } = this.state;

    return (
      <div className="postsList">
        {posts.length > 0 && <PostList posts={posts} />}
      </div>
    );
  }
}

GetByCategory.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      categorySlug: PropTypes.string.isRequired,
    }),
  }),
};
