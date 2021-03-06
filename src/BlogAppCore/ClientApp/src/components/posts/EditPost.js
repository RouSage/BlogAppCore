/* eslint-disable jsx-a11y/label-has-associated-control */
import React, { Component } from 'react';
import PropTypes from 'prop-types';
import RichTextEditor from 'react-rte';

export default class EditPost extends Component {
  static displayName = EditPost.name;

  constructor(props) {
    super(props);

    this.state = {
      id: 0,
      title: '',
      slug: '',
      description: RichTextEditor.createEmptyValue(),
      content: RichTextEditor.createEmptyValue(),
      categoryId: 0,
      tags: [],
      published: false,
      updateSlug: false,
      categoriesSelect: [],
      tagsSelect: []
    };
  }

  componentWillMount() {
    const { match } = this.props;
    const postSlug = match.params.slug;

    Promise.all([
      fetch(`api/Posts/GetBySlug/${postSlug}`, {
        method: 'Get'
      }).then((response) => response.json()),
      fetch('api/Categories/GetAll', { method: 'GET' }).then((response) =>
        response.json()
      ),
      fetch('api/Tags/GetAll', { method: 'GET' }).then((response) =>
        response.json()
      )
    ]).then((response) => {
      const [post, categories, tags] = response;

      // Convert html strings back to EditorValue type so editor can understand it
      const postDescriptionValue = RichTextEditor.createValueFromString(
        post.description,
        'html'
      );
      const postContentValue = RichTextEditor.createValueFromString(
        post.content,
        'html'
      );

      this.setState({
        id: post.id,
        title: post.title,
        slug: postSlug,
        description: postDescriptionValue,
        content: postContentValue,
        categoryId: post.category.id,
        categoriesSelect: categories,
        tagsSelect: tags
      });
    });
  }

  handleSave(event) {
    event.preventDefault();

    const {
      id,
      title,
      description,
      content,
      categoryId,
      tags,
      published,
      updateSlug
    } = this.state;

    // Convert EditorValue objects to html strings
    const descriptionValue = description.toString('html');
    const contentValue = content.toString('html');

    fetch('api/Posts/Update', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        id,
        title,
        description: descriptionValue,
        content: contentValue,
        categoryId,
        tags,
        published,
        updateSlug
      })
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
      target: { name, type }
    } = event;
    const value = type === 'checkbox' ? target.checked : target.value;

    this.updateInputState(name, value);
  }

  updateInputState(name, value) {
    this.setState({ [name]: value });
  }

  handleCategoryChange(event) {
    const {
      target: { name, value }
    } = event;
    this.updateInputState(name, parseInt(value, 10));
  }

  handleTagsChange(event) {
    const selectedTags = Array.from(event.target.options).reduce(
      (tags, tag) => {
        if (tag.selected) {
          tags.push(parseInt(tag.value, 10));
        }

        return tags;
      },
      []
    );

    this.updateInputState(event.target.name, selectedTags);
  }

  handleDescriptionChange(value) {
    this.updateInputState('description', value);
  }

  handleContentChange(value) {
    this.updateInputState('content', value);
  }

  renderEditForm() {
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
      categoriesSelect,
      tagsSelect
    } = this.state;

    return (
      <form onSubmit={(event) => this.handleSave(event)} className="form">
        <input type="hidden" name="id" value={id} />
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
            <small className="post-meta">{`Current Slug: ${slug}`}</small>
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="description" className="form__label">
            Description
            <RichTextEditor
              value={description}
              onChange={(value) => this.handleDescriptionChange(value)}
            />
          </label>
        </div>
        <div className="form-group">
          <label htmlFor="content" className="form__label">
            Content
            <RichTextEditor
              value={content}
              onChange={(value) => this.handleContentChange(value)}
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
          <label
            htmlFor="published"
            className="form__label form__label_checkbox"
          >
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
          <label
            htmlFor="updateSlug"
            className="form__label form__label_checkbox"
          >
            Update Slug
            <input
              className="form-checkbox"
              type="checkbox"
              name="updateSlug"
              id="updateSlug"
              checked={updateSlug}
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
    const contents = this.renderEditForm();

    return (
      <div className="wrapper">
        <h1 className="main-title">Edit the Post</h1>
        <hr className="divider" />
        {contents}
      </div>
    );
  }
}

EditPost.propTypes = {
  history: PropTypes.object,
  match: PropTypes.shape({
    params: PropTypes.shape({
      slug: PropTypes.string.isRequired
    })
  })
};
