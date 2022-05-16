<template>
	<div id="mvxiangqing">
		<a-row>
			<a-col :span="17">
				<div class="title">
					<div>
						<span style="font-size: 28px; color: #0C8ED9;margin-right: 15px;">MV</span>
						<span class="name">{{MVTitle}}</span>
						<span class="singer">{{singer}}</span>
					</div>
				</div>

				<div class="content">
					<video-player class="video-player vjs-custom-skin" ref="videoPlayer" :playsinline="true"
						:options="playerOptions"></video-player>
					<div class="xq"><h3>发布时间：</h3>{{createDate}}  &nbsp;&nbsp;&nbsp;&nbsp; <h3>播放次数：</h3>{{count}}</div>
				</div>
				
			</a-col>

			<a-col :span="7">
				<div>
					<div class="tuijian">
						<h2>推荐MV</h2>
						<hr style="margin-top: -5px;" />
						<ul>
							<li class="menu_li" v-for="(item) in tuijianMVList" :key="item.index" >
								<a @click="tuijianClick(item.id)">
									<a-row>
										<a-col :span="6">
											<img :src="item.pic">
										</a-col>
										<a-col :span="18">
											 <h3>{{item.name}}</h3>
											<span class="updateFrequency">{{item.singer}}</span>
										</a-col>
									</a-row>
								</a>
							</li>
						</ul>
					</div>
				</div>
			</a-col>
		</a-row>
	</div>
</template>

<script>
	export default {
		data() {
			return {
				playerOptions: {
					playbackRates: [0.7, 1.0, 1.5, 2.0], //播放速度
					autoplay: false, //如果true,浏览器准备好时开始回放。
					muted: false, // 默认情况下将会消除任何音频。
					loop: false, // 导致视频一结束就重新开始。
					preload: 'auto', // 建议浏览器在<video>加载元素后是否应该开始下载视频数据。auto浏览器选择最佳行为,立即开始加载视频（如果浏览器支持）
					language: 'zh-CN',
					aspectRatio: '16:9', // 将播放器置于流畅模式，并在计算播放器的动态大小时使用该值。值应该代表一个比例 - 用冒号分隔的两个数字（例如"16:9"或"4:3"）
					fluid: true, // 当true时，Video.js player将拥有流体大小。换句话说，它将按比例缩放以适应其容器。
					sources: [{
						type: "", //这里的种类支持很多种：基本视频格式、直播、流媒体等，具体可以参看git网址项目
						src: "" //url地址
					}],
					poster: "", //你的封面地址
					// width: document.documentElement.clientWidth, //播放器宽度
					notSupportedMessage: '此视频暂无法播放，请稍后再试', //允许覆盖Video.js无法播放媒体源时显示的默认信息。
					controlBar: {
						timeDivider: true, //当前时间和持续时间的分隔符
						durationDisplay: true, //显示持续时间
						remainingTimeDisplay: false, //是否显示剩余时间功能
						fullscreenToggle: true //全屏按钮
					}
				},
				singer: '',
				MVTitle: '',
				createDate: '',
				count: 0,
				tuijianMVList:[],
			};
		},
		computed: {
			id: function() {
				return this.$route.query.id;
			}
		},
		methods: {
			//初始化
			init: function(id) {
				var _this = this;
				this.$axios.get("https://autumnfish.cn/mv/detail?mvid=" + id).then(response => {
					var data = response.data.data;
					_this.MVTitle = data.name;
					_this.singer = data.artistName;
					_this.createDate = data.publishTime;
					_this.count = data.playCount;
					_this.playerOptions.poster = data.cover;
				});
			},
			//获取播放地址
			getMVUrl: function(id) {
				var _this = this;
				this.$axios.get("https://autumnfish.cn/mv/url?id=" + id).then(response => {
					var data = response.data.data;
					_this.playerOptions.sources[0].src = data.url;
				});
			},
			
			//推荐mv
			tiuJian:function(id){
				var _this=this;
				this.tuijianMVList=[];
				this.$axios.get("https://autumnfish.cn/simi/mv?mvid="+id).then(response=>{
					var data=response.data.mvs;
					data.forEach(function(item){
						_this.tuijianMVList.push({
							name:item.name,
							pic:item.cover,
							singer:item.artistName,
							id:item.id,
						});
					})
				});
			},
			//推荐mv点击事件
			tuijianClick:function(id){
				this.init(id);
				this.getMVUrl(id);
				this.tiuJian(id);
			},
		},
		beforeMount() {
			this.init(this.id);
			this.getMVUrl(this.id);
			this.tiuJian(this.id);
		},

	}
</script>

<style>
	#mvxiangqing {
		background-color: #fff;
		width: 80%;
		margin: 0 auto;
	}

	#mvxiangqing .title {
		width: 800px;
		margin: 0 auto;
		margin-top: 20px;
	}

	#mvxiangqing .name {
		font-size: 24px;
		color: #000000;
		margin-right: 15px;
	}

	#mvxiangqing .singer {
		font-size: 16px;
	}
	
	#mvxiangqing .content{
		width: 95%;
		margin: 15px auto;
		margin-left: 20px;
	}
	
	#mvxiangqing .content .vjs-custom-skin{
		margin:20px;
		margin: 0 auto;
	}
	#mvxiangqing .content .xq h3{
		display: inline-block;
		margin-top: 15px;
	}
	
	
	#mvxiangqing .tuijian ul {
		list-style: none;
		margin: 0 auto;
	}
	
	#mvxiangqing .tuijian ul li {
		margin-top: 10px;
	}
	
	#mvxiangqing .tuijian li:hover {
		background-color: #d8dada;
	}
	
	
	#mvxiangqing .menu_li img{
		width: 65px;
		height: 65px;
	}
	
	#mvxiangqing .updateFrequency{
		color:#a2a2a2;
		font-size: 14px;
	}
	
	#mvxiangqing .tuijian{
		margin-top: 20px;
	}
	#mvxiangqing .tuijian li h3{
		width:100%;
		white-space: nowrap;
		overflow: hidden;
		text-overflow: ellipsis;
	}
	
</style>
