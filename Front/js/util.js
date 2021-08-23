export default {
    addChild(parent, element, className, contents) {
        const el = document.createElement(element);
        parent.appendChild(el);
        if(className !== undefined) 
            el.className = className 
        if(contents !== undefined)
            el.innerText = contents
        return el;
    },
    addChildren(parent, elements) {
        return elements.map(el => this.addChild(parent, ...el))
    },
    addInputs(parent, inputs) { 
        return inputs.reduce((doms, [name, type]) => {
            const label = this.addChild(parent, 'label', '', name)
            const input = this.addChild(parent, 'input')
            input.type = type

            doms[name] = input
            return doms
        }, {})
    },
    addSelect(parent, options) {
        const select = this.addChild(parent, 'select')
        options.forEach(([text, val]) => {
            this.addChild(select, 'option', '', text).value = val
        })
        return select
    },
    checkInputs(inputs) {
        const emptyFields = Object.entries(inputs).reduce((acc, [key, input]) => {
            let isEmpty = input.value === undefined || input.value === ''

            input.style.backgroundColor = isEmpty? 'pink' : 'white'
            return isEmpty? (acc == ''? acc : acc + ', ') + key : acc
        }, '')

        if(emptyFields !== '')
            return alert('Ostavili ste bar jedno prazno polje: ' + emptyFields)
    },
    randomColor() {
        return `hsl(${(Math.random() * 360)|0}deg,75%, 75%)`
    },
    API(method, url, json) {
        const isGET = method === 'GET'

        return fetch(url, isGET? {} : {
            method: method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(json)
        }).then(async (res) => {
            if(res.ok)
                return res.json().catch()
            else {
                let err = await res.text()
                throw Error(err)
            }
        })
    }
}
