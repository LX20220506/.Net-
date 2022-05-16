# js-cookie

## 说明

js-cookie是一个简单的，轻量级的[处理cookies的js API](https://www.npmjs.com/package/js-cookie)。

## 安装

~~~
npm install vue-cookies --save
~~~

## 引用

~~~vue
import Vue from 'vue'
import VueCookies from 'vue-cookies'
Vue.use(VueCookies)
~~~

## 创建

```js
//创建简单的cookie
Cookies.set('name', 'value');
//创建有效期为7天的cookie
Cookies.set('name', 'value', { expires: 7 });
//为当前页创建有效期7天的cookie
Cookies.set('name', 'value', { expires: 7, path: '' });
```

## 取值

```js
Cookies.get('name'); // => 'value'
Cookies.get('nothing'); // => undefined
//获取所有cookie
Cookies.get(); // => { name: 'value' }
```

## 删除值

```js
Cookies.remove('name');

//如果值设置了路径，那么不能用简单的delete方法删除值，需要在delete时指定路径
Cookies.set('name', 'value', { path: '' });
Cookies.remove('name'); // 删除失败
Cookies.remove('name', { path: '' }); // 删除成功
//注意，删除不存在的cookie不会报错也不会有返回
```

## 命名空间

如果担心不小心修改掉Cookies中的数据，可以用noConflict方法定义一个新的cookie。

```js
var Cookies2 = Cookies.noConflict();
Cookies2.set('name', 'value');12
```

## json相关

js-cookie允许你向cookie中存储json信息。

如果你通过set方法，传入Array或类似对象，而不是简单的string，那么js-cookie会将你传入的数据用JSON.stringify转换为string保存。

```js
Cookies.set('name', { foo: 'bar' });
Cookies.get('name'); // => '{"foo":"bar"}'
Cookies.get(); // => { name: '{"foo":"bar"}' }
```

如果你用getJSON方法获取cookie，那么js-cookie会用JSON.parse解析string并返回。

```js
Cookies.getJSON('name'); // => { foo: 'bar' }
Cookies.getJSON(); // => { name: { foo: 'bar' } }
```

## set方法支持的属性

1. expires
   定义有效期。如果传入Number，那么单位为天，你也可以传入一个Date对象，表示有效期至Date指定时间。默认情况下cookie有效期截止至用户退出浏览器。
2. path
   string，表示此cookie对哪个地址可见。默认为”/”。
3. domain
   string，表示此cookie对哪个域名可见。设置后cookie会对所有子域名可见。默认为对创建此cookie的域名和子域名可见。
4. secure
   true或false，表示cookie传输是否仅支持https。默认为不要求协议必须为https。

## 骚操作

------

1.read

> 通过withConverter方法可以覆写默认的decode实现，并返回一个新的cookie实例。所有与decode有关的get操作，如Cookies.get()或Cookies.get(‘name’)都会先执行此方法中的代码。

```js
document.cookie = 'escaped=%u5317';
document.cookie = 'default=%E5%8C%97';
var cookies = Cookies.withConverter(function (value, name) {
    if ( name === 'escaped' ) {
        return unescape(value);
    }
});
cookies.get('escaped'); // 北
cookies.get('default'); // 北
cookies.get(); // { escaped: '北', default: '北' }12345678910
```

------

2.write

> 通过withConverter方法也可以覆写默认的encode实现，并返回一个新的cookie实例。

```js
Cookies.withConverter({
    read: function (value, name) {
        // Read converter
    },
    write: function (value, name) {
        // Write converter
    }
});
```

# 处理JWT中的Token

>jwt有三部分 头部 信息 签名；将这三部分通过拼接就得到了token；拼接的字符是小数点(**.**)
>
>头部有 加密方式 "alg": "HS256"(加密方式为后台定义的方式),  token类型 "typ": "JWT"
>
>信息部分是后台定义的claim数组；前台需要的信息都放在这个里面
>
>签名是指 我们在后台会有一段字符串，对它进行加密，就成了第三部分

> 头部和信息部分都是通过Base64加密(默认加密)，签名部分是我们自定义加密的，我们可以通过atob()方法进行解密，拿到需要的信息

~~~ js
var info=atob(data.obj.token.split(".")[1]); // 拿到token中的信息(claim)
~~~

## 案例：

这是一个登陆操作，通过axios向后台请求数据，当后台判断登陆成功后，返回token，然后将token存在cookies中；

通过 atob() 方法，将用户的信息解密，并存放在cookies中

~~~js
axios.post("http://localhost:5000/api/Account/Login",{"userName":values.username,"userPwd":values.password}).then(response=>{
          
          let data=response.data;
          if (data.code==200){

            //sessionStorage.setItem("token",data.obj);
            //console.log(data);

            var millisecond = new Date().getTime();
            var expiresTime = new Date(millisecond + 60 * 1000 * 60);// 设置1个小时后过期(里面是毫秒)
            var info=atob(data.obj.token.split(".")[1]); // 拿到token中的信息(claim)
            //console.log("原始数据",data.obj.account);
            //console.log("json数据",JSON.parse(data.obj.account));

            Cookies.set('token', data.obj.token, { expires: expiresTime });//expires表示过期时间  expires: 7 则表示7天后到期
            Cookies.set('account',info, { expires:expiresTime });
            Cookies.set('type',data.obj.type, { expires:expiresTime });

           // console.log("cookies里的数据",Cookies.get("account"));
            router.push('/') ;// 登录成功后跳转到根目录
            
            //console.log(info);
          }else{
            message.error('账号或密码错误');
          }
      })
~~~

# vue-router

## 引用

~~~
npm install vue-router@4
~~~

## 配置

> 1.创建router文件夹
>
> 2.在router文件夹下创建index.js配置文件，并配置该文件
>
> 3.在main.js中引用router配置文件，进行全局配置

### index.js配置

~~~js
import { createRouter, createWebHashHistory } from "vue-router";
import Home from "../views/Home/Index";
import Login from "../views/Login";
import Index from "../views/Index";
import Cookies from "js-cookie";
import Layout from "../views/Layout/Index";
import Demo from "../views/Index_dome";

const routes = [
  // {
  //   path:" ",
  //   redirect:"/", // 当路由为 "" 时，路由重定向到"/"
  // },
  {
    path: "/",
    name: "Layout",
    component: Layout,
    meta: {
      requireAuth: true,  // 添加该字段，表示进入这个路由是需要登录的,没有该字段的默认不用登录
    },
    // 嵌套路由
    // 可以让Layout页面中，显示Home和Demo页面
    // 通过router-link 和 router-view 标签实现页面的嵌套
    children:[
      {
        path: "/home",
        name: "Home",
        component: Home,
      },
      {
        path:"/demo",
        name:"Demo",
        component:Demo
      }
    ]
  },
  
  {
    path: "/about",
    name: "About",
    component: () => import("../views/About.vue"),
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/index",
    name: "Index",
    component: Index,
  },
];

// 创建一个路由
const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

// 路由守卫，每次跳转时都会过这个方法
router.beforeEach((to, from, next) => {

  //to表示去哪里
  //from表示从哪里来
  //next表示下一步
  let islogin = Cookies.get("token") != null && Cookies.get("token") != "";

  // requireAuth:可以在路由元信息指定哪些页面需要登录权限
  if (to.meta.requireAuth) {
    if (islogin) {
      next();
    } else {
      next("/login");
    }
  } else {
    next();
  }
});

// 返回路由
export default router;

~~~

### main.js配置

~~~js
import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; // 引用路由
import Antd from 'ant-design-vue';
import 'ant-design-vue/dist/antd.css';
import Cookies from 'js-cookie';

// use(router) 挂载
createApp(App).use(Antd).use(router).use(Cookies).mount('#app')
~~~

## 常用属性和方法

### 属性

~~~js
<router-link to="/demo">demo</router-link>
<router-view></router-view>

// to      ：要跳转的router
// :to     ：绑定to
// :style  :绑定样式
~~~

### 方法

~~~js
// push() ：跳转到指定路由
router.push('/') ;
~~~

# axios

详细信息自行查看官方文档

## 模板

~~~js
this.$axios.post(api地址,{请求对象}).then(res=>{
    //res.data 返回的数据
}).catch(error=>{
    //请求失败后的处理
})
~~~



## 请求案例

~~~js
axios.post("http://localhost:5000/api/Account/Login",{"userName":values.username,"userPwd":values.password}).then(response=>{
          
          let data=response.data;
          if (data.code==200){

            var millisecond = new Date().getTime();
            var expiresTime = new Date(millisecond + 60 * 1000 * 60);// 设置1个小时后过期(里面是毫秒)
            var info=atob(data.obj.token.split(".")[1]); // 拿到token中的信息(claim)

            Cookies.set('token', data.obj, { expires: expiresTime });//expires表示过期时间  expires: 7 则表示7天后到期
            Cookies.set('account',info, { expires:expiresTime });

            router.push('/') ;
            
          }else{
            message.error('账号或密码错误');
          }
      })
~~~

## 拦截器案例

> 拦截器是指在axios向server请求前后进行的操作
>
> 下列案例是在请求发出之前，设置Authorization的值

~~~js
import axios from "axios";
import Cookies from "js-cookie";

var service=axios.create({
        baseURL: process.env.VUE_APP_BASE_URL, // url = base url + request url
        timeout: 10000, // request timeout
    });

// 拦截器--- 在请求之前设置Authorization的值，以便后台进行身份验证
service.interceptors.request.use(
    // config请求的信息
    config=>{
        let token = Cookies.get("token") != null && Cookies.get("token") != "";
        if (token){
            // config.headers是请求携带的headers
            config.headers['Authorization']="Bearer "+Cookies.get("token");
            config.headers["Access-Control-Allow-Origin"]="*";
        }

        return config;
    },error=>{
        return Promise.reject(error)
    }
)

//还有请求完成之后的拦截器

export default service
~~~

