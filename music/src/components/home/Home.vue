<template>
	<div id="home">
		<LunBo></LunBo>
		<TuJian @GetSongId="getSongId" @GetGeDanId="getGeDanId" @GetMVId="getMVId" @GetSingerId="getSingerId"></TuJian>
	</div>
</template>

<script>
	import LunBo from "./LunBo.vue";
	import TuJian from "./TuiJian.vue";

	export default{
		data(){
			return{
				SongId:0,
			}
		},
		components:{
			LunBo,
			TuJian,
		},
		methods:{
			//获取歌曲id
			getSongId:function(value){
				this.$emit("GetSongId",value);
			},
			
			//获取歌单id
			getGeDanId:function(id){
				this.$emit("GetGeDanId",id);
				console.log(this.$router);
			},
			
			//获取歌手id
			getSingerId:function(id){
				this.$emit("GetSingerId",id);
			},
			
			//获取歌单id
			getMVId:function(id){
				this.$emit("GetMVId",id);
			}
		}
	}
</script>

<style>
	
</style>
