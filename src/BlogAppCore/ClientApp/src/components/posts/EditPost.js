import React, { Component } from 'react';
import PropTypes from 'prop-types';

export default class EditCategory extends Component {
  static displayName = EditCategory.name;

  constructor(props) {
    super(props);
    this.state = {
      id: 0,
      title: '',
      slug: '',
      description: '',
      content: '',
      categoryId: 0,
      tags: [],
      published: false,
      updateSlug: false,
      categoriesSelect: [],
      tagsSelect: [],
    };
  }

  componentWillMount() {
    const { match } = this.props;
    const postSlug = String(match.params.slug);

    Promise.all([
      fetch(`api/Posts/GetBySlug/${postSlug}`, { method: 'Get' }).then((response) => response.json()),
      fetch('api/Categories/GetAll', { method: 'GET' }).then((response) => response.json()),
      fetch('api/Tags/GetAll', { method: 'GET' }).then((response) => response.json()),
    ]).then((response) => {
      const [post, categories, tags] = response;

      this.setState({
        id: post.id,
        title: post.title,
        slug: postSlug,
        description: post.description,
        content: post.content,
        categoryId: post.categoryId,
        categoriesSelect: categories,
        tagsSelect: tags,
      });
    });
  }

  handleSave(event) {
    event.preventDefault();

    const {
      id,
      title,
      slug,
      description,
      content,
      categoryId,
      tags,
      published,
      updateSlug,
    } = this.state;

    fetch('api/Posts/Update', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        id,
        title,
        description,
        content,
        categoryId,
        tags,
        published,
        updateSlug,
      }),
    }).then(() => {
      const { history } = this.props;
      history.push(`/post/${slug}`);
    });
  }

  handleCancel(event) {
    event.preventDefault();

    const { history } = this.props;
    const { slug } = this.state;

    history.push(`/post/${slug}`);
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

  renderEditForm() {
    const {
      id,
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
        <input type="hidden" name="id" value={id} />
        <label htmlFor="title" className="form__label">
          Title
          <input
            type="text"
            name="title"
            id="title"
            value={title}
            onChange={(event) => this.handleInputChange(event)}
          />
        </label>
        <label htmlFor="description" className="form__label">
          Description
          <textarea
            name="description"
            id="description"
            value={description}
            onChange={(event) => this.handleInputChange(event)}
          />
        </label>
        <label htmlFor="content" className="form__label">
          Content
          <textarea
            name="content"
            id="content"
            value={content}
            onChange={(event) => this.handleInputChange(event)}
          />
        </label>
        <label htmlFor="categoryId" className="form__label">
          Category
          <select
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
        <label htmlFor="tags" className="form__label">
          Tags
          <select
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
        <label htmlFor="published" className="form__label form__label_checkbox">
          Published
          <input
            type="checkbox"
            name="published"
            id="published"
            checked={published}
            onChange={(event) => this.handleInputChange(event)}
          />
        </label>
        <div className="form-group">
          <button type="button" className="button" onClick={(event) => this.handleCancel(event)}>
            Cancel
          </button>
          <input type="submit" className="button button_primary" value="Save" />
        </div>
      </form>
    );
  }

  render() {
    const contents = this.renderEditForm();

    return (
      <div>
        <h1>Edit the Post</h1>
        {contents}
      </div>
    );
  }
}

EditCategory.propTypes = {
  history: PropTypes.object,
  match: PropTypes.shape({
    params: PropTypes.shape({
      slug: PropTypes.string.isRequired,
    }),
  }),
};
