import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';

export default class TagTable extends Component {
  static displayName = TagTable.name;

  constructor(props) {
    super(props);
    this.state = { tags: [], loading: true };

    fetch('api/Tags/GetAll')
      .then(response => response.json())
      .then((data) => {
        this.setState({ tags: data, loading: false });
      });
  }

  handleEdit(id) {
    const { history } = this.props;
    history.push(`/edit-tag/${id}`);
  }

  renderCategoriesTable(tags) {
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
              <Link to="/create-tag">Create New</Link>
            </th>
          </tr>
        </thead>
        <tbody>
          {tags.map(tag => (
            <tr key={tag.id}>
              <td>{tag.id}</td>
              <td>{tag.name}</td>
              <td>{tag.slug}</td>
              <td>{tag.created}</td>
              <td>{tag.totalPosts}</td>
              <td>
                <button type="button" onClick={() => this.handleEdit(tag.id)}>
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
    const { tags, loading } = this.state;
    const contents = loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      this.renderCategoriesTable(tags)
    );

    return (
      <div>
        <h1>Tags</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }
}

TagTable.propTypes = {
  history: PropTypes.object,
};
