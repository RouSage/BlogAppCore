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
        <h2 className="secondary-title">Tags</h2>
        <div className="tags-list">
          {tags.map((tag, index) => (
            <span key={tag.slug}>
              <Link to={`/archive/tag/${tag.slug}`}>
                {tag.name + (index >= 0 && index < tags.length - 1 ? ',' : '')}
              </Link>
            </span>
          ))}
        </div>
      </div>
    );
  }
}
