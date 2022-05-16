<template>
	<div id="song">
		<a-row>
			<a-col :span="7">
				<div id="Da">
					<div class="left">
						<ul>
							<li class="menu_li" v-for="(item,index) in menuList" :key="item.index" @click="qieHuan(item.id,index)">
								<a>
									<a-row>
										<a-col :span="6">
											<img :src="item.pic">
										</a-col>
										<a-col :span="18">
											 {{item.name}}<br />
											<span class="updateFrequency">{{item.updateFrequency}}</span>
										</a-col>
									</a-row>
								</a>
							</li>
						</ul>
					</div>
				</div>
			</a-col>
			<a-col :span="16">
				<div id="songlist">
					<a-list item-layout="horizontal" :data-source="data">
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
			</a-col>
		</a-row>
	</div>
</template>
<script>
	export default {
		data() {
			return {
				menuList: [],
				data: []
			};
		},
		beforeMount() {
			this.initMenu();
			this.initSong(19723756,0);
		},
		methods: {
			//初始话导航
			initMenu: function() {
				var _this = this;
				this.$axios.get("https://autumnfish.cn/toplist/detail").then(response => {
					var data = response.data.list;
					data.forEach(function(item) {
						_this.menuList.push({
							pic: item.coverImgUrl,
							name: item.name,
							updateFrequency: item.updateFrequency,
							id:item.id,
						})
					})
				})
			},
			//初始化歌曲
			initSong: function(id) {
				var _this = this;
				_this.data=[];

				//获取飙升榜歌曲
				var data = this.$axios.get("https://autumnfish.cn/playlist/detail?id="+id).then(response => {
					return response.data.privileges;
				});
				data.then(data => {
					data.forEach(function(item){
						_this.$axios.get("https://autumnfish.cn/song/detail?ids="+item.id).then(response=>{
							var data=response.data.songs[0]
							var id=data.id;
							var singer='';
							data.ar.forEach(function(item){
								singer+=item.name+"   "; 
							})
							_this.data.push({
								name:data.name,
								pic:data.al.picUrl,
								id:id,
								singer:singer
							})
						})
					})
				});
				
			},
			//切换榜单
			qieHuan:function(id,index){
				//删除之前选中导航的样式
				var liarr=document.getElementsByClassName("show");
				if(liarr.length!=0){
					liarr.forEach(item=>{
						item.classList.remove("show");
					})
				}
				
				//给刚刚点击的导航添加样式
				var menu_li=document.getElementsByClassName("menu_li");
				menu_li[index].classList.add("show");
				
				this.initSong(id);
				
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
			//点击播放
			getSong:function(id){
				this.$emit("GetSongId",id);
			}
		},
	};
</script>

<style>
	#song {
		width: 80%;
		margin: 0 auto;
		background-color: #FFFFFF;
	}

	#Da .left {
		margin-top: 30px;
	}

	#Da .left ul {
		list-style: none;
		margin: 0 auto;
		margin-bottom: 50px;
	}

	#Da .left ul li {
		height: 65px;
		margin-top: 10px;
	}

	#Da .left ul li a {
		color: #2a2a2a;
		font-weight: 300;
		font-size: 20px;
	}

	#Da .left ul li a:hover {
		color: white;
	}

	#Da .left li:hover {
		background-color: #d8dada;
	}
	
	
	#Da .left img{
		width: 65px;
		height: 65px;
	}
	
	#Da .updateFrequency{
		color:#a2a2a2;
		font-size: 14px;
	}

	#song li:hover {
		background-color: #d8dada;
	}

	#song .ico {
		font-size: 30px;
		color: #0c8ed9;
		padding: 10px;
		padding-top: 0px;
	}

	#song .ant-list-item-meta-content {
		height: 30px;
		line-height: 30px;
	}

	#song .ant-list-item-meta-content h4 {
		font-size: 16px;
	}

	#song .ant-avatar-image {
		margin-left: 10px;
	}

	#song .ant-avatar-image img {
		width: 100%;
		height: 100%;
	}
	
	#song #songlist{
		margin-top: 30px;
	}
	#song #songlist h2{
		width: 30px;
		height: 20px;
		line-height: 40px;
		margin-top: 20;
		margin-left: 10px;
		margin-right: 5px;
	}
	
	#song .show{
		background-color:#d8dada ;
	}
	#song .ant-list-item-meta-description{
		/* 超出指定宽度变成省略号 */
		width:500px;
		white-space: nowrap;
		overflow: hidden;
		text-overflow: ellipsis;
	}
</style>
