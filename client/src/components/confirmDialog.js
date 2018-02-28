import React, { Component } from 'react';
import Dialog from 'material-ui/Dialog';

class ConfirmDialog extends Component {

  render() {
    return (
      <Dialog
        title={this.props.title}
        actions={this.props.actions}
        modal={true}
        open={this.props.open}>
        <p>{this.props.message}</p>
      </Dialog>
    )
  }
}

export default ConfirmDialog