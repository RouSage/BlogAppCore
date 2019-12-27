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
    const { match } = this.props;
    const { categorySlug } = match.params;

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

    return <div className="postsList">{posts.length > 0 && <PostList posts={posts} />}</div>;
  }
}

GetByCategory.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      categorySlug: PropTypes.string.isRequired,
    }),
  }),
  posts: PropTypes.arrayOf(PropTypes.object),
};
