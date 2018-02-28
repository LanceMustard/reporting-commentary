import React, { Component } from 'react';
import Paper from 'material-ui/Paper'
import RaisedButton from "material-ui/RaisedButton"
import SelectField from "material-ui/SelectField"
import MenuItem from 'material-ui/MenuItem'
import IconMenu from 'material-ui/IconMenu'
import IconButton from 'material-ui/IconButton'
import NavigationExpandMoreIcon from 'material-ui/svg-icons/navigation/expand-more'
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
  ToolbarTitle,
  ToolbarSeparator} from 'material-ui/Toolbar'
import CircularProgress from 'material-ui/CircularProgress'
import FontIcon from 'material-ui/FontIcon'
import ReportingItemForm from './form'
import { fetchContracts } from 'containers/contract/api'
import {
  fetchReportingItems
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

class ReportingItem extends Component {
  state = {
    reportingItems: [],
    contracts: [],
    contractId: 0,
    contractName: '',
    showLoading: false,
    formOpen: false
  }

  componentWillMount() {
    fetchContracts()
    .then(res => {
      this.setState({ contracts: res.data })
    })
  }

  executeFetchReportingItems(id) {
    this.setState({showLoading: true})
    fetchReportingItems(id)
      .then(res => {
        console.log('res.data', res.data)
        this.setState({
          reportingItems: res.data,
          showLoading: false
        })
      })
      .catch(err => {
        console.error(err)
        this.setState({showLoading: false})
      })
  }

  handleSelectChange = (e, name, index, value) => {
    console.log(e.target.innerText)
    this.setState({
      [name]: value,
      contractName: e.target.innerText
    })
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
      reportingItems: this.state.reportingItems.filter(x => x.id !== res.data.id)
    })
  }

  handleUpdate = (res, insertState) => {
    if (insertState) {
      this.setState({
        formOpen: false,
        reportingItems: [ ...this.state.reportingItems, res.data ]
      })
    } else {
      this.setState({
        formOpen: false,
        reportingItems: this.state.reportingItems.map(x => x.id === res.data.id ? res.data : x)
      })
    }
  }

  renderRow(reportingItem) {
    return (
      <TableRow key={reportingItem.id}>
        <TableRowColumn>{reportingItem.id}</TableRowColumn>
        <TableRowColumn>{reportingItem.number}</TableRowColumn>
        <TableRowColumn>{reportingItem.description}</TableRowColumn>
        <TableRowColumn>
          <RaisedButton
            label="Edit"
            icon={<FontIcon className="material-icons">mode_edit</FontIcon>}
            style={iconStyle}
            onClick={() => this.handleEdit(reportingItem.id)} />
        </TableRowColumn>
      </TableRow>
    )
  }

  renderToolbar() {
    return (
      <Toolbar>
        <ToolbarGroup firstChild={true}>
          <ToolbarTitle text={this.state.contractName === '' ? 'Select Contract' : this.state.contractName } />
          <IconMenu
            value={this.state.contractId}
            onChange={(e, value) => {
              this.setState({
                contractId: value,
                contractName: e.target.innerText
              })
              this.executeFetchReportingItems(value)
            }}
            iconButtonElement={
              <IconButton touch={true}>
                <NavigationExpandMoreIcon />
              </IconButton>
            }
          >
            {this.state.contracts.map((contract, index) => ( <MenuItem key={index} value={contract.id} primaryText={contract.name}/> ))}
          </IconMenu>
          <ToolbarTitle text="Reporting Items" />
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
            <TableHeaderColumn>Number</TableHeaderColumn>
            <TableHeaderColumn>Description</TableHeaderColumn>
            <TableHeaderColumn>Action</TableHeaderColumn>
          </TableRow>
        </TableHeader>
        <TableBody
          displayRowCheckbox={false}
          showRowHover={true}
          stripedRows={true}>
          { this.state.reportingItems.map((reportingItem) => this.renderRow(reportingItem)) }
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
        <ReportingItemForm
          id={this.state.id}
          contractId={this.state.contractId}
          contractName={this.state.contractName}
          onDelete={this.handleDelete}
          onSubmit={this.handleUpdate}
          onCancel={this.handleCloseForm}
          />
      )
    } else return null
  }

  render() {
    return (
      <div>
        { this.renderForm() }
        { this.renderToolbar() }
        { this.state.showLoading ? this.renderProgress('Loading Reporting Item records...') : null }
        { this.state.reportingItems.length > 0 ? this.renderTable() : null }
      </div>
    )
  }
}

export default ReportingItem;