<template>
  <div id="login">
	  <div class="login-container">
	    <h2 class="login-title">韵雅音乐</h2>
	    <a-form ref="form" :model="form" class="login-form" style="margin: 0 auto;">
	      <h2 class="title">用户登录</h2>
	      <a-form-item>
	        <a-input class="inputBox" v-model="form.username">
	          <img :src="user" slot="prefix" alt="" style="margin-bottom: 10px;" >
	        </a-input>
	      </a-form-item>
	      <a-form-item>
	        <a-input-password class="inputBox" v-model="form.password">
	          <img :src="mima" slot="prefix" alt="" style="margin-bottom: 10px;" >
	        </a-input-password>
	      </a-form-item>
	  
	      <a-form-item>
	        <a-button class="submit" type="primary" @click="onSubmit">登录</a-button>
	      </a-form-item>
	    </a-form>
	  </div>
  </div>
</template>

<script>
import user from "../assets/user.png"
import mima from "../assets/mima.png"
export default {
  data() {
    return {
      user:user,
      mima:mima,
      form: {
        username: "admin",
        password: "123456"
      }
    }
  },
  methods: {
     onSubmit()  {
      console.log('登陆成功！')
    },
  },
}
</script>
<style>
#login .login-form {
  width: 565px;
  height: 372px;
  margin: 0 auto;
  /* background: url("../assets/houTaiKuang.png"); */
  padding: 40px 110px;
}

/* 背景 */
#login .login-container {
  position: absolute;
  width: 100%;
  height: 100%;
  background: url("../assets/houTaiBg.png");
}

/* Log */
#login .login-title {
  color: #fff;
  text-align: center;
  margin: 150px 0;
  font-size: 48px;
  font-family: Microsoft Yahei;
}
/* 登陆按钮 */
#login .submit{
  width: 100%;
  height: 45px;
  font-size: 16px;
}
/* 用户登陆标题 */
#login .title{
  margin-bottom: 50px;
  color: #fff;
  font-weight: 700;
  font-size: 24px;
  font-family: Microsoft Yahei;
}
/* 输入框 */
#login .inputBox{
  height: 45px;
}
/* 输入框内左边距50px */
#login .ant-input-affix-wrapper .ant-input:not(:first-child) {
    padding-left: 50px;
}
</style>
