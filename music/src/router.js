import Vue from "vue";
import VueRouter from "vue-router";

Vue.use(VueRouter);

//引入组件
import Home from "./components/home/Home.vue";
import Song from "./components/song/Song.vue";
import GDXiangQing from "./components/gedan/GDXiangQing.vue";
import GeDan from "./components/gedan/GeDan.vue";
import Singer from "./components/singer/Singer.vue";
import SingerXiangQing from "./components/singer/SingerXiangQing.vue";
import MVXiangQing from "./components/MV/MVXiangQing.vue";
import MV from "./components/MV/MV.vue";
import Login from "./components/Login.vue";

//配置路由
const routes = [
	{
		path: "/",
		redirect: "/Main/Home"
	},
	{
		path: "/Home",
		component: Home,
	},
	{
		path: "/Song",
		component: Song,
	},
	{
		path: "/GeDan/GDXiangQing",
		component: GDXiangQing,
	},
	{
		path: "/GeDan",
		component: GeDan,
	},
	{
		path: "/Singer",
		component: Singer,
	},
	{
		path: "/Singer/SingerXiangQing",
		component: SingerXiangQing,
	},
	{
		path: "/MV/MVXiangQing",
		component: MVXiangQing,
	},
	{
		path: "/MV",
		component: MV,
	},
	{
		path:"/Login",
		component:Login,
	}
];

var router = new VueRouter({
	routes,
});

export default router;
