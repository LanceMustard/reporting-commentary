import React, { Component } from 'react'
import Dialog from 'material-ui/Dialog'
import TextField from "material-ui/TextField"
import RaisedButton from "material-ui/RaisedButton"
import SelectField from 'material-ui/SelectField'
import MenuItem from 'material-ui/MenuItem'
import ConfirmMessage from 'components/confirmDialog'
import { fetchUsers } from "containers/user/api"
import { fetchCustomers } from "containers/customer/api"
import {
  fetchContract,
  createContract,
  updateContract,
  deleteContract
} from './api'


class ContractForm extends Component {
  state = {
    id: this.props.id,
    number: '',
    numberError: '',
    name: '',
    nameError: '',
    reportingCycle: '',
    reportingCycleError: '',
    contractManagerId: '',
    contractManagerIdError: '',
    customerId: '',
    customerError: '',
    original: {},
    users: [],
    customers: [],
    confirm: null
  }

  componentWillMount() {
    let users = []
    let customers =[]
    // retrieve parent data
    if (this.props.id === 0) {
      fetchUsers()
      .then(res => {
        users = res.data
        fetchCustomers()
        .then(res => {
          customers = res.data
        })
      })
      this.setState({original: {
        id: 0,
        number: '',
        name: '',
        reportingCycle: 'Weekly',
        contractManagerId: 0,
        customerId: 0,
        users,
        customers
      }})
    } else {
      fetchContract(this.props.id)
      .then(res => {
        let contract = res.data
        fetchUsers()
        .then(res => {
          users = res.data
          fetchCustomers()
          .then(res => {
            customers = res.data
            this.setState({
              number: contract.number,
              name: contract.name,
              reportingCycle: contract.reportingCycle,
              contractManagerId: contract.contractManagerId,
              customerId: contract.customerId,
              original: contract,
              users,
              customers
            })
          })
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

  handleSelectChange = (name, index, value) => {
    this.setState({
      [name]: value
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
      deleteContract(id)
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
      name: this.state.original.name,
      nameError: '',
      reportingCycle: this.state.reportingCycle,
      reportingCycleError: '',
      contractManagerId: this.state.contractManagerId,
      contractManagerIdError: '',
      customerId: this.state.customerId,
      customerIdError: ''
    })
  }

  executeNew() {
    this.setState({
      id: 0,
      number: '',
      numberError: '',
      name: '',
      nameError: '',
      reportingCycle: '',
      reportingCycleError: '',
      contractManagerId: 0,
      contractManagerIdError: '',
      customerId: 0,
      customerIdError: '',
      original: {
        id: 0,
        number: '',
        name: '',
      }
    })
  }

  executeSubmit = e => {
    e.preventDefault()
    const err = this.validate()
    if (!err) {
      let insertState = (this.state.id === 0)
      let api = insertState ? createContract : updateContract
      let contract = {
        id: this.state.id,
        number: this.state.number,
        name: this.state.name,
        reportingCycle: this.state.reportingCycle,
        customerId: this.state.customerId,
        customer: this.state.customers.find(x => x.id === this.state.customerId),
        contractManagerId: this.state.contractManagerId,
        contractManager: this.state.users.find(x => x.id === this.state.contractManagerId)
      }
      let p = new Promise(function (resolve, reject) {
        api(contract)
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
    if (this.state.name !== this.state.original.name) changed = true
    if (this.state.reportingCycle !== this.state.original.reportingCycle) changed = true
    if (this.state.customerId !== this.state.original.customerId) changed = true
    if (this.state.contractManagerId !== this.state.original.contractManagerId) changed = true
    return changed
  }

  validate = () => {
    let isError = false
    const errors = {
      numberError: "",
      nameError: "",
      reportingCycleError: ""
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
          name="name"
          hintText="Name"
          floatingLabelText="Name"
          value={this.state.name}
          onChange={this.handleChange}
          errorText={this.state.nameError}
          floatingLabelFixed
        />
        <br />
        <SelectField
          name="reportingCycle"
          hintText="Reporting Cycle"
          floatingLabelText="Reporting Cycle"
          value={this.state.reportingCycle}
          onChange={(e, i, v) => this.handleSelectChange('reportingCycle', i, v)}
        >
          <MenuItem value="Weekly" primaryText="Weekly" />
          <MenuItem value="Forghtnightly" primaryText="Forghtnightly" />
          <MenuItem value="Monthly" primaryText="Monthly" />
        </SelectField>
        <br />
        <SelectField
          name="contractManagerId"
          hintText="Contract Manager"
          floatingLabelText="Contract Manager"
          value={this.state.contractManagerId}
          onChange={(e, i, v) => this.handleSelectChange('contractManagerId', i, v)}
        >
          {this.state.users.map((user, index) => ( <MenuItem key={index} value={user.id} primaryText={user.name}/> ))}
        </SelectField>
        <br />
        <SelectField
          name="customerId"
          hintText="Customer"
          floatingLabelText="Customer"
          value={this.state.customerId}
          onChange={(e, i, v) => this.handleSelectChange('customerId', i, v)}
        >
          {this.state.customers.map((customer, index) => ( <MenuItem key={index} value={customer.id} primaryText={customer.name}/> ))}
        </SelectField>
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
        title={this.state.id === 0 ? 'Create Contract' : 'Edit Contract'}
        actions={actions}
        modal={true}
        open={true}>
        { this.renderForm() }
        { this.renderConfirmDialog() }
      </Dialog>
    )
  }
}

export default ContractForm;