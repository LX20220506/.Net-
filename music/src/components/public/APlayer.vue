<template>
  <div id="app">
    <aplayer :audio="audio"  :lrcType="1" fixed  />
  </div>
</template>

<script>
export default {
  data() {
    return {
      audio: []
    };
  },
  props:{
  	SongId:Number
  },
  mounted() {
  	this.Init();
  },
  methods: {
  	Init: function() {
  		var _this = this;
  		//歌单信息
  		this.$axios.get("https://autumnfish.cn/playlist/detail?id=5001").then((obj) => {
  			var data = obj.data.playlist.tracks;
			
  			//这里使用forEach遍历
  			//在使用for遍历时  会造成逻辑混乱
  			data.forEach(function(item) {
  				var id = item.id;
  				//getSrc(id)方法返回的是promise对象  直接取值取不到  使用钩子函数可以取到值 具体如下方操作
  				 _this.getSrc(id).then(da => {
  					var title = item.name; //名称
  					var pic = item.al.picUrl; //图片
  				 	var name = item.ar[0].name; //歌手名称
					var id=item.id;
					
					_this.getLrc(id).then(response=>{
						if(da){
							_this.audio.push({
								name: title,
								artist: name,
								url: da,
								cover: pic,
								lrc:response
							});
						}
					})
  				 })
  			})
  		});
  	},
  	//获取歌曲的播放地址  该方法返回的是promise对象
  	getSrc: function(id) {
  		return this.$axios.get("https://autumnfish.cn/song/url?id=" + id).then(response => {
  			//console.log(obj.data.data[0]);
  			return response.data.data[0].url;
  		});
  	},
	//获取歌词
	getLrc:function(id){
		return this.$axios.get("https://autumnfish.cn//lyric?id="+id).then(response=>{
			return response.data.lrc.lyric;
		});
	},
  	//播放音乐
  	getSong:function(){
  		var _this=this;
  		this.$axios.get("https://autumnfish.cn/song/detail?ids="+_this.SongId).then(response=>{
  			var obj=_this.getSrc(_this.SongId);
  			obj.then(url=>{
  				var data=response.data.songs[0];
				var id=data.id;
				_this.getLrc(id).then(response=>{
						//将数据添加到第一个
						_this.audio.unshift({
							name:data.name,
							cover:data.al.picUrl,
							artist:data.ar[0].name,
							url:url,
							lrc:response
						})
				})
				
  				
  			})
  		});
  		_this.$refs.aud.switch(0);//切换歌曲
  	}
  },
  watch:{
  	SongId:"getSong"
  }
};
</script>

<style>
	.aplayer-lrc-contents p{
		font-size: 18px;
	}
</style>