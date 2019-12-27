import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export default class TagList extends Component {
  static displayName = TagList.name;

  constructor(props) {
    super(props);

    this.state = { tags: [] };
  }

  componentWillMount() {
    fetch('api/Tags/GetList', { method: 'GET' })
      .then((response) => response.json())
      .then((data) => {
        this.setState({ tags: data });
      });
  }

  render() {
    const { tags } = this.state;

    return (
      <div className="tags">
        {tags.map((tag) => (
          <span key={tag.slug}>
            <Link to={`/archive/tag/${tag.slug}`}>{tag.name}</Link>
            {' '}
          </span>
        ))}
      </div>
    );
  }
}
