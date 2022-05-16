<template>
	<div id="tuijian">
		<a-row>
			<a-col :span="24">
				<div>
					<span class="title">推荐<span style="color:#0c8ed9;">歌单</span></span>
					<span> 华语 </span>
					<span class="gang"> | </span>
					<span> 流行 </span>
					<span class="gang"> | </span>
					<span> 电子 </span>
					<span class="gang"> | </span>
					<span> 摇滚 </span>
					<span class="gengduo1">更多<a-icon class="ico" type="arrow-right" /></span>
				</div>
				<a-divider style="height: 2px; background-color: #0c8ed9;margin-top: 5px;margin-bottom: 0px;" />
			</a-col>
		</a-row>
		
		<a-row type="flex" justify="space-between" :gutter="[32,32]" :wrap="true">
		      <a-col :span="5"  v-for="item in TuiJianList" :key="item.id">
				  <a href="javascript:;" @click="getGeDanId(item.id)" >
					  <img :src="item.pic" alt="">
					  <h3>{{item.name}}</h3>
				  </a>
			  </a-col>
		</a-row>
		
		<div style="height: 50px;"></div>
		
		<a-row>
			<a-col :span="24">
				<div>
					<span class="title">推荐<span style="color:#0c8ed9;">MV</span></span>
					<span class="gengduo2">更多<a-icon class="ico" type="arrow-right" /></span>
				</div>
				<a-divider style="height: 2px; background-color: #0c8ed9;margin-top: 5px;margin-bottom: 0px;" />
			</a-col>
		</a-row>
		
		<a-row type="flex" justify="space-between" :gutter="[32,32]" :wrap="true" class="mv">
		      <a-col :span="5"  v-for="item in MVList" :key="item.id" @click="getMVId(item.id)" >
				  <img :src="item.pic" alt="" >
				  <h3>{{item.name}}</h3>
			  </a-col>
		</a-row>
		
		<div style="height: 50px;"></div>
		
		<a-row type="flex" justify="space-between">
			<a-col :span="12">
				<a-row>
					<a-col :span="24">
						<div>
							<span class="title">热门<span style="color:#0c8ed9;">歌曲</span></span>
							<span> 华语 </span>
							<span class="gang"> | </span>
							<span> 流行 </span>
							<span class="gang"> | </span>
							<span> 电子 </span>
							<span class="gang"> | </span>
							<span> 摇滚 </span>
							<span class="gengduo3">更多<a-icon class="ico" type="arrow-right" /></span>
						</div>
						<a-divider style="height: 2px; background-color: #0c8ed9;margin-top: 5px;margin-bottom: 0px;" />
					</a-col>
				</a-row>
				
				<a-row type="flex" justify="space-between" :gutter="[32,32]" :wrap="true">
				      <a-col :span="24" >
						  <!-- 子向父传值 
						  在组件标签上定义一个事件 GetSongId为自定义事件
						  当GetSongId当这个事件被执行时 
						  执行的对应的方法 可以获取到传过来的值 -->
						  <SongList @GetSongId="getSongId"></SongList>
					  </a-col>
				</a-row>
			</a-col>
			<a-col :span="11">
				<a-row>
					<a-col :span="24">
						<div>
							<span class="title"><span style="color:#0c8ed9;">歌手</span></span>
							<span class="gengduo4">更多<a-icon class="ico" type="arrow-right" /></span>
						</div>
						<a-divider style="height: 2px; background-color: #0c8ed9;margin-top: 5px;margin-bottom: 0px;" />
					</a-col>
				</a-row>
				
				<a-row type="flex" justify="space-around" :gutter="[32,32]" wrap="true" class="singer" >
				      <a-col :span="11"  v-for="item in SingerList" :key="item.id"  >
						  <div @click="getSingerId(item.id)">
							  <img :src="item.pic" alt="">
							  <h3>{{item.name}}</h3>
						  </div>
					  </a-col>
				</a-row>
			</a-col>
		</a-row>
	</div>
</template>

<script>
	import SongList from "./SongList.vue";
	
	export default{
		data(){
			return{
				TuiJianList:[],
				MVList:[],
				SingerList:[]
			}
		},
		components:{
			SongList
		},
		beforeMount() {
			this.Init();
		},
		methods:{
			Init:function(){
				this.getGeDan();
				this.getMV();
				this.getSinger();
			},
			getGeDan:function(){
				var _this=this;
				//热门歌单
				//全部类型的歌单 https://autumnfish.cn/top/playlist?limit=12
				//推荐歌单
				var obj= this.$axios.get("https://autumnfish.cn/personalized?limit=8").then(obj=>{
					return obj.data.result;
				})
				obj.then(obj=>{
					obj.forEach(function(item){
						_this.TuiJianList.push({
							pic:item.picUrl,
							name:item.name,
							id:item.id
						})
					})
				})
			},
			getMV:function(){
				var _this=this;
				//最新mv
				var obj= this.$axios.get("https://autumnfish.cn/personalized/mv").then(obj=>{
					return obj.data.result;
				});
				
				obj.then(obj=>{
					obj.forEach(function(item){
						_this.MVList.push({
							name:item.name,
							pic:item. picUrl,
							id:item.id,
						});
					})
				});
			},
			//推荐歌手
			getSinger:function(){
				var _this=this;
				this.$axios.get("https://autumnfish.cn/top/artists?offset=0&limit=4").then(response=>{
					var data=response.data.artists;
					data.forEach(function(item){
						_this.SingerList.push({
							name:item.name,
							pic:item.img1v1Url,
							id:item.id,
						});
					})
				})
			},
			//获取子组件的值
			getSongId:function(value){
				this.$emit("GetSongId",value);
			},
			
			//发送歌单id
			getGeDanId:function(id){
				this.$emit("GetGeDanId",id);
			},
			
			//发送歌手id
			getSingerId:function(id){
				this.$emit("GetSingerId",id);
			},
			
			//发送mvid
			getMVId:function(id){
				this.$emit("GetMVId",id);
			},
		}
	}
</script>

<style>
	#tuijian {
		padding: 30px;
		width: 80%;
		margin: 0 auto;
		padding-top: 40px;
		background-color: #FFFFFF;
		margin-top: -5px;
	}
	#tuijian img{
		text-align: center;
		width:100%;
		height: 215px;
	}
	#tuijian h3{
		width: 200px;
		text-align: left;
	}
	#tuijian div .title{
		font-size: 24px;
		font-weight: 300;
		padding-right: 30px;
	}
	
	#tuijian .gang{
		padding-left:10px ;
		padding-right: 10px;
	}
	
	#tuijian .gengduo1{
		font-size: 18px;
		font-weight: 300;
		display: inline-block;
		margin-left: 760px;
		text-align: right;
	}
	
	#tuijian .gengduo2{
		font-size: 18px;
		font-weight: 300;
		display: inline-block;
		width: 1038px;
		text-align: right;
	}
	
	#tuijian .gengduo3{
		font-size: 18px;
		font-weight: 300;
		display: inline-block;
		margin-left: 182px;
		text-align: right;
	}
	
	#tuijian .gengduo4{
		font-size: 18px;
		font-weight: 300;
		display: inline-block;
		margin-left: 395px;
		text-align: right;
	}
	
	#tuijian .gengduo1:hover,.gengduo2:hover,.gengduo3:hover,.gengduo4:hover{
		color: #0c8ed9;
	}
	
	#tuijian .mv h3{
		text-align: center;
	}
	
	#tuijian .singer h3,img{
		text-align: center;
	}
	#tuijian .singer img{
		height: 220px;
		width: 220px;
	}
	
	#tuijian .ico{
		color: #0c8ed9;
	}

</style>
