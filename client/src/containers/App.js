import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import 'styles/site.css'
import Home from 'containers/home/Home'
import CBS from 'containers/cbs/CBS'
import Customer from 'containers/customer/Customer'
import Project from 'containers/project/Project'
import Contract from 'containers/contract/Contract'
import User from 'containers/user/User'
import ReportingItem from 'containers/reportingItem/ReportingItem'
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'
import AppBar from 'material-ui/AppBar'
import MenuItem from 'material-ui/MenuItem'
import Divider from 'material-ui/Divider'
import Drawer from 'material-ui/Drawer'
import {Toolbar, ToolbarTitle} from 'material-ui/Toolbar'
import { BrowserRouter as Router, Route } from 'react-router-dom'

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
        "open": false,
        "show": null
    };
  }
  handleMenuToggle = () => this.setState({open: !this.state.open});

  renderAppBar() {
    return (
      <AppBar
      title="Reporting Commentary"
      iconClassNameRight="muidocs-icon-navigation-expand-more"
      onLeftIconButtonClick={this.handleMenuToggle}
      />
    )
  }

  renderMenu() {
    return (
      <Drawer
        docked={false}
        width={200}
        open={this.state.open}
        onRequestChange={(open) => this.setState({open})}>
        <Toolbar>
          <Link to="/" onClick={this.handleMenuToggle}>
            <ToolbarTitle text="Home"/>
          </Link>
        </Toolbar>
        <MenuItem>Add Comments</MenuItem>
        <Divider />
        <Link to="/contract" onClick={this.handleMenuToggle}><MenuItem>Contracts</MenuItem></Link>
        <Link to="/project" onClick={this.handleMenuToggle}><MenuItem>Projects</MenuItem></Link>
        <MenuItem>Reporting Periods</MenuItem>
        <Link to="/reportingitem" onClick={this.handleMenuToggle}><MenuItem>Reporting Items</MenuItem></Link>
        <Link to="/cbs" onClick={this.handleMenuToggle}><MenuItem>CBS</MenuItem></Link>
        <Link to="/customer" onClick={this.handleMenuToggle}><MenuItem>Customers</MenuItem></Link>
        <Divider />
        <Link to="/user" onClick={this.handleMenuToggle}><MenuItem>User</MenuItem></Link>
      </Drawer>
    )
  }

  render() {
    return (
      <Router>
        <MuiThemeProvider>
          <div>
            { this.renderMenu() }
            { this.renderAppBar() }
            <Route exact path="/" component={Home}/>
            <Route path="/cbs/:id?" component={CBS}/>
            <Route path="/customer/:id?" component={Customer}/>
            <Route path="/project/:id?" component={Project}/>
            <Route path="/contract/:id?" component={Contract}/>
            <Route path="/user/:id?" component={User}/>
            <Route path="/reportingitem/:id?" component={ReportingItem}/>
          </div>
        </MuiThemeProvider>
      </Router>
    );
  }
}

export default App;