import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export default class CategoryList extends Component {
  static displayName = CategoryList.name;

  constructor(props) {
    super(props);

    this.state = { categories: [] };
  }

  componentWillMount() {
    fetch('api/Categories/GetList', { method: 'GET' })
      .then((response) => response.json())
      .then((data) => {
        this.setState({ categories: data });
      });
  }

  render() {
    const { categories } = this.state;

    return (
      <ul className="categories">
        {categories.map((category) => (
          <li key={category.slug}>
            <Link to={`/archive/category/${category.slug}`}>
              {category.name}
              {' '}
              <span className="categories__count">{category.totalPosts}</span>
            </Link>
          </li>
        ))}
      </ul>
    );
  }
}
