import Util from './util.js'
import {Channel} from './channel.js'
import {Network} from './network.js'

Util.API('GET', 'https://localhost:5001/Network').then(res => {
   if(res.length != 0) {
      res.map(Network.fromJSON)
         .forEach(net => net.draw(document.body))
   }
   else {
      const name = prompt("Unesite ime TV mreze:")
      Util.API('POST', 'https://localhost:5001/Network', { name: name }).then(res => {
         Network.fromJSON(res).draw(document.body)
      }).catch(alert)
   }
})

