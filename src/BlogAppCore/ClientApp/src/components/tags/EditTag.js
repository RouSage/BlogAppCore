import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class EditTag extends Component {
  static displayName = EditTag.name;

  constructor(props) {
    super(props);
    this.state = {
      id: 0,
      name: '',
      slug: '',
      updateSlug: false,
    };
  }

  componentWillMount() {
    const { match } = this.props;
    const tagId = Number(match.params.id);

    fetch(`api/Tags/Get?id=${tagId}`, { method: 'GET' })
      .then(response => response.json())
      .then((data) => {
        this.setState({
          id: data.id,
          name: data.name,
          slug: data.slug,
        });
      });
  }

  handleNameChange(event) {
    this.setState({ name: event.target.value });
  }

  handleUpdateSlugChange(event) {
    this.setState({ updateSlug: event.target.checked });
  }

  handleSave(event) {
    event.preventDefault();

    const { id, name, updateSlug } = this.state;

    fetch('api/Tags/Update', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json;charset=UTF-8',
      },
      body: JSON.stringify({ id, name, updateSlug }),
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

  renderEditForm() {
    const {
      id, name, slug, updateSlug,
    } = this.state;

    return (
      <form onSubmit={event => this.handleSave(event)}>
        <input type="hidden" name="id" value={id} />
        <div className="form-group">
          <label htmlFor="tagName" className="col-form-label">
            Name:
            <input
              className="form-control"
              type="text"
              name="name"
              id="tagName"
              value={name}
              onChange={event => this.handleNameChange(event)}
            />
            <small>
              Current Slug:
              {slug}
            </small>
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="tagUpdateSlug" className="form-check-label">
            Update Slug:
            <input
              type="checkbox"
              className="form-check-inline"
              name="updateSlug"
              id="tagUpdateSlug"
              checked={updateSlug}
              onChange={event => this.handleUpdateSlugChange(event)}
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
    const contents = this.renderEditForm();

    return (
      <div>
        <h1>Edit the Category</h1>
        {contents}
      </div>
    );
  }
}

EditTag.propTypes = {
  history: PropTypes.object,
  match: PropTypes.shape({
    params: PropTypes.shape({
      id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
    }),
  }),
};
