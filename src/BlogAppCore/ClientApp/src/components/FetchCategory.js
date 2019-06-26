import React, { Component } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class FetchCategory extends Component {
  static displayName = FetchCategory.name;

  constructor(props) {
    super(props);
    this.state = { categories: [], loading: true };

    fetch('api/Categories/GetAll')
      .then(response => response.json())
      .then(data => {
        this.setState({ categories: data, loading: false });
      });
  }

  static renderCategoriesTable(categories) {
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
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/category/create">
                  Create New
                </NavLink>
              </NavItem>
            </th>
          </tr>
        </thead>
        <tbody>
          {categories.map(category => (
            <tr key={category.id}>
              <td>{category.name}</td>
              <td>{category.slug}</td>
              <td>{category.created}</td>
              <td>{category.totalPosts}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      FetchCategory.renderCategoriesTable(this.state.categories)
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
