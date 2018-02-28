import React, { Component } from 'react'
import Dialog from 'material-ui/Dialog'
import TextField from "material-ui/TextField"
import RaisedButton from "material-ui/RaisedButton"
import ConfirmMessage from 'components/confirmDialog'
import {
  fetchCustomer,
  createCustomer,
  updateCustomer,
  deleteCustomer
} from './api'


class CustomerForm extends Component {
  state = {
    id: this.props.id,
    code: '',
    codeError: '',
    name: '',
    nameError: '',
    original: {},
    confirm: null
  }

  componentWillMount() {
    if (this.props.id === 0) {
      this.setState({original: {
        id: 0,
        code: '',
        name: '',
      }})
    } else {
      fetchCustomer(this.props.id)
      .then(res => {
        this.setState({
          code: res.data.code,
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
      deleteCustomer(id)
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
      code: this.state.original.code,
      codeError: '',
      name: this.state.original.name,
      nameError: '',
    })
  }

  executeNew() {
    this.setState({
      id: 0,
      code: '',
      codeError: '',
      name: '',
      nameError: '',
      original: {
        id: 0,
        code: '',
        name: '',
      }
    })
  }

  executeSubmit = e => {
    e.preventDefault()
    const err = this.validate()
    if (!err) {
      let insertState = (this.state.id === 0)
      let api = insertState ? createCustomer : updateCustomer
      let customer = {
        id: this.state.id,
        code: this.state.code,
        name: this.state.name
      }
      let p = new Promise(function (resolve, reject) {
        api(customer)
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
    if (this.state.code !== this.state.original.code)  changed = true
    if (this.state.name !== this.state.original.name) changed = true
    return changed
  }

  validate = () => {
    let isError = false
    const errors = {
      codeError: "",
      nameError: "",
    }
    if (this.state.code.length < 1) {
      isError = true;
      errors.codeError = "Please enter a Code";
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
          name="code"
          hintText="Code"
          floatingLabelText="Code"
          value={this.state.code}
          onChange={this.handleChange}
          errorText={this.state.codeError}
          floatingLabelFixed
        />
        <br />
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
        title={this.state.id === 0 ? 'Create Customer' : 'Edit Customer'}
        actions={actions}
        modal={true}
        open={true}>
        { this.renderForm() }
        { this.renderConfirmDialog() }
      </Dialog>
    )
  }
}

export default CustomerForm;