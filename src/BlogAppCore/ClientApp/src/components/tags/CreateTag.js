import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class CreateTag extends Component {
  static displayName = CreateTag.name;

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

    fetch('api/Tags/Create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ name }),
    }).then(() => {
      const { history } = this.props;
      history.push('/tags');
    });
  }

  handleCancel(event) {
    event.preventDefault();

    const { history } = this.props;
    history.push('/tags');
  }

  renderCreateForm() {
    const { name } = this.state;
    return (
      <form onSubmit={(event) => this.handleSave(event)} className="form">
        <div className="form-group">
          <label htmlFor="tagName" className="form__label">
            Name
            <input
              className="form-control"
              type="text"
              name="name"
              id="tagName"
              value={name}
              onChange={(event) => this.handleChange(event)}
            />
          </label>
        </div>
        <div className="form-group">
          <button
            className="button button-danger"
            type="button"
            onClick={(event) => this.handleCancel(event)}
          >
            Cancel
          </button>
          <input type="submit" className="button button-primary" value="Save" />
        </div>
      </form>
    );
  }

  render() {
    const contents = this.renderCreateForm();

    return (
      <div className="wrapper">
        <h1 className="main-title">Create a new Tag</h1>
        <hr className="divider" />
        {contents}
      </div>
    );
  }
}

CreateTag.propTypes = {
  history: PropTypes.object,
};
