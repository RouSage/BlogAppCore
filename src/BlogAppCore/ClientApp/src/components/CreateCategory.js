import React, { Component } from 'react';

export class CreateCategory extends Component {
  static displayName = CreateCategory.name;

  constructor(props) {
    super(props);
    this.state = { name: '', loading: false };

    this.handleSave = this.handleSave.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  handleSave(event) {
    event.preventDefault();
    const data = new FormData(event.target);

    fetch('api/Categories/Create', {
      method: 'POST',
      body: data
    }).then(_response => {
      this.props.history.push('/categories');
    });
  }

  handleCancel(e) {
    alert('Cancel clicked');
    e.preventDefault();
    // this.props.history.push('/categories');
  }

  static renderCreateForm() {
    return (
      <form onSubmit={this.handleSave}>
        <div className="form-group row">
          <label htmlFor="Name" className="col-form-label col-md-12">
            Name
          </label>
          <div className="col-md-4">
            <input className="form-control" type="text" name="name" />
          </div>
        </div>
        <div className="form-group">
          <button className="btn btn-danger" onClick={this.handleCancel}>
            Cancel
          </button>
          <input type="submit" className="btn btn-dark" value="Save" />
        </div>
      </form>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      CreateCategory.renderCreateForm()
    );

    return (
      <div>
        <h1>Create a new Category</h1>
        {contents}
      </div>
    );
  }
}
