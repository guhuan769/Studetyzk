<template>
    用户名: <input type="text" v-model="state.loginData.userName" />
    密码: <input type="password" v-model="state.loginData.password" />
    <input type="submit" value="登录" @click="loginSubmit" />
    <ul>
        <li v-for="p in state.processes" :key="p.id">{{ p.name }}{{ p.workingSet }}</li>
    </ul>
</template>

<script>

import axios from 'axios';
import { reactive, onMounted } from 'vue'

//demo1.js
// export-default.js

export default {
    name:'Login',
    setup() {
        const state = reactive({ loginData: {}, processes: [] });
        const loginSubmit = async ()=>{
            const payload = state.loginData;
            const resp = await axios.post('https://localhost:7000/api/Login/Login',payload);
            const data = resp.data;
            if(!data.ok)
            {
                alert("登录失败");
                return;
            }
            state.processes = data.processInfos;
        }
        return  {state,loginSubmit}
    }
}
</script>
<!-- 前端发ajax请求一般使用 axios -->