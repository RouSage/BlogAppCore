import React, { Component } from 'react';
import PropTypes from 'prop-types';
import PostList from './PostList';

export default class GetByTag extends Component {
  static displayName = GetByTag.name;

  constructor(props) {
    super(props);

    this.state = { posts: [] };
  }

  componentWillMount() {
    const {
      match: {
        params: { tagSlug },
      },
    } = this.props;

    this.getPosts(tagSlug);
  }

  componentWillReceiveProps(newProps) {
    const {
      match: {
        params: { tagSlug },
      },
    } = this.props;

    if (tagSlug !== newProps.match.params.tagSlug) {
      this.getPosts(newProps.match.params.tagSlug);
    }
  }

  getPosts = (tagSlug) => {
    fetch(`api/Posts/GetByTag/${tagSlug}`, { method: 'GET' })
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

GetByTag.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      tagSlug: PropTypes.string.isRequired,
    }),
  }),
};
