<template>
  <div id="index">
	  <Head></Head>
	  <!--顶部导航-->
	  <div id="top">
	      <ul>
	          <router-link tag="li" to="/Home">首 页</router-link>
	  		  <router-link tag="li" to="/Song/">歌 曲</router-link>
	  		  <router-link tag="li" to="/GeDan">歌 单</router-link>
	  		  <router-link tag="li" to="/Singer">歌 手</router-link>
	  		  <router-link tag="li" to="/MV">M V</router-link>
	      </ul>
	  </div>
	  <router-view @GetSongId="getSong" @GetGeDanId="getGeDanId" @GetMVId="getMVId" @GetSingerId="getSingerId">
		  
	  </router-view>
	 <!-- 将SongId传给APlayer组件 -->
	  <APlayer  :SongId="SongId"></APlayer>
    <Foot></Foot>
  </div>
</template>

<script>
	import Head from "./public/Head.vue";
	import Foot from "./public/Foot.vue";
	import APlayer from "./public/APlayer.vue";
export default {
  name: "App",
  components: {
	Head,
	Foot,
	APlayer,
  },
  data(){
	  return {
		  SongId:0,
		  
	  }
  },
  methods:{
	  //接收歌手id
	  getSong:function(value){
		  this.SongId=value;
	  },
	  getGeDanId:function(id){
		  this.$router.push("/GeDan/GDXiangQing?id="+id);
		  
	  },
	  getSingerId:function(id){
		  this.$router.push("/Singer/SingerXiangQing?id="+id);
	  },
	  getMVId:function(id){
		  this.$router.push("/MV/MVXiangQing?id="+id);
	  }
  }
};
</script>

<style>
	#top {
	    margin: auto;
	    width: 100%;
	    height: 50px;
	    line-height: 50px;
	    background-color: #2c323b;
	}
	
	    #top ul {
	        width: 80%;
	        margin: 0 auto;
	        list-style-type: none;
	        font-size: 24px;
	        font-weight: bold;
			padding: 0px;
			color: #fff;
	    }
	
	        #top ul li {
	            float: left;
	            width: 20%;
	            text-decoration: none;
	            text-align: center;
	        }
	
	        #top ul .choose {
	            background-color:  #0c8ed9;
	        }
			
	        #top ul li a {
	            color: #fff;
	        }
	
	        #top ul li:hover {
	            background-color: #0c8ed9;
	        }
			
			.router-link-active{
				background-color:#0c8ed9 ;
			}
</style>
