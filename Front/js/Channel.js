import Util from './util.js'
import {Slot} from './slot.js'

export class Channel {
    constructor(name) {
        this.name = name

        this.slots = []
        for(var hr = 0; hr < 24; hr++)
            this.slots.push(new Slot(hr, 'Prazan', 'white'))
    }

    static fromJSON(json) {
        const ch = new Channel(json.name)
        ch.id = json.id
        if(json.slots)
            ch.slots = json.slots.map(slot => Slot.fromJSON(slot)).sort((a,b) => (a.hour > b.hour)? 1 : -1)
        return ch
    }

    toJSON() {
        const json = {
            name: this.name,
            slots: this.slots
        }
        if(this.id)
            json.id = this.id
        return json 
    }

    draw(parent) {
        this.el = Util.addChild(parent, 'div', 'channel')

        // Element type, class name
        const [header, body] = Util.addChildren(this.el, [
            ['div', 'channel-header'],
            ['div', 'channel-body']
        ])

        const [renameButton, , deleteButton] = Util.addChildren(header, [
            ['button', '', 'Preimenuj'],
            ['h2', 'header', 'Kanal: ' + this.name],
            ['button', '', 'X'],
        ])
        renameButton.onclick = () => this.rename(this.el)
        deleteButton.onclick = () => this.delete(this.el)

        const [controls, slotsDOM] = Util.addChildren(body, [
            ['div', 'controls'],
            ['div', 'slots']
        ])

        // Label text, input type
        const inputs = Util.addInputs(controls, [
            ['Ime Emisije', 'text'],
            ['Pocetni Slot', 'number'],
            ['Broj Slotova', 'number']
        ])
        Util.addChild(controls, 'button', '', 'Dodaj').onclick = (() => this.addShow(inputs))

        this.slots.forEach(slot => slot.draw(slotsDOM))
    }

    rename(dom) {
        const newName = prompt('Unesite novo ime:')
        this.name = newName
        
        Util.API('PUT', 'https://localhost:5001/Channel', this.toJSON()).then(() => {
            dom.querySelector('.header').innerText = this.name
        }).catch(alert)
    }

    delete(dom) {
        Util.API('DELETE', 'https://localhost:5001/Channel/' + this.id).then(() => {
            dom.remove()
        }).catch(alert)
    }

    addShow(inputs) {
        Util.checkInputs(inputs)

        const startID = Number(inputs['Pocetni Slot'].value)
        const count = Number(inputs['Broj Slotova'].value)
        const name = inputs['Ime Emisije'].value

        if(startID >= 24 || startID < 0)
            return alert('Pocetni Slot nije validan')
        else if(startID + count > 24)
            return alert('Broj Slotova nije validan')
            
        const showColor = Util.randomColor()
        for(var i = 0; i < count; i++)
            this.slots[startID + i].setShow(name, showColor)
    }


}
