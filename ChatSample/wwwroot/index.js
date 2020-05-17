'use strict';

const e = React.createElement;

class ChatArea extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      name: ""
    };
    var connection = new signalR.HubConnectionBuilder()
      .withUrl('/chat')
      .build();

    var t = this;

    connection.on('inMessage', function (name, message) { t.addMessage(name, message);});

    connection.on('connected', function () { t.addMessage("Tech", "Connected"); });
    // Transport fallback functionality is now built into start.
    connection.start()
      .then(function () {
        console.log('connection started');
        var name = prompt('Enter your name:', '');

        t.setState({name: name});
      })
      .catch(error => {
        console.error(error.message);
      });

    this.state = { name: "", connection: connection, message: "", messages:[] };
    this.handleChange = this.handleChange.bind(this);
    this.sendMessage = this.sendMessage.bind(this);
    this.addMessage = this.addMessage.bind(this);
  }

  handleChange(event) {
    this.setState({message: event.target.value});
  }

  sendMessage() {
    this.state.connection.invoke('send', this.state.name, this.state.message, "1");

    this.addMessage(this.state.name, this.state.message);
    this.setState({message: ""});
  }

  addMessage(name, message){
    var messages = this.state.messages;
    messages.push({name: name, message: message});
    this.setState({messages: messages});
  }

  render() {
    return (
      <div>
        <input type="text" id="message" value={this.state.message} onChange={this.handleChange} />
        <button type="button" id="sendmessage" onClick={this.sendMessage}>Send</button>
        <ul id="discussion">
          {this.state.messages.map((m, i) => <li key={i}><strong>{m.name}</strong>:&nbsp;&nbsp;{m.message}</li>)}
        </ul>
      </div>
    );
  }
}

const domContainer = document.querySelector('#container');
ReactDOM.render(e(ChatArea), domContainer);