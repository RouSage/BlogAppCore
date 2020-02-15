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
      <div className="categories">
        <h2 className="secondary-title">Cateogires</h2>
        <ul className="categories-list">
          {categories.map((category) => (
            <li key={category.slug} className="categories-list-item">
              <Link to={`/archive/category/${category.slug}`}>{category.name}</Link>
              <span className="categories__count categories__count_primary">
                {category.totalPosts}
              </span>
            </li>
          ))}
        </ul>
      </div>
    );
  }
}
