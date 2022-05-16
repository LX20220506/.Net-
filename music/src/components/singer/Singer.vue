<template>
	<div id="singer">
		<a-row>
			<a-col :span="6">
				<div>
					<a-menu style="width: 256px" :default-selected-keys="['0']" :open-keys.sync="openTypeKeys" mode="inline">
						<a-sub-menu key="sub1">
							<span slot="title">
								<a-icon type="unordered-list" /><span>歌手类型</span>
							</span>
						<!-- 	这里的key不是v-for的  是左侧导航default-selected-keys的索引 
							该索引必须是string类型的  索引将index转换成了字符串 -->
							<a-menu-item v-for="(item,index) in type" :key="index+''" @click="typeClick(item.id)" >
								{{item.name}}
							</a-menu-item>
						</a-sub-menu>
					</a-menu>
					
					<a-menu style="width: 256px" :default-selected-keys="['0']" :open-keys.sync="openAreaKeys" mode="inline">
						<a-sub-menu key="sub1">
							<span slot="title">
								<a-icon type="unordered-list" /><span>地区分类</span>
							</span>
							<a-menu-item v-for="(item,index) in area" :key="index+''" @click="areaClick(item.id)" >
								{{item.name}}
							</a-menu-item>
						</a-sub-menu>
					</a-menu>
				</div>
			</a-col>

			<a-col :span="17">
				<div class="content">
					<div><span class="title">歌手列表</span></div>
					<a-divider style="height:2px; background-color: #0c8ed9;margin-top: 0px;margin-bottom: 0px;" />
					<div class="zimu">
						<ul>
							<li ><a class="select" @click="zimuCkick($event)">热门</a></li>
							<li v-for="item in zimuList" :key="item.id" > <a @click="zimuCkick($event)" >{{item}}</a></li>
						</ul>
					</div>
					<div style="clear: both;"></div>
					<a-row style="margin-top: 20px;" type="flex" justify="space-around" wrap="true" :gutter="[32,32]" >
						<a-col :span="4.1" v-for="item in singerList" :key="item.id">
							<dl @click="getSingerId(item.id)">
								<dt><img :src="item.pic" /></dt>
								<dd>{{item.name}}</dd>
							</dl>
						</a-col>
					</a-row>
				</div>
				<div class="fenye">
					<a-pagination :default-current="1" :current="current" :defaultPageSize="28" :total="500" @change="fenyeClick" />
				</div>
			</a-col>
		</a-row>

	</div>
</template>

<script>
	export default {
		data() {
			return {
				openTypeKeys: ['sub1'],//type导航 开始打开的的数组
				openAreaKeys:['sub1'],//area导航 开始打开的的数组
				type: [{
						id: -1,
						name: '全部'
					},
					{
						id: 1,
						name: '男歌手'
					},
					{
						id: 2,
						name: '女歌手'
					},
					{
						id: 3,
						name: '乐队'
					},
				],
				area: [{
						id: -1,
						name: '全部'
					},
					{
						id: 7,
						name: '华语'
					},
					{
						id: 96,
						name: '欧美'
					},
					{
						id: 8,
						name: '日本'
					},
					{
						id: 16,
						name: '韩国'
					},
					{
						id: 0,
						name: '其他'
					},
				],
				typeID:-1,
				areaID:-1,
				initialID:'-1',//字母
				singerList:[],
				MVList:[],
				current:1,//分页页码
				zimuList:['A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','S','Y','Z'],
			};
		},
		methods: {
			//初始化
			init:function(typeid,areaid,initialid){
				var _this=this;
				this.singerList=[];
				this.$axios.get("https://autumnfish.cn/artist/list?limit=28&type="+typeid+"&area="+areaid+"&initial="+initialid).then(response=>{
					var data=response.data.artists;
					data.forEach(item=>{
						_this.singerList.push({
							id:item.id,
							name:item.name,
							pic:item.picUrl,
						});
					})
				})
			},
			
			//字母点击事件
			zimuCkick:function(e){
				//分页页码返回到1
				this.current=1;
				//删除 所有select类
				var selectList=document.getElementsByClassName("select");
				selectList.forEach(item=>{
					item.classList.remove("select");
				})
				//给选中的标签添加select类
				//target选中的dom标签对象
				e.target.classList.add("select");
				var initial=e.target.text;
				if(initial=="热门")
					initial="-1";
				this.initialID=initial;
				this.init(this.typeID,this.areaID,initial);
			},
			//歌手类型点击事件
			typeClick:function(typeid){
				this.typeID=typeid;
				//分页页码返回到1
				this.current=1;
				this.init(typeid,this.areaID,this.initialID);
			},
			//地区分类点击事件
			areaClick:function(areaid){
				this.areaID=areaid;
				//分页页码返回到1
				this.current=1;
				this.init(this.typeID,areaid,this.initialID);
			},
			//分页方法
			fenyeClick:function(page, pageSize){
				var _this=this;
				this.singerList=[];
				this.$axios.get("https://autumnfish.cn/artist/list?offset="+(page-1)*pageSize+"&limit=28&type="+this.typeID+"&area="+this.areaID+"&initial="+this.initialID).then(response=>{
					var data=response.data.artists;
					data.forEach(item=>{
						_this.singerList.push({
							id:item.id,
							name:item.name,
							pic:item.picUrl,
						});
					})
				})
				this.current=page;
			},
			//跳转歌手详情
			getSingerId:function(id){
				this.$emit("GetSingerId",id);
			},
		},
		beforeMount() {
			this.init(this.typeID,this.areaID,this.initialID);
		}
	};
</script>

<style>
	#singer {
		background-color: #FFFFFF;
		width: 80%;
		margin: 0 auto;
	}
	
	#singer .content{
		margin-top: 10px;
	}
	
	#singer .content .title{
		font-size: 24px;
		font-weight: 300;
		padding-right: 30px;
	}
	
	#singer .content dl dt{
		text-align: center;
	}
	#singer .content dl dt img{
		width: 150px;
		height: 150px;
		text-align: center;
	}
	#singer .zimu ul{
		list-style: none;
	}
	#singer .zimu ul li{
		float: left;
		padding-left: 13.5px;
		font-size: 18px;
	}
	#singer .zimu ul li a{
		color: #464646;
	}
	#singer .zimu ul li a:hover{
		color: #0C8ED9;
	}
	
	#singer .zimu .select{
		color: #0C8ED9;
	}
	#singer .fenye{
		margin: 30px auto;
		text-align: center;
	}
</style>
