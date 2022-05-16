<template>
	<div id="gedan">
		<div class="title"><span>歌单</span>
		<a-select default-value="全部" style="width: 120px" @change="check($event)">
		      <a-select-option value="全部">
		        全部
		      </a-select-option>
			  <a-select-option v-for="(item,index) in typeList" :key="index" :value="item">
		        {{item}}
		      </a-select-option>
		    </a-select>
		</div>
		<a-divider style="height:2px; background-color: #0c8ed9;margin-top: 0px;margin-bottom: 0px;" />
		<a-row class="content" type="flex" justify="space-around" :gutter="[32,32]" :wrap="true">
		      <a-col :span="5"  v-for="item in gdList" :key="item.id">
				  <a href="javascript:;" @click="getGeDanId(item.id)" >
					  <img :src="item.pic" alt="">
					  <h3>{{item.name}}</h3>
				  </a>
			  </a-col>
		</a-row>
		<div class="fenye">
			<a-pagination :default-current="1" :current="page" :defaultPageSize="28" :total="1000" @change="fenYe" />
		</div>
	
	</div>
</template>

<script>
	export default{
		data(){
			return {
				typeList:[],
				gdList:[],
				type:'全部',
				page:1
			}
		},
		beforeMount() {
			this.getType();
			this.initGeDan('全部');
		},
		methods:{
			//初始化类型
			getType:function(){
				var _this=this;
				this.$axios.get("https://autumnfish.cn/playlist/hot").then(response=>{
					var data= response.data.tags;
					data.forEach(function(item){
						_this.typeList.push(item.name)
					})
				})
			},
			//初始化歌单
			initGeDan:function(name){
				var _this=this;
				this.gdList=[];
				this.page=1;
				this.$axios.get("https://autumnfish.cn/top/playlist?limit=28&offset=0&tag='"+name+"'").then(response=>{
					var data= response.data.playlists;
					data.forEach(function(item){
						_this.gdList.push({
							name:item.name,
							id:item.id,
							pic:item.coverImgUrl
						})
					})
				}).catch(err=>{
					console.log(err);
				})
			},
			//下拉框事件
			check:function(event){//这里直接获取到下拉框选中的值
				this.initGeDan(event);
				this.page=1;
				console.log(this.page);
			},
			//分页方法
			fenYe:function(page, pageSize){
				var _this=this;
				this.gdList=[];
				this.$axios.get("https://autumnfish.cn/top/playlist?limit=28&offset="+(page-1)*pageSize+"&tag='"+this.type+"'").then(response=>{
					var data= response.data.playlists;
					data.forEach(function(item){
						_this.gdList.push({
							name:item.name,
							id:item.id,
							pic:item.coverImgUrl
						})
					})
				})
				this.page=page;
			},
			//向父组件传递歌单id
			getGeDanId:function(id){
				this.$emit("GetGeDanId",id);
			}
		}
	}
</script>

<style>
	#gedan{
		background-color: #ffffff;
		width: 80%;
		margin: 0 auto;
		padding-top: 10px;
	}
	
	#gedan .title{
		margin: 5px;
	}
	
	#gedan .title span{
		font-size: 24px;
		font-weight: 300;
		padding-right: 30px;
		margin-left: 10px;
	}
	
	#gedan img{
		height: 200px;
		width: 200px;
		
	}
	#gedan .fenye {
		padding-top: 30px;
		margin-left: 31%;
		padding-bottom: 30px;
	}
	#gedan .content{
		padding-top: 20px;
		text-align: center;
	}
</style>
