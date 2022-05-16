<template>
    <div id="login">
        <div id="signinbox">
          <a-form
    :model="formState"
    name="normal_login"
    class="login-form"
    @finish="onFinish"
    @finishFailed="onFinishFailed"
  >
    <a-form-item
      label="姓 名"
      name="username"
      :rules="[{ required: true, message: '请输入姓名!' }]"
    >
      <a-input v-model:value="formState.username">
        <template #prefix>
          <UserOutlined class="site-form-item-icon" />
        </template>
      </a-input>
    </a-form-item>

    <a-form-item
      label="密 码"
      name="password"
      :rules="[{ required: true, message: '请输入密码!' }]"
    >
      <a-input-password v-model:value="formState.password">
        <template #prefix>
          <LockOutlined class="site-form-item-icon" />
        </template>
      </a-input-password>
    </a-form-item>

    <div class="login-form-wrap">
      <a-form-item name="remember" no-style>
        <a-checkbox v-model:checked="formState.remember">记住密码</a-checkbox>
        
        <a class="login-form-forgot"  href="">忘记密码</a>
      </a-form-item>
      
    </div>

    <a-form-item>
      <a-button :disabled="disabled" type="primary" html-type="submit" class="login-form-button">
        Log in
      </a-button>
    </a-form-item>
  </a-form>
      </div>
    </div>
</template>

<script>
import { UserOutlined, LockOutlined,message } from '@ant-design/icons-vue';
import { reactive, computed } from 'vue';

export default {
    components: {
    UserOutlined,
    LockOutlined,
  },

  setup() {
    const formState = reactive({
      username: '',
      password: '',
      remember: true,
    });

    const onFinish = values => {
      console.log('Success:', values.password);
      
      let fromData = new FormData()
                    fromData.append('a', 111)
                    fromData.append('b', 222)

      console.log(values.username);
      this.$axios.post("http://localhost:5000/api/Account/Login",{"userName":values.username,"userPwd":values.password}).then(response=>{
          let data=response.data;
          if (data.code==200){
            sessionStorage.setItem("token",data.obj);
            this.router.push('/') ;
          }else{
            message.error('This is an error message');
          }
      })
    };

    const onFinishFailed = errorInfo => {
      console.log('Failed:', errorInfo);
    };

    const disabled = computed(() => {
      return !(formState.username && formState.password);
    });
    return {
      formState,
      onFinish,
      onFinishFailed,
      disabled,
    };
  },
}
</script>
