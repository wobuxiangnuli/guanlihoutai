<template>
	<div class="common-layout">
		<el-container>
			<el-aside class="aside" width="310px">
				<AsideMenu @confirm="handleComponent"></AsideMenu>
			</el-aside>
			<el-main>
			<component :is="component"></component>
			</el-main>
		</el-container>
	</div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import AsideMenu from './components/asidemenu.vue'

const component = ref()
const handleComponent=(value:string)=>{
	component.value = value
}
</script>

<style scoped lang="scss">

.aside{
	background-color:rgb(245, 245, 245);
	height: 96vh;
}


</style>
