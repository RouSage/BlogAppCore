import React, { Component } from 'react';
import PropTypes from 'prop-types';
import PostList from './PostList';

export default class GetByCategory extends Component {
  static displayName = GetByCategory.name;

  constructor(props) {
    super(props);
    this.setState({
      posts: [],
    });
  }

  componentWillMount() {
    const { match } = this.props;
    const categorySlug = String(match.params.categorySlug);

    fetch(`api/Posts/GetByCategory/${categorySlug}`, { method: 'GET' })
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

GetByCategory.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      categorySlug: PropTypes.string.isRequired,
    }),
  }),
};
