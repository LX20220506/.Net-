<template>
	<div id="gdxiangqing">
		<a-row>
			<a-col :span="17">
				<a-row>
					<a-col :span="12">
						<div class="fengmian">
							<img :src="gd.pic" />
						</div>
					</a-col>

					<a-col :span="12">
						<div class="zuozhe">
							<div>
								<h1>{{gd.name}}</h1>
							</div>
							<div>
								<img :src="gd.zuozhepic" />&nbsp;&nbsp;&nbsp;&nbsp;
								{{gd.zuozhename}}&nbsp;&nbsp;&nbsp;&nbsp;
								{{gd.date}} 创作
							</div>
							<div>
								标签：<a-button v-for="item in gd.tags" :key="item.id" size="small">{{item}}</a-button>
							</div>
							<div>
								介绍：<h4 style="">{{gd.jieshao}}</h4>
							</div>
						</div>
					</a-col>
				</a-row>

				<div class="list">
					<div>
						<span class="title"><span style="color:#0c8ed9;">歌单列表</span></span>
					</div>
					<a-divider style="height: 2px; background-color: #0c8ed9;margin-top: 5px;margin-bottom: 0px;" />
					
					<div id="songlist">
						<a-list item-layout="horizontal" :data-source="songList">
							<a-list-item slot="renderItem" slot-scope="item,index" >
								<h2>{{index+1}}</h2>
								<a-list-item-meta :description="item.singer">
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
				</div>
			</a-col>

			<a-col :span="7">
				<div class="tuijian">
					<h2>推荐歌单</h2>
					<hr style="margin-top: -5px;" />
					<ul>
						<li class="menu_li" v-for="(item) in tuiJianList" :key="item.index" >
							<a @click="initGD(item.id)">
								<a-row>
									<a-col :span="6">
										<img :src="item.pic">
									</a-col>
									<a-col :span="18">
										 <h3>{{item.name}}</h3>
										<span class="updateFrequency">{{item.zuozhe}}</span>
									</a-col>
								</a-row>
							</a>
						</li>
					</ul>
				</div>
			</a-col>
		</a-row>


	</div>
</template>

<script>
	export default {
		data() {
			return {
				songList: [],
				gd: {
					id: 0,
					name: '',
					pic: '',
					tags: [],
					jieshao: '',
					zuozhename: '',
					date: '',
					zuozhepic: ''
				},
				tuiJianList:[]
			}
		},
		computed: {
			//相对于定义一个getId的变量 用来接收传过来的id
			id: function() {
				//返回路由传递过来的id
				return this.$route.query.id;
			}
		},
		beforeMount() {
			this.initGD(this.id);
		},
		methods: {
			//初始化
			initGD: function(id) {
				var _this = this;
				this.songList=[];
				this.$axios.get("https://autumnfish.cn/playlist/detail?id=" + id).then(response => {
					var data = response.data;
					_this.gd.name = data.playlist.name;
					_this.gd.pic = data.playlist.coverImgUrl;
					_this.gd.tags = data.playlist.tags;
					_this.gd.zuozhename = data.playlist.creator.nickname;
					_this.gd.zuozhepic = data.playlist.creator.avatarUrl;
					_this.gd.date = _this.formatDate(data.playlist.createTime);
					_this.gd.id = data.playlist.id;
					_this.gd.jieshao = data.playlist.description;

					data.privileges.forEach(function(item) {
						_this.$axios.get("https://autumnfish.cn/song/detail?ids=" + item.id).then(
							response => {
								var data = response.data.songs[0]
								var id = data.id;
								var singer = '';
								data.ar.forEach(function(item) {
									singer += item.name + "   ";
								})
								_this.songList.push({
									name: data.name,
									pic: data.al.picUrl,
									id: id,
									singer: singer
								})
							})
					})
				})
				this.tiuJian(id)
			},
			//时间戳转换方法    date:时间戳数字
			formatDate: function(time) {
				var date = new Date(time);
				var YY = date.getFullYear() + '-';
				var MM = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
				var DD = (date.getDate() < 10 ? '0' + (date.getDate()) : date.getDate());
				return YY + MM + DD;
			},
			//推荐歌单
			tiuJian:function(id){
				var _this=this;
				this.tuiJianList=[];
				this.$axios.get("https://autumnfish.cn/related/playlist?id="+id).then(response=>{
					var data=response.data.playlists;
					data.forEach(function(item){
						_this.tuiJianList.push({
							name:item.name,
							pic:item.coverImgUrl,
							zuozhe:item.creator.nickname,
							id:item.id,
						});
					})
				});
			},
			//下载音乐
			xiazaiSong: function(id, name) {
				this.$axios.get("https://autumnfish.cn/song/url?id=" + id).then(response => {
					let downUrl = response.data.data[0].url; // 音乐地址
					let fileName = name; // 文件名设置
					this.$axios({
						method: 'get',
						url: downUrl,
						responseType: 'blob',
						headers: {
							'content-type': 'audio/mpeg'
						}
					}).then((res) => {
						let blob = new Blob([res.data], {
							type: "mp3"
						}) // 创建blob 设置blob文件类型 data 设置为后端返回的文件(例如mp3,jpeg) type:这里设置后端返回的类型 为 mp3
						let downa = document.createElement('a') // 创建A标签
						let href = window.URL.createObjectURL(blob) // 创建下载的链接
						downa.href = href // 下载地址
						downa.download = fileName + ".mp3" // 下载文件名
						document.body.appendChild(downa)
						downa.click() // 模拟点击A标签
						document.body.removeChild(downa) // 下载完成移除元素
						window.URL.revokeObjectURL(href) // 释放blob对象
					}).catch(function(error) {
						console.log(error)
					})
				});
			},
			//传值  
			getSong:function(id){
				this.$emit("GetSongId",id);
			},
		}
	}
</script>

<style>
	#gdxiangqing {
		width: 80%;
		background-color: #fff;
		margin: 0 auto;
		padding-bottom: 30px;
	}

	#gdxiangqing .fengmian {
		width: 210px;
		height: 210px;
		background-color: #f1f3f4;
		border: 1px solid #959595;
		margin: 10px auto;
	}

	#gdxiangqing .fengmian img {
		width: 200px;
		height: 200px;
		margin: 5px;
	}

	#gdxiangqing .zuozhe {
		margin-top: 20px;

	}

	#gdxiangqing .zuozhe img {
		width: 50px;
		height: 50px;
	}

	#gdxiangqing .zuozhe button {
		margin-left: 10px;
	}

	#gdxiangqing .zuozhe div {
		margin: 10px;
	}

	#gdxiangqing .zuozhe div h4 {
		margin-top: -20px;
		margin-left: 40px;
	}

	#gdxiangqing .list {
		margin-top: 30px;
		width:90%;
		margin-left: 5%;
	}

	#gdxiangqing div .title {
		font-size: 24px;
		font-weight: 300;
		padding-right: 30px;
	}
	
	#gdxiangqing #songlist{
		margin-top: 30px;
	}
	#gdxiangqing #songlist h2{
		width: 30px;
		height: 20px;
		line-height: 40px;
		margin-top: 20;
		margin-left: 10px;
		margin-right: 5px;
	}
	
	#gdxiangqing .tuijian ul {
		list-style: none;
		margin: 0 auto;
	}
	
	#gdxiangqing .tuijian ul li {
		margin-top: 10px;
	}
	
	#gdxiangqing .tuijian li:hover {
		background-color: #d8dada;
	}
	
	
	#gdxiangqing .menu_li img{
		width: 65px;
		height: 65px;
	}
	
	#gdxiangqing .updateFrequency{
		color:#a2a2a2;
		font-size: 14px;
	}
	
	#gdxiangqing .tuijian{
		margin-top: 20px;
	}
	#gdxiangqing .tuijian li h3{
		width:100%;
		white-space: nowrap;
		overflow: hidden;
		text-overflow: ellipsis;
	}
	
	#gdxiangqing .ant-list-item-meta-description{
		width:400px;
		white-space: nowrap;
		overflow: hidden;
		text-overflow: ellipsis;
	}
</style>
