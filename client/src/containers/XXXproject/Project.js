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
import ProjectForm from './form'
import {
  fetchProjects
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

class Project extends Component {
  state = {
    projects: [],
    formOpen: false
  }

  componentWillMount() {
    // load inital record if one is specified in the params
    //if (this.props.match.params.id) this.selectProject(this.props.match.params.id)
    // populate project tables
    if (this.state.projects.length === 0) {
      fetchProjects()
        .then(res => {
          this.setState({ projects: res.data })
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
      projects: this.state.projects.filter(x => x.id !== res.data.id)
    })
  }

  handleUpdate = (res, insertState) => {
    if (insertState) {
      this.setState({
        formOpen: false,
        projects: [ ...this.state.projects, res.data ]
      })
    } else {
      this.setState({
        formOpen: false,
        projects: this.state.projects.map(x => x.id === res.data.id ? res.data : x)
      })
    }
  }

  renderRow(project) {
    return (
      <TableRow key={project.id}>
        <TableRowColumn>{project.id}</TableRowColumn>
        <TableRowColumn>{project.number}</TableRowColumn>
        <TableRowColumn>{project.name}</TableRowColumn>
        <TableRowColumn>
          <RaisedButton
            label="Edit"
            icon={<FontIcon className="material-icons">mode_edit</FontIcon>}
            style={iconStyle}
            onClick={() => this.handleEdit(project.id)} />
        </TableRowColumn>
      </TableRow>
    )
  }

  renderToolbar() {
    return (
      <Toolbar>
        <ToolbarGroup firstChild={true}>
          <ToolbarTitle text="Projects" />
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
            <TableHeaderColumn>Name</TableHeaderColumn>
            <TableHeaderColumn>Action</TableHeaderColumn>
          </TableRow>
        </TableHeader>
        <TableBody
          displayRowCheckbox={false}
          showRowHover={true}
          stripedRows={true}>
          { this.state.projects.map((project) => this.renderRow(project)) }
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
        <ProjectForm
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
    if (this.state.projects.length > 0) {
      content = (
        <div>
          { this.renderForm() }
          { this.renderToolbar() }
          { this.renderTable() }
        </div>
      )
    } else {
      content = this.renderProgress('Loading Project records...')
    }
    return content
  }
}

export default Project;