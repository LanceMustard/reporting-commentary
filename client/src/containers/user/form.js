import React, { Component } from 'react'
import Dialog from 'material-ui/Dialog'
import TextField from "material-ui/TextField"
import RaisedButton from "material-ui/RaisedButton"
import ConfirmMessage from 'components/confirmDialog'
import {
  fetchUser,
  createUser,
  updateUser,
  deleteUser
} from './api'


class UserForm extends Component {
  state = {
    id: this.props.id,
    name: '',
    nameError: '',
    original: {},
    confirm: null
  }

  componentWillMount() {
    if (this.props.id === 0) {
      this.setState({original: {
        id: 0,
        name: '',
      }})
    } else {
      fetchUser(this.props.id)
      .then(res => {
        this.setState({
          name: res.data.name,
          original: res.data
        })
      })
      .catch(err => {
        console.error(err)
      })
    }
  }

  handleChange = (e, value) => {
    this.setState({
      [e.target.name]: value
    })
  }

  handleCancel = () => {
    if (!this.isChanged()) {
      this.props.onCancel()
    } else {
      // Prompt user first
      this.setState({
        confirm: {
          title: 'Changes exist',
          message: 'All changes will be lost. Please confirm you wish to continue?',
          actions: [
            <RaisedButton
              label="No"
              onClick={() => this.setState({confirm: null})}
            />,
            <RaisedButton
              label="Yes"
              primary={true}
              onClick={() => {
                this.setState({confirm: null})
                this.props.onCancel()
              }}
            />
          ]
        }
      })
    }
  }

  handleNew = () => {
    if (!this.isChanged()) {
      this.executeNew()
    } else {
      // Prompt user first
      this.setState({
        confirm: {
          title: 'Changes exist',
          message: 'All changes will be lost. Please confirm you wish to continue?',
          actions: [
            <RaisedButton
              label="No"
              onClick={() => this.setState({confirm: null})}
            />,
            <RaisedButton
              label="Yes"
              primary={true}
              onClick={() => {
                this.setState({confirm: null})
                this.executeNew()
              }}
            />
          ]
        }
      })
    }
  }

  handleDelete = () => {
    this.setState({
      confirm: {
        title: 'Confirmation required',
        message: 'Please confirm you wish to delete this record?',
        actions: [
          <RaisedButton
            label="No"
            onClick={() => this.setState({confirm: null})}
          />,
          <RaisedButton
            label="Yes"
            primary={true}
            onClick={() => {
              this.setState({confirm: null})
              this.executeDelete()
            }}
          />
        ]
      }
    })
  }

  handleRefresh = () => {
    if (!this.isChanged()) {
      this.executeRefresh()
    } else {
      // Prompt user first
      this.setState({
        confirm: {
          title: 'Changes exist',
          message: 'All changes will be lost. Please confirm you wish to continue?',
          actions: [
            <RaisedButton
              label="No"
              onClick={() => this.setState({confirm: null})}
            />,
            <RaisedButton
              label="Yes"
              primary={true}
              onClick={() => {
                this.setState({confirm: null})
                this.executeRefresh()
              }}
            />
          ]
        }
      })
    }
  }

  executeDelete = () => {
    let id = this.state.id
    let p = new Promise(function (resolve, reject) {
      deleteUser(id)
        .then(res => {
          resolve({
            data: res.data,
          })
        })
        .catch(err => {
          reject(err.message)
        })
    })
    p.then((res) => {
      // success
      this.props.onDelete(res)
    })
    p.catch(err => {
      // failure
      console.error('failure', err)
      return
    })
  }

  executeRefresh() {
    this.setState({
      id: this.state.original.id,
      name: this.state.original.name,
      nameError: '',
    })
  }

  executeNew() {
    this.setState({
      id: 0,
      name: '',
      nameError: '',
      original: {
        id: 0,
        name: '',
      }
    })
  }

  executeSubmit = e => {
    e.preventDefault()
    const err = this.validate()
    if (!err) {
      let insertState = (this.state.id === 0)
      let api = insertState ? createUser : updateUser
      let user = {
        id: this.state.id,
        name: this.state.name
      }
      let p = new Promise(function (resolve, reject) {
        api(user)
          .then(res => {
            resolve({
              data: res.data,
            })
          })
          .catch(err => {
            reject(err.message)
          })
      })
      p.then((res) => {
        this.props.onSubmit(res, insertState)
      })
      p.catch(err => {
        console.error('failure', err)
        return
      })
    }
  }

  isChanged = () => {
    let changed = false
    if (this.state.name !== this.state.original.name) changed = true
    return changed
  }

  validate = () => {
    let isError = false
    const errors = {
      nameError: "",
    }
    if (this.state.name.length < 1) {
      isError = true;
      errors.nameError = "Please enter a Name";
    }
    this.setState({
      ...this.state,
      ...errors
    });
    return isError
  }

  renderForm() {
    return (
      <form>
        <TextField
          name="name"
          hintText="Name"
          floatingLabelText="Name"
          value={this.state.name}
          onChange={this.handleChange}
          errorText={this.state.nameError}
          floatingLabelFixed
        />
      </form>
    )
  }

  renderConfirmDialog() {
    if (this.state.confirm) {
      return (
        <ConfirmMessage
          title = {this.state.confirm.title}
          message = {this.state.confirm.message}
          actions = {this.state.confirm.actions}
          open = {this.state.confirm ? true : false }
        />
      )
    } else return null
  }

  render() {
    const actions = [
      <RaisedButton
        label="New"
        onClick={this.handleNew}
      />,
      <RaisedButton
        label="Delete"
        onClick={this.handleDelete}
      />,
      <RaisedButton
        label="Refresh"
        onClick={this.handleRefresh}
      />,
      <RaisedButton
        label="Cancel"
        onClick={this.handleCancel}
      />,
      <RaisedButton
        label="Submit"
        primary={true}
        onClick={this.executeSubmit}
      />
    ]
    return (
      <Dialog
        title={this.state.id === 0 ? 'Create User' : 'Edit User'}
        actions={actions}
        modal={true}
        open={true}>
        { this.renderForm() }
        { this.renderConfirmDialog() }
      </Dialog>
    )
  }
}

export default UserForm;