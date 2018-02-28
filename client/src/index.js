import React from 'react';
import ReactDOM from 'react-dom';
import App from 'containers/App';
import registerServiceWorker from './registerServiceWorker';

import injectTapEventPlugin from 'react-tap-event-plugin';   // add
injectTapEventPlugin();  // add

ReactDOM.render(<App />, document.getElementById('root'));
registerServiceWorker();
