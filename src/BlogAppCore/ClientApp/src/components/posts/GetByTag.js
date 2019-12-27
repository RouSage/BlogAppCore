import React, { Component } from 'react';
import PropTypes from 'prop-types';
import PostList from './PostList';

export default class GetByTag extends Component {
  static displayName = GetByTag.name;

  constructor(props) {
    super(props);
    this.setState({
      posts: [],
    });
  }

  componentWillMount() {
    const { match } = this.props;
    const tagSlug = String(match.params.tagSlug);

    fetch(`api/Posts/GetByTag/${tagSlug}`, { method: 'GET' })
      .then((response) => response.json())
      .then((data) => {
        this.setState({
          posts: data,
        });
      });
  }

  render() {
    const { posts } = this.state;

    return <PostList posts={posts} />;
  }
}

GetByTag.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      tagSlug: PropTypes.string.isRequired,
    }),
  }),
};
