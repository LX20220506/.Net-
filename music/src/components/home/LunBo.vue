<template>
	<a-carousel effect="fade" autoplay>
		<div v-for="item in imglist" :key="item.id">
			<img :src="item" alt="">
		</div>
	</a-carousel>
</template>

<script>
	export default {
		data() {
			return {
				imglist: [],
			}
		},
		methods: {
			InitData: function() {
				var _this = this;
				this.$axios.get("https://autumnfish.cn/banner").then(function(obj) {
					var data = obj.data.banners;
					var list = [];
					for (var i = 0; i < data.length; i++) {
						if (data[i].targetType == 1) {
							list.push(data[i].imageUrl);
						}
					}
					_this.imglist = list;
				}).catch(function(err) {
					console.log(err);
				})
			}
		},
		beforeMount: function() {
			this.InitData();
		},
	}
</script>

<style scoped>
	/* For demo */
	.ant-carousel :deep(.slick-slide) {
		text-align: center;
		height: 160px;
		line-height: 160px;
		background: #364d79;
		overflow: hidden;
	}

	.ant-carousel :deep(.slick-slide h3) {
		color: #fff;
	}

	img {
		width: 80%;
		height: 400px;
		margin: 0 auto;
	}
</style>
