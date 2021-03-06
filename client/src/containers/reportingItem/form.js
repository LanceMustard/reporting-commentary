import React, { Component } from 'react'
import Dialog from 'material-ui/Dialog'
import TextField from "material-ui/TextField"
import RaisedButton from "material-ui/RaisedButton"
import ConfirmMessage from 'components/confirmDialog'
import {
  fetchReportingItem,
  createReportingItem,
  updateReportingItem,
  deleteReportingItem
} from './api'


class ReportingItemForm extends Component {
  state = {
    id: this.props.id,
    number: '',
    numberError: '',
    description: '',
    descriptionError: '',
    original: {},
    confirm: null
  }

  componentWillMount() {
    if (this.props.id === 0) {
      this.setState({original: {
        id: 0,
        number: '',
        description: '',
      }})
    } else {
      fetchReportingItem(this.props.id)
      .then(res => {
        this.setState({
          number: res.data.number,
          description: res.data.description,
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
      deleteReportingItem(id)
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
      number: this.state.original.number,
      numberError: '',
      description: this.state.original.description,
      descriptionError: '',
    })
  }

  executeNew() {
    this.setState({
      id: 0,
      number: '',
      numberError: '',
      description: '',
      descriptionError: '',
      original: {
        id: 0,
        number: '',
        description: '',
      }
    })
  }

  executeSubmit = e => {
    e.preventDefault()
    const err = this.validate()
    if (!err) {
      let insertState = (this.state.id === 0)
      let api = insertState ? createReportingItem : updateReportingItem
      let reportingItem = {
        id: this.state.id,
        contractId: this.props.contractId,
        number: this.state.number,
        description: this.state.description
      }
      let p = new Promise(function (resolve, reject) {
        api(reportingItem)
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
    if (this.state.number !== this.state.original.number)  changed = true
    if (this.state.description !== this.state.original.description) changed = true
    return changed
  }

  validate = () => {
    let isError = false
    const errors = {
      numberError: "",
      descriptionError: "",
    }
    if (this.state.number.length < 1) {
      isError = true;
      errors.numberError = "Please enter a Number";
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
          name="number"
          hintText="Number"
          floatingLabelText="Number"
          value={this.state.number}
          onChange={this.handleChange}
          errorText={this.state.numberError}
          floatingLabelFixed
        />
        <br />
        <TextField
          name="description"
          hintText="Description"
          floatingLabelText="Description"
          value={this.state.description}
          onChange={this.handleChange}
          errorText={this.state.descriptionError}
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
        title={this.state.id === 0 ? `Create Reporting Item for ${this.props.contractName}` : `Edit Reporting Item for ${this.props.contractName}`}
        actions={actions}
        modal={true}
        open={true}>
        { this.renderForm() }
        { this.renderConfirmDialog() }
      </Dialog>
    )
  }
}

export default ReportingItemForm;