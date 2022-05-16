<template>
	<div id="singerxiangqing" >
		<a-row>
			<a-col :span="17">
				<div class="title">
					<div>
						<span class="name">{{singer.name}}</span>
						<span class="english">[{{singer.englishname}}]</span>
					</div>
					<img :src="singer.pic" />
				</div>
				
				<div class="content">
					<a-tabs default-active-key="1" class="tabs">
					    <a-tab-pane key="1" tab="歌单列表">
					      <div id="songlist">
					      	<a-list item-layout="horizontal" :data-source="songList">
					      		<a-list-item slot="renderItem" slot-scope="item,index" >
					      			<h2>{{index+1}}</h2>
					      			<a-list-item-meta >
					      				<a slot="title" href="javascript:;" @click="getSong(item.id)">{{ item.name }}</a>
					      				<a-avatar slot="avatar" :src="item.pic" />
					      			</a-list-item-meta>
					      			<div class="caozuo">
					      				<a-icon class="ico" type="caret-right" theme="filled" @click="getSong(item.id)" />
					      				<a-icon class="ico" type="vertical-align-bottom"
					      					@click="xiazaiSong(item.id,item.name)" />
					      			</div>
					      		</a-list-item>
					      	</a-list>
					      </div>
					    </a-tab-pane>
						
					    <a-tab-pane key="2" tab="相关MV" >
					      <a-row type="flex" class="mv" justify="space-around" wrap="true">
							  <a-col :span="7" v-for="item in MVList" :key="item.id" @click="getMVId(item.id)" >
								  <img :src="item.pic" />
								  <h3>{{item.name}}</h3>
							  </a-col>
						  </a-row>
					    </a-tab-pane>
						
					    <a-tab-pane key="3" tab="艺人介绍">
							<dl>
								 
								  <dt><span style="height:40px ;margin-right: 8px;  width: 20px;background-color: #0C8ED9;">&nbsp;</span>{{singer.name}}简介</dt>
								  <dd>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span v-text="info.jianjie"></span></dd>
							  </dl>
							  
							  <dl v-for="item in info.txtlist" :key="item.index" >
							  	 
							  	  <dt>{{item.title}}</dt>
							  	  <dd><span v-text="item.txt"></span></dd>
							    </dl>
					    </a-tab-pane>
					  </a-tabs>
				</div>
			</a-col>
		
			<a-col :span="7">
				<div class="tuijian">
					<h2>推荐歌手</h2>
					<hr style="margin-top: -5px;" />
					<a-row type="flex" justify="space-around" wrap="true" :gutter="[32,32]" >
						<a-col :span="7" v-for="item in tuijianSinger" :key="item.id">
							<dl @click="qiehuanSinger(item.id)" >
								<dt><img :src="item.pic" /></dt>
								<dd>{{item.name}}</dd>
							</dl>
						</a-col>
					</a-row>
				</div>
			</a-col>
		</a-row>
		
	</div>
</template>

<script>
	export default{
		data(){
			return{
				//歌手
				singer:{
					name:'',
					pic:'',
					englishname:'',
				},
				//简介
				info:{
					jianjie:'',
					txtlist:[],
				},
				//歌曲
				songList:[],
				//推荐歌手
				tuijianSinger:[],
				MVList:[],
			}
		},
		computed:{
			//获取id
			id:function(){
				return this.$route.query.id;
			}
		},
		beforeMount() {
			this.getSingerAndSong(this.id);
			this.getTuiJianSinger(this.id);
			this.getInfo(this.id);
			this.getMV(this.id);
		},
		methods:{
			//歌手简介信息
			getInfo:function(id){
				var _this=this;
				this.$axios.get("https://autumnfish.cn/artist/desc?id="+id).then(response=>{
					var data=response.data;
					_this.info.jianjie=data.briefDesc;
					data.introduction.forEach(function(item){
						_this.info.txtlist.push({
							title:item.ti,
							txt:item.txt,
						})
					})
				})
			},
			//歌手信息
			getSingerAndSong:function(id){
				var _this=this;
				this.$axios.get("https://autumnfish.cn/artists?id="+id).then(response=>{
					var data=response.data;
					_this.singer.name=data.artist.name;
					_this.singer.englishname=data.artist.alias[0];
					_this.singer.pic=data.artist.picUrl;
					data.hotSongs.forEach(function(item){
						_this.songList.push({
							id:item.id,
							name:item.name,
							pic:item.al.picUrl,
						});
					})
				})
			},
			//推荐歌手
			getTuiJianSinger:function(id){
				//https://autumnfish.cn/simi/artist?id=5781
				var _this=this;
				this.$axios.get("https://autumnfish.cn/simi/artist?limit=6&id="+id).then(response=>{
					var data=response.data.artists;
					data.forEach(item=>{
						_this.tuijianSinger.push({
							id:item.id,
							name:item.name,
							pic:item.img1v1Url,
						});
					})
				})
			},
			//下载音乐
			xiazaiSong:function(id,name){
					   this.$axios.get("https://autumnfish.cn/song/url?id=" + id).then(response => {
						let downUrl = response.data.data[0].url; // 音乐地址
						let fileName = name; // 文件名设置
						this.$axios({
						        method: 'get',
						        url: downUrl,
						        responseType: 'blob',
						        headers: {'content-type': 'audio/mpeg'}
						      }).then((res) => {
								let blob = new Blob([res.data], {type:"mp3" }) // 创建blob 设置blob文件类型 data 设置为后端返回的文件(例如mp3,jpeg) type:这里设置后端返回的类型 为 mp3
								let downa = document.createElement('a') // 创建A标签
								let href = window.URL.createObjectURL(blob) // 创建下载的链接
								downa.href = href // 下载地址
								downa.download = fileName+".mp3" // 下载文件名
								document.body.appendChild(downa)
								downa.click() // 模拟点击A标签
								document.body.removeChild(downa) // 下载完成移除元素
								window.URL.revokeObjectURL(href) // 释放blob对象
						      }).catch(function (error) {
						        console.log(error)
						      })
					  });
			},
			//播放歌曲--传值
			getSong:function(id){
				this.$emit("GetSongId",id);
			},
			
			//点击切换歌手
			qiehuanSinger:function(id){
				this.id=id;
				this.singer={
					name:'',
					pic:'',
					englishname:'',
				};
				this.info={
					jianjie:'',
					txtlist:[],
				};
				this.songList=[];
				this.tuijianSinger=[];
				this.getInfo(id);
				this.getSingerAndSong(id);
				this.getTuiJianSinger(id);
				this.getMV(id);
			},
			//获取歌手mv
			getMV:function(id){
				var _this=this;
				this.$axios.get("https://autumnfish.cn/artist/mv?limit=12&id="+id).then(response=>{
					var data=response.data.mvs;
					data.forEach(item=>{
						_this.MVList.push({
							name:item.name,
							id:item.id,
							pic:item.imgurl,
						});
					})
				})
			},
			
			//mvid--传值
			getMVId:function(id){
				this.$emit("GetMVId",id);
			}
		}
	}
	
</script>

<style>
	#singerxiangqing{
		background-color: #FFFFFF;
		width: 80%;
		margin: 0 auto;
	}
	
	#singerxiangqing .title{
		width: 800px;
		margin: 0 auto;
		margin-top: 20px;
	}
	
	#singerxiangqing .name{
		font-size: 24px;
		color: #000000;
		margin-right: 15px;
	}
	
	#singerxiangqing .english{
		font-size: 16px;
	}
	
	#singerxiangqing .title img{
		width: 800px;
		height: 350px;
		object-fit: cover;
		margin: 0 auto;
		border: 2px solid #7a7a7a;
		padding: 0px;
	}
	
	#singerxiangqing .content {
		width: 800px;
		margin: 20px auto;
	}
	
	#singerxiangqing #songlist h2{
		width: 30px;
	}
	
	#singerxiangqing #songlist .ant-list-item-meta-title{
		height: 30px;
		line-height: 30px;
	}
	
	#singerxiangqing .tuijian{
		margin-top: 20px;
	}
	
	#singerxiangqing .tuijian dl dt,dd{
		text-align: center;
	}
	
	#singerxiangqing .tuijian dl img{
		width: 80px;
		height: 80px;
	}
	#singerxiangqing .content .tabs dl {
		margin-bottom: 30px;
	}
	
	#singerxiangqing .content .tabs dl dd,dt{
		text-align: left;
	}
	#singerxiangqing .content .tabs dl dt{
		font-size: 18px;
		color: #000;
	}
	#singerxiangqing .content .tabs dl dd{
		line-height: 30px;
	}
	
	#singerxiangqing .mv img{
		width: 160px;
		height: 90px;
		margin-top: 10px;
	}
	#singerxiangqing .mv{
		margin: 0 auto;
		text-align: center;
	}
	#singerxiangqing .mv h3{
		width: 160px;
		margin: 0 auto;
		margin-bottom: 10px;
		white-space: nowrap;
		overflow: hidden;
		text-overflow: ellipsis;
	}
</style>
