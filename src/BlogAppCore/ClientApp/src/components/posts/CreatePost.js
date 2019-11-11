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
      fetch('api/Categories/GetAll', { method: 'GET' }).then(response => response.json()),
      fetch('api/Tags/GetAll', { method: 'GET' }).then(response => response.json()),
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
    }).then(() => {
      const { history } = this.props;
      history.push('/posts');
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
      <form onSubmit={event => this.handleSave(event)} className="form">
        <label htmlFor="title" className="form__label">
          Title
          <input
            type="text"
            name="title"
            id="title"
            value={title}
            onChange={event => this.handleInputChange(event)}
          />
        </label>
        <label htmlFor="description" className="form__label">
          Description
          <textarea
            name="description"
            id="description"
            value={description}
            onChange={event => this.handleInputChange(event)}
          />
        </label>
        <label htmlFor="content" className="form__label">
          Content
          <textarea
            name="content"
            id="content"
            value={content}
            onChange={event => this.handleInputChange(event)}
          />
        </label>
        <label htmlFor="categoryId" className="form__label">
          Category
          <select
            name="categoryId"
            id="categoryId"
            value={categoryId}
            onChange={event => this.handleInputChange(event)}
          >
            <option key="0" value="0">
              Select Category
            </option>
            {categoriesSelect.map(category => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
        </label>
        <label htmlFor="tags" className="form__label">
          Tags
          <select
            name="tags"
            id="tags"
            multiple
            value={tags}
            onChange={event => this.handleTagsChange(event)}
          >
            {tagsSelect.map(tag => (
              <option key={tag.id} value={tag.id}>
                {tag.name}
              </option>
            ))}
          </select>
        </label>
        <label htmlFor="published" className="form__label form__label_checkbox">
          Published
          <input
            type="checkbox"
            name="published"
            id="published"
            checked={published}
            onChange={event => this.handleInputChange(event)}
          />
        </label>
        <div className="form-group">
          <button type="button" className="button" onClick={event => this.handleCancel(event)}>
            Cancel
          </button>
          <input type="submit" className="button button_primary" value="Save" />
        </div>
      </form>
    );
  }

  render() {
    const contents = this.renderCreateForm();

    return (
      <div>
        <h1>Create a new Post</h1>
        {contents}
      </div>
    );
  }
}

CreatePost.propTypes = {
  history: PropTypes.object,
};