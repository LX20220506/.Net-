<template>
	<div id="songlist">
		<a-list item-layout="horizontal" :data-source="data">
		  <a-list-item slot="renderItem" slot-scope="item">
		    <a-list-item-meta
			:description="item.singer" class="singerlist" >
		      <a slot="title" href="javascript:;" @click="getSong(item.id)">{{ item.name }}</a>
		      <a-avatar slot="avatar" :src="item.pic" />
		    </a-list-item-meta>
			  <div  class="caozuo" >
				  <a-icon class="ico" type="caret-right" theme="filled" @click="getSong(item.id)" />
				  <a-icon class="ico" type="vertical-align-bottom" @click="xiazaiSong(item.id,item.name)" />
			  </div>
		  </a-list-item>
		</a-list>
	</div>
</template>
<script>
export default {
  data() {
    return {
      data:[]
    };
  },
  beforeMount() {
	var _this=this;
	var data= this.$axios.get("https://autumnfish.cn/personalized/newsong?limit=8").then(response=>{
		return response.data.result;
	});
	data.then(data=>{
		data.forEach(function(item){
			var singer='';
			item.song.artists.forEach(function(s){
				singer+=s.name+" ";
			});
			//console.log(singer);
			_this.data.push({
				name:item.name,
				pic:item.picUrl,
				id:item.id,
				singer:singer
			})
		})
	});
  },
  methods:{
	  getSong:function(id){
		  //通过$emit方法向父组件传值  
		  //第一个参数为定义事件 当父组件有使用这个事件时 就会将值传递过去
		  //第二个参数为要传递的值
		  this.$emit("GetSongId",id);
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
	  }
  }
};
</script>

<style>
	#songlist li:hover{
		background-color: #d8dada;
	}
	#songlist .ico{
		font-size: 30px;
		color: #0c8ed9;
		padding: 10px;
		padding-top: 0px;
	}
	
	#songlist .ant-list-item-meta-content{
		height: 30px;
		line-height: 30px;
	}
	#songlist .ant-list-item-meta-content h4{
		font-size: 16px;
	}
	#songlist .ant-avatar-image {
		margin-left: 10px;
	}
	#songlist .ant-avatar-image img{
		width: 100%;
		height: 100%;
	}
	
	#songlist .ant-list-item-meta-description{
		width:400px;
		white-space: nowrap;
		overflow: hidden;
		text-overflow: ellipsis;
	}
</style>
