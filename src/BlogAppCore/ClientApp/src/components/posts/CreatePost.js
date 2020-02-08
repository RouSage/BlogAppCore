import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class CreatePost extends Component {
  static displayName = CreatePost.name;

  constructor(props) {
    super(props);
    this.state = {
      title: '',
      description: '',
      content: '',
      categoryId: 0,
      tags: [],
      published: false,
      categoriesSelect: [],
      tagsSelect: [],
    };
  }

  componentWillMount() {
    Promise.all([
      fetch('api/Categories/GetAll', { method: 'GET' }).then((response) => response.json()),
      fetch('api/Tags/GetAll', { method: 'GET' }).then((response) => response.json()),
    ]).then((response) => {
      const [categories, tags] = response;

      this.setState({ categoriesSelect: categories, tagsSelect: tags });
    });
  }

  handleSave(event) {
    event.preventDefault();

    const {
      title, description, content, categoryId, tags, published,
    } = this.state;

    fetch('api/Posts/Create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json;charset=UTF-8',
      },
      body: JSON.stringify({
        title,
        description,
        content,
        categoryId,
        tags,
        published,
      }),
    })
      .then((response) => response.json())
      .then((data) => {
        const { history } = this.props;
        history.push(`/post/${data.slug}`);
      });
  }

  handleCancel(event) {
    event.preventDefault();

    const { history } = this.props;
    history.push('/posts');
  }

  handleInputChange(event) {
    const {
      target,
      target: { name, type },
    } = event;
    const value = type === 'checkbox' ? target.checked : target.value;

    this.setState({
      [name]: value,
    });
  }

  handleCategoryChange(event) {
    this.setState({
      categoryId: parseInt(event.target.value, 10),
    });
  }

  handleTagsChange(event) {
    const selectedTags = Array.from(event.target.options).reduce((tags, tag) => {
      if (tag.selected) {
        tags.push(tag.value);
      }

      return tags;
    }, []);

    this.setState({ tags: selectedTags });
  }

  renderCreateForm() {
    const {
      title,
      description,
      content,
      categoryId,
      tags,
      published,
      categoriesSelect,
      tagsSelect,
    } = this.state;

    return (
      <form onSubmit={(event) => this.handleSave(event)} className="form">
        <div className="form-group">
          <label htmlFor="title" className="form__label">
            Title
            <input
              className="form-control"
              type="text"
              name="title"
              id="title"
              value={title}
              onChange={(event) => this.handleInputChange(event)}
            />
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="description" className="form__label">
            Description
            <textarea
              className="form-control"
              cols="50"
              rows="25"
              name="description"
              id="description"
              value={description}
              onChange={(event) => this.handleInputChange(event)}
            />
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="content" className="form__label">
            Content
            <textarea
              className="form-control"
              cols="50"
              rows="25"
              name="content"
              id="content"
              value={content}
              onChange={(event) => this.handleInputChange(event)}
            />
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="categoryId" className="form__label">
            Category
            <select
              className="form-control"
              name="categoryId"
              id="categoryId"
              value={categoryId}
              onChange={(event) => this.handleCategoryChange(event)}
            >
              <option key="0" value="0">
                Select Category
              </option>
              {categoriesSelect.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </select>
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="tags" className="form__label">
            Tags
            <select
              className="form-control"
              name="tags"
              id="tags"
              multiple
              value={tags}
              onChange={(event) => this.handleTagsChange(event)}
            >
              {tagsSelect.map((tag) => (
                <option key={tag.id} value={tag.id}>
                  {tag.name}
                </option>
              ))}
            </select>
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="published" className="form__label form__label_checkbox">
            Published
            <input
              className="form-checkbox"
              type="checkbox"
              name="published"
              id="published"
              checked={published}
              onChange={(event) => this.handleInputChange(event)}
            />
          </label>
        </div>
        <div className="form-group">
          <button
            type="button"
            className="button button-danger"
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
      <div className="post-form-wrapper">
        <h1 className="main-title">Create a new Post</h1>
        <hr className="divider" />
        {contents}
      </div>
    );
  }
}

CreatePost.propTypes = {
  history: PropTypes.object,
};
