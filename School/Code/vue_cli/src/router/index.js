import { createRouter, createWebHashHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Login from "../views/Login.vue"


const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    component: () => import('../views/About.vue')
  },
  {
    path:'/login',
    name:'Login',
    component:Login
  }
]

// //过滤器(跳转之前)
// router.beforeEach((to, from, next) => {
//   //to表示去哪里
//   //from表示从哪里来
//   //next表示下一步
//   let islogin = sessionStorage.getItem('token')!=null&&sessionStorage!="";
  
//   if(to.path == "/login"){
    
//     if(islogin){
//       next("/");
//     }else{
//       next();
//     }

//   }else{
   
//     // requireAuth:可以在路由元信息指定哪些页面需要登录权限
//     if( islogin) {
//       next();
//     }else{
//       next("/login");
//     }

//   }
// })

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
