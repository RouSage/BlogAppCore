import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { Link } from 'react-router-dom';
import './Posts.scss';

export default class PostDetail extends Component {
  static displayName = PostDetail.name;

  constructor(props) {
    super(props);
    this.state = {
      title: '',
      description: '',
      content: '',
      created: '',
      category: {
        name: '',
        slug: ''
      },
      tags: []
    };
  }

  componentWillMount() {
    const {
      match: {
        params: { slug }
      }
    } = this.props;

    fetch(`api/Posts/GetBySlug/${slug}`, { method: 'GET' })
      .then((respone) => respone.json())
      .then((data) => {
        this.setState({
          title: data.title,
          description: data.description,
          content: data.content,
          created: data.created,
          category: {
            name: data.category.name,
            slug: data.category.slug
          },
          tags: data.tags
        });
      });
  }

  render() {
    const { title, description, content, created, category, tags } = this.state;

    return (
      <div className="post">
        <div className="post-title">{title}</div>
        <div className="post-meta">
          <span>
            <Link to={`/archive/category/${category.slug}`}>
              <small>{category.name}</small>
            </Link>
          </span>
          &bull;
          <span>
            <small>{created}</small>
          </span>
        </div>
        <hr className="divider" />
        <div className="post-content">
          <div dangerouslySetInnerHTML={{ __html: description }} />
          <div dangerouslySetInnerHTML={{ __html: content }} />
        </div>
        {tags.length > 0 &&
          tags.map((tag) => (
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
    );
  }
}

PostDetail.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      slug: PropTypes.string.isRequired
    })
  })
};
