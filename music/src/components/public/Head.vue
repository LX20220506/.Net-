<template>
	<div style="background-color: #FFFFFF;">
	<div id="head">
		<a-row type="flex" justify="space-around" align="middle">
			<a-col :span="6">
				<img src="../../img/login.png" class="logo" alt="">
				<div class="title">
					<span >韵雅音乐</span>
				</div>
			</a-col>
			
			<!-- 搜索框 -->
			<a-col :span="6">
				<div class="global-search-wrapper" style="width: 300px">
					<a-auto-complete class="global-search" size="large" style="width: 100%" placeholder="搜索音乐、MV、歌单"
						option-label-prop="title" @select="onSelect" @search="handleSearch">
						<template slot="dataSource">
							<a-select-option v-for="item in dataSource" :key="item.category" :title="item.category">
								Found {{ item.query }} on
								<a :href="`https://s.taobao.com/search?q=${item.query}`" target="_blank"
									rel="noopener noreferrer">
									{{ item.category }}
								</a>
								<span className="global-search-item-count">{{ item.count }} results</span>
							</a-select-option>
						</template>
						<a-input>
							<a-button slot="suffix" style="margin-right: -12px" class="search-btn" size="large"
								type="primary">
								<a-icon type="search" />
							</a-button>
						</a-input>
					</a-auto-complete>
				</div>
			</a-col>

			<a-col :span="6">
				<a-button type="link">客服中心 </a-button> | 
				<a-button type="link"> 关于我们 </a-button> | 
				<a-button type="link"> 会员中心</a-button>
			</a-col>
			<a-col :span="2">
				<div>
					<a-button type="primary" size="large">
					      登 录
					    </a-button>
						
				</div>
			</a-col>
		</a-row>
	</div>
	</div>
</template>

<script>
	export default {
		data() {
		    return {
		      dataSource: [],
		    };
		  },
		  methods: {
		    onSelect(value) {
		      console.log('onSelect', value);
		    },
		
		    handleSearch(value) {
		      this.dataSource = value ? this.searchResult(value) : [];
		    },
		
		    getRandomInt(max, min = 0) {
		      return Math.floor(Math.random() * (max - min + 1)) + min;
		    },
		
		    searchResult(query) {
		      return new Array(this.getRandomInt(5))
		        .join('.')
		        .split('.')
		        .map((item, idx) => ({
		          query,
		          category: `${query}${idx}`,
		          count: this.getRandomInt(200, 100),
		        }));
		    },
		  },
	}
</script>

<style>
	#head {
		width: 80%;
		margin: 0 auto;
		border-bottom: 1px solid #E1E1E1;
	}

	#head .title {
		text-align: center;
		padding-left: -50px;
		float: left;
		line-height: 100px;
	}

	#head .title  span {
		font-size: 28px;
		font-weight: bold;
		color: #067cb7;
	}

	#head .logo {
		display: block;
		width: 130px;
		height: 100px;
		float: left;
	}


	#head .global-search-wrapper {
		padding-right: 50px;
	}

	#head .global-search {
		width: 100%;
	}

	.global-search.ant-select-auto-complete .ant-select-selection--single {
		margin-right: -46px;
	}

	.global-search.ant-select-auto-complete .ant-input-affix-wrapper .ant-input:not(:last-child) {
		padding-right: 62px;
	}

	.global-search.ant-select-auto-complete .ant-input-affix-wrapper .ant-input-suffix button {
		border-top-left-radius: 0;
		border-bottom-left-radius: 0;
	}

	.global-search-item {
		display: flex;
	}

	.global-search-item-desc {
		flex: auto;
		text-overflow: ellipsis;
		overflow: hidden;
	}

	.global-search-item-count {
		flex: none;
	}
</style>
