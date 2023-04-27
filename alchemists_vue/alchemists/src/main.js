import { createApp } from 'vue'
import { createWebHashHistory, createRouter } from 'vue-router';
import { default as App } from '@/App.vue'
import { routes } from '@/assets/script/index.js'

//bootstrap :: UI framework
import { BootstrapVue3 } from 'bootstrap-vue-3';
import { BootstrapIconsPlugin } from 'bootstrap-icons-vue';

//bootstrap css
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css'

const router = createRouter({
    history: createWebHashHistory(),
    routes : routes
})

const vue = createApp(App);

vue.use(router);
vue.use(BootstrapVue3);
vue.use(BootstrapIconsPlugin);
vue.mount('#app');

//모든 파일에서 사용할 전역변수 작성
//vue.config.globalProperties.$변수명;

export { vue };