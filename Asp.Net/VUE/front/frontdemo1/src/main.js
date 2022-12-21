import { createApp } from 'vue'
import App from './App.vue'
import router from './route'

import './assets/main.css'
import axios from 'axios'
// Vue.prototype.axios = axios


createApp(App).use(router).mount('#app')
