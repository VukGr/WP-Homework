import Util from './util.js'
import {Channel} from './channel.js'

export class Network {
    constructor(name) {
        this.name = name
        this.channels = []
    }

    static fromJSON(json) {
        const net = new Network(json.name)
        net.id = json.id
        if(json.channels)
            net.channels = json.channels.map(ch => Channel.fromJSON(ch))
        return net
    }

    toJSON() {
        const json = {
            name: this.name,
            channels: this.channels
        }
        if(this.id)
            json.id = this.id

        return json
    }

    draw(parent) {
        this.el = Util.addChild(parent, 'div', 'network')

        // Element type, class name, contents
        const [headerDOM, channelsDOM, controlsDOM] = Util.addChildren(this.el, [
            ['h1', 'header', this.name],
            ['div', 'channels'],
            ['div', 'network-controls']
        ])

        // Create Form
        const createDOM = Util.addChild(controlsDOM, 'div', 'controls')
        Util.addChild(createDOM, 'b', '', 'Dodaj Kanal')
        const createInputs = Util.addInputs(createDOM, [
            ['Ime Kanala', 'text']
        ])
        Util.addChild(createDOM, 'button', '', 'Dodaj').onclick = () => this.addChannel(createInputs, channelsDOM)

        this.channels.forEach(ch => ch.draw(channelsDOM))
    }

    addChannel(inputs, channelsDOM) {
        Util.checkInputs(inputs)
        const name = inputs['Ime Kanala'].value

        Util.API('POST', 'https://localhost:5001/Channel/' + this.id, { name: name }).then(json => {
            const ch = Channel.fromJSON(json)
            this.channels.push(ch)
            ch.draw(channelsDOM)
        }).catch(alert)
    }
}
