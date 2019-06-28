import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class CreateCategory extends Component {
  static displayName = CreateCategory.name;

  constructor(props) {
    super(props);
    this.state = { name: '' };
  }

  handleChange(event) {
    this.setState({ name: event.target.value });
  }

  handleSave(event) {
    event.preventDefault();

    const { name } = this.state;

    fetch('api/Categories/Create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;charset=UTF-8',
      },
      body: JSON.stringify({ name }),
    }).then(() => {
      const { history } = this.props;
      history.push('/categories');
    });
  }

  handleCancel(event) {
    event.preventDefault();

    const { history } = this.props;
    history.push('/categories');
  }

  renderCreateForm() {
    const { name } = this.state;
    return (
      <form onSubmit={event => this.handleSave(event)}>
        <div className="form-group row">
          <label htmlFor="categoryName" className="col-form-label col-md-12">
            Name
            <input
              className="form-control"
              type="text"
              name="name"
              id="categoryName"
              value={name}
              onChange={event => this.handleChange(event)}
            />
          </label>
        </div>
        <div className="form-group">
          <button
            type="button"
            className="btn btn-danger"
            onClick={event => this.handleCancel(event)}
          >
            Cancel
          </button>
          <input type="submit" className="btn btn-dark" value="Save" />
        </div>
      </form>
    );
  }

  render() {
    const contents = this.renderCreateForm();

    return (
      <div>
        <h1>Create a new Category</h1>
        {contents}
      </div>
    );
  }
}

CreateCategory.propTypes = {
  history: PropTypes.object,
};
