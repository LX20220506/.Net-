<template>
	<div id="mv" >
		<div class="title"><span>歌单</span>
		<a-select default-value="全部" style="width: 120px" @change="check($event)">
		      
			  <a-select-option v-for="(item,index) in typeList" :key="index" :value="item">
		        {{item}}
		      </a-select-option>
		    </a-select>
		</div>
		<a-divider style="height:2px; background-color: #0c8ed9;margin-top: 0px;margin-bottom: 0px;" />
		
		<a-row class="content" type="flex" justify="space-around" :gutter="[32,32]" :wrap="true">
		      <a-col :span="5"  v-for="item in mvList" :key="item.id">
				  <a href="javascript:;" @click="getMVId(item.id)" >
					  <img :src="item.pic" alt="">
					  <h3>{{item.singer}}&nbsp;-&nbsp;{{item.title}}</h3>
				  </a>
			  </a-col>
		</a-row>
		<div class="fenye">
			<a-pagination :default-current="1" :current="page" :defaultPageSize="30" :total="count" @change="fenYe" />
		</div>
	
	</div>
</template>

<script>
	export default {
		data(){
			return {
				count:0,
				typeList:['全部','内地','港台','欧美','日本','韩国'],
				mvList:[],
				page:1,
				type:'全部',
			};
		},
		methods:{
			//初始化
			init:function(type){
				var _this=this;
				this.mvList=[];
				this.$axios.get("https://autumnfish.cn/mv/all?limit=28&order=最热&area="+type).then(response=>{
					_this.count=response.data.count;
					var data=response.data.data;
					data.forEach(item=>{
						_this.mvList.push({
							title:item.name,
							id:item.id,
							singer:item.artistName,
							pic:item.cover,
						});
					})
				});
			},

			//下拉框值改变触发事件
			check:function(e){
				this.type=e;
				this.page=1;
				this.init(e);
			},
			
			//分页
			fenYe:function(page,pageSize){
				var _this=this;
				this.mvList=[];
				this.page=page;
				this.$axios.get("https://autumnfish.cn/mv/all?offset="+(page-1)*pageSize+"&limit=28&order=最热&area="+_this.type).then(response=>{
					var data=response.data.data;
					data.forEach(item=>{
						_this.mvList.push({
							title:item.name,
							id:item.id,
							singer:item.artistName,
							pic:item.cover,
						});
					})
				});
			},
			
			//获取吗mvid  传值
			getMVId:function(id){
				this.$emit("GetMVId",id);
			}
		},
		beforeMount() {
			this.init(this.type);
		}
	}
</script>

<style>
	#mv {
		background-color: #fff;
		width: 80%;
		margin: 0 auto;
		margin-top: -5px;
	}
	
	#mv .title{
		margin: 5px;
	}
	
	#mv .title span{
		font-size: 24px;
		font-weight: 300;
		padding-right: 30px;
		margin-left: 10px;
	}
	
	#mv img{
		height:120px;
		width: 190px;
		
	}
	#mv .fenye {
		padding-top: 30px;
		margin-left: 31%;
		padding-bottom: 30px;
	}
	#mv .content{
		padding-top: 20px;
		text-align: center;
	}
	
	#mv .content h3{
		white-space: nowrap;
		overflow: hidden;
		text-overflow: ellipsis;
	}
</style>


