import React, { Component } from 'react';
import PropTypes from 'prop-types';
import './Categories.scss';

export default class EditCategory extends Component {
  static displayName = EditCategory.name;

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
    const categoryId = Number(match.params.id);

    fetch(`api/Categories/Get?id=${categoryId}`, { method: 'GET' })
      .then((response) => response.json())
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

    fetch('api/Categories/Update', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ id, name, updateSlug }),
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

  renderEditForm() {
    const {
      id, name, slug, updateSlug,
    } = this.state;

    return (
      <form onSubmit={(event) => this.handleSave(event)} className="form">
        <input type="hidden" name="id" value={id} />
        <div className="form-group">
          <label htmlFor="categoryName" className="form__label">
            Name
            <input
              className="form-control"
              type="text"
              name="name"
              id="categoryName"
              value={name}
              onChange={(event) => this.handleNameChange(event)}
            />
            <small className="category-meta">{`Current Slug: ${slug}`}</small>
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="categoryUpdateSlug" className="form__label form__label_checkbox">
            Update Slug
            <input
              className="form-checkbox"
              type="checkbox"
              name="updateSlug"
              id="categoryUpdateSlug"
              checked={updateSlug}
              onChange={(event) => this.handleUpdateSlugChange(event)}
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
    const contents = this.renderEditForm();

    return (
      <div className="wrapper">
        <h1 className="main-title">Edit the Category</h1>
        <hr className="divider" />
        {contents}
      </div>
    );
  }
}

EditCategory.propTypes = {
  history: PropTypes.object,
  match: PropTypes.shape({
    params: PropTypes.shape({
      id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
    }),
  }),
};
