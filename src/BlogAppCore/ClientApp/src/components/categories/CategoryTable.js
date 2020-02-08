import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

export default class CategoryTable extends Component {
  static displayName = CategoryTable.name;

  constructor(props) {
    super(props);
    this.state = { categories: [], loading: true };

    fetch('api/Categories/GetAll')
      .then((response) => response.json())
      .then((data) => {
        this.setState({ categories: data, loading: false });
      });
  }

  handleEdit(id) {
    const { history } = this.props;
    history.push(`/edit-category/${id}`);
  }

  renderCategoriesTable(categories) {
    return (
      <table className="table">
        <thead className="thead-light">
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Name</th>
            <th scope="col">Slug</th>
            <th scope="col">Created</th>
            <th scope="col">Total Posts</th>
            <th scope="col">
              <Link to="/create-category">Create New</Link>
            </th>
          </tr>
        </thead>
        <tbody>
          {categories.map((category) => (
            <tr key={category.id}>
              <td>{category.id}</td>
              <td>{category.name}</td>
              <td>{category.slug}</td>
              <td>{category.created}</td>
              <td>{category.totalPosts}</td>
              <td>
                <button type="button" onClick={() => this.handleEdit(category.id)}>
                  Edit
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    const { categories, loading } = this.state;
    const contents = loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderCategoriesTable(categories)
    );

    return (
      <div className="wrapper">
        <h1 className="main-title">Categories</h1>
        <hr className="divider" />
        {contents}
      </div>
    );
  }
}

CategoryTable.propTypes = {
  history: PropTypes.object,
};
