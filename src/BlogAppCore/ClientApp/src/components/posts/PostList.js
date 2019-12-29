import React from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import './Posts.scss';

const PostList = ({ posts }) => (
  <ul className="posts-list">
    {posts.map((post) => (
      <li key={post.id} className="posts-list-item">
        <div className="post">
          <div className="post-title">
            <span>
              <Link to={`/post/${post.slug}`}>{post.title}</Link>
            </span>
          </div>
          <div className="post-meta">
            <span>
              <Link to={`/archive/category/${post.category.slug}`}>
                <small>{post.category.name}</small>
              </Link>
            </span>
            &bull;
            <span>
              <small>{post.created}</small>
            </span>
          </div>
          <hr />
          <div className="post-content">
            <p>{post.description}</p>
          </div>
          {post.tags.length > 0
            && post.tags.map((tag) => (
              <span key={tag.slug}>
                <Link to={`/archive/tag/${tag.slug}`}>
                  <small>
                    {tag.name}
                    {', '}
                  </small>
                </Link>
              </span>
            ))}
        </div>
        <hr className="posts-list-item__divider" />
      </li>
    ))}
  </ul>
);

PostList.propTypes = {
  posts: PropTypes.arrayOf(PropTypes.object),
};

export default PostList;
