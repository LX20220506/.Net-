<template>
  <div id="app">
	  <index></index>
  </div>
</template>

<script>
	import Index from "./components/Index.vue";

export default {
  name: "App",
  components: {
	Index
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  background-color: #F1F3F4;
}
</style>
