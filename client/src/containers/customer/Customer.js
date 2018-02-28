import React, { Component } from 'react';
import Paper from 'material-ui/Paper'
import RaisedButton from "material-ui/RaisedButton"
import {
  Table,
  TableBody,
  TableHeader,
  TableHeaderColumn,
  TableRow,
  TableRowColumn,
} from 'material-ui/Table';
import {
  Toolbar,
  ToolbarGroup,
  ToolbarTitle} from 'material-ui/Toolbar';
import CircularProgress from 'material-ui/CircularProgress';
import FontIcon from 'material-ui/FontIcon';
import CustomerForm from './form'
import {
  fetchCustomers
} from './api'

const paperStyle = {
  height: '85%',
  width: "85%",
  margin: '7%',
  textAlign: 'center',
  display: 'inline-block',
}

const iconStyle = {
  margin: 12,
}

class Customer extends Component {
  state = {
    customers: [],
    formOpen: false
  }

  componentWillMount() {
    // load inital record if one is specified in the params
    //if (this.props.match.params.id) this.selectCustomer(this.props.match.params.id)
    // populate customer tables
    if (this.state.customers.length === 0) {
      fetchCustomers()
        .then(res => {
          this.setState({ customers: res.data })
        })
        .catch(err => {
          console.error(err)
        })
    }
  }

  handleNew = () => {
    this.setState({
      formOpen: true,
      id: 0
    })
  }

  handleCloseForm = () => {
    this.setState({formOpen: false})
  }

  handleEdit = (id) => {
    this.setState({
      formOpen: true,
      id
    })
  }

  handleDelete = (res) => {
    // close the form, unselect the current record and remove the deleted record from the table
    this.setState({
      formOpen: false,
      id: 0,
      customers: this.state.customers.filter(x => x.id !== res.data.id)
    })
  }

  handleUpdate = (res, insertState) => {
    if (insertState) {
      this.setState({
        formOpen: false,
        customers: [ ...this.state.customers, res.data ]
      })
    } else {
      this.setState({
        formOpen: false,
        customers: this.state.customers.map(x => x.id === res.data.id ? res.data : x)
      })
    }
  }

  renderRow(customer) {
    return (
      <TableRow key={customer.id}>
        <TableRowColumn>{customer.id}</TableRowColumn>
        <TableRowColumn>{customer.code}</TableRowColumn>
        <TableRowColumn>{customer.name}</TableRowColumn>
        <TableRowColumn>
          <RaisedButton
            label="Edit"
            icon={<FontIcon className="material-icons">mode_edit</FontIcon>}
            style={iconStyle}
            onClick={() => this.handleEdit(customer.id)} />
        </TableRowColumn>
      </TableRow>
    )
  }

  renderToolbar() {
    return (
      <Toolbar>
        <ToolbarGroup firstChild={true}>
          <ToolbarTitle text="Customers" />
        </ToolbarGroup>
        <ToolbarGroup>
          <RaisedButton label="New" primary={true} onClick={this.handleNew} />
        </ToolbarGroup>
      </Toolbar>
    )
  }

  renderTable() {
    return (
      <Table
        height='500px'
        fixedHeader={true}
        selectable={true}>
        <TableHeader
          displaySelectAll={false}
          adjustForCheckbox={false}>
          <TableRow>
            <TableHeaderColumn>Id</TableHeaderColumn>
            <TableHeaderColumn>Code</TableHeaderColumn>
            <TableHeaderColumn>Name</TableHeaderColumn>
            <TableHeaderColumn>Action</TableHeaderColumn>
          </TableRow>
        </TableHeader>
        <TableBody
          displayRowCheckbox={false}
          showRowHover={true}
          stripedRows={true}>
          { this.state.customers.map((customer) => this.renderRow(customer)) }
        </TableBody>
      </Table>
    )
  }

  renderProgress(message) {
    return (
      <Paper style={paperStyle} zDepth={5}>
        <Toolbar style={{"justifyContent": "center"}}>
          <ToolbarTitle text={message}/>
        </Toolbar>
        <CircularProgress size={60} thickness={7} />
      </Paper>
    )
  }

  renderForm() {
    if (this.state.formOpen) {
      return (
        <CustomerForm
          id={this.state.id}
          onDelete={this.handleDelete}
          onSubmit={this.handleUpdate}
          onCancel={this.handleCloseForm}
          />
      )
    } else return null
  }

  render() {
    let content = null;
    if (this.state.customers.length > 0) {
      content = (
        <div>
          { this.renderForm() }
          { this.renderToolbar() }
          { this.renderTable() }
        </div>
      )
    } else {
      content = this.renderProgress('Loading Customer records...')
    }
    return content
  }
}

export default Customer;