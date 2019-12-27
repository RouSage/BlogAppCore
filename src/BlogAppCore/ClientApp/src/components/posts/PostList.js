import React from 'react';
import { Link } from 'react-router';
import PropTypes from 'prop-types';

const PostList = ({ posts }) => (
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

PostList.propTypes = {
  posts: PropTypes.arrayOf(PropTypes.oneOfType([PropTypes.string, PropTypes.number])).isRequired,
};

export default PostList;
