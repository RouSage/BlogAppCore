import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

export default class CategoryTable extends Component {
  static displayName = CategoryTable.name;

  constructor(props) {
    super(props);
    this.state = { categories: [], loading: true };

    fetch('api/Categories/GetAll')
      .then(response => response.json())
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
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Slug</th>
            <th>Created</th>
            <th>Total Posts</th>
            <th>
              <Link to="/create-category">Create New</Link>
            </th>
          </tr>
        </thead>
        <tbody>
          {categories.map(category => (
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
      <div>
        <h1>Categories</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }
}

CategoryTable.propTypes = {
  history: PropTypes.object,
};
