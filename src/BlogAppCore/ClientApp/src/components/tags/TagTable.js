import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import PropTypes from 'prop-types';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

export default class TagTable extends Component {
  static displayName = TagTable.name;

  constructor(props) {
    super(props);
    this.state = { tags: [], loading: true };

    fetch('api/Tags/GetAll')
      .then((response) => response.json())
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
      <table className="table">
        <thead className="thead-light">
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Name</th>
            <th scope="col">Slug</th>
            <th scope="col">Created</th>
            <th scope="col">Total Posts</th>
            <th scope="col">
              <Link to="/create-tag">
                <FontAwesomeIcon icon="plus" className="color-green" />
              </Link>
            </th>
          </tr>
        </thead>
        <tbody>
          {tags.map((tag) => (
            <tr key={tag.id}>
              <td>{tag.id}</td>
              <td>{tag.name}</td>
              <td>{tag.slug}</td>
              <td>{tag.created}</td>
              <td>{tag.totalPosts}</td>
              <td>
                <button
                  type="button"
                  onClick={() => this.handleEdit(tag.id)}
                  className="button-icon"
                >
                  <FontAwesomeIcon icon="edit" />
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
      <div className="wrapper">
        <h1 className="main-title">Tags</h1>
        <hr className="divider" />
        {contents}
      </div>
    );
  }
}

TagTable.propTypes = {
  history: PropTypes.object
};
