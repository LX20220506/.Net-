import Vue from "vue";
import App from "./App.vue";
import antd from "ant-design-vue";
import router from "./router.js";
import axios from 'axios';
//音乐播放器插件
import APlayer from '@moefe/vue-aplayer';
//视频播放插件
import VideoPlayer from 'vue-video-player';
require('video.js/dist/video-js.css');
require('vue-video-player/src/custom-theme.css');
Vue.use(VideoPlayer);

import "ant-design-vue/dist/antd.css";
Vue.use(antd);
Vue.use(APlayer);

Vue.config.productionTip = false;

Vue.prototype.$axios=axios;

new Vue({
  render: (h) => h(App),
  router,
}).$mount("#app");
