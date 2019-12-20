import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class PostDetail extends Component {
  static displayName = PostDetail.name;

  constructor(props) {
    super(props);
    this.state = {
      title: '',
      // slug: '',
      description: '',
      content: '',
      created: '',
      category: {
        name: '',
        slug: '',
      },
      tags: [],
    };
  }

  componentWillMount() {
    const { match } = this.props;
    const postSlug = String(match.params.slug);

    fetch(`api/Posts/GetBySlug/${postSlug}`, { method: 'GET' })
      .then((respone) => respone.json())
      .then((data) => {
        this.setState({
          title: data.title,
          description: data.description,
          content: data.content,
          created: data.created,
          category: {
            name: data.category.name,
            slug: data.category.slug,
          },
          tags: data.tags,
        });
      });
  }

  render() {
    const {
      title, description, content, created, category, tags,
    } = this.state;

    return (
      <div className="post">
        <h1>{title}</h1>
        <h4>{category.name}</h4>
        <span>{category.slug}</span>
        <span>{created}</span>
        <p>{description}</p>
        <p>{content}</p>
        <ul>
          {tags.map((tag) => (
            <li key={tag.slug}>{tag.name}</li>
          ))}
        </ul>
      </div>
    );
  }
}

PostDetail.propTypes = {
  match: PropTypes.shape({
    params: PropTypes.shape({
      slug: PropTypes.string.isRequired,
    }),
  }),
};
