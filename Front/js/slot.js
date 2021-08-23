import Util from './util.js'
import {Channel} from './channel.js'

export class Slot {
    constructor(hour, content, color) {
        this.hour = hour
        this.content = content
        this.color = color
    }

    static fromJSON(json) {
        const slot = new Slot(json.hour, json.content, json.color)
        slot.id = json.id
        return slot
    }

    toJSON() {
        let json = {
            hour: this.hour,
            content: this.content,
            color: this.color
        }

        if(this.id)
            json.id = this.id

        return json
    }

    draw(parent) {
        this.el = Util.addChild(parent, 'div', 'slot')
        this.el.style.backgroundColor = this.color

        // Element type, class name, contents
        const [contentDOM,,button] = Util.addChildren(this.el, [
            ['b', 'slot-content', this.content],
            ['span', 'slot-time', `${this.hour}:00`],
            ['button', 'slot-delete', 'X']
        ])
        button.onclick = (() => this.setShow('Prazan', 'white', contentDOM))
    }

    setShow(newContent, newColor, contentDOM) {
        this.content = newContent
        this.color = newColor
        Util.API('PUT', 'https://localhost:5001/Slot', this.toJSON()).then(() => {
            if(!contentDOM)
                contentDOM = this.el.querySelector('.slot-content')
            contentDOM.innerText = newContent
            this.el.style.backgroundColor = newColor
        }).catch(alert)
    }
}
