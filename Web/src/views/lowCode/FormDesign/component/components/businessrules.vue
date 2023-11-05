<template>
	<div>
		<el-row>
			<el-col :span="20">
				<span class="font">业务规则</span><br />
				<i>添加业务规则可以规范化表单数据录入。当满足条件时，显示、隐藏、更改字段属性或提示错误。</i>
			</el-col>
			<el-col :span="4">
				<el-button class="btn" @click="drawer" type="primary"><span>+</span>添加规则</el-button>
				<Drawer ref="chouti"></Drawer>
			</el-col>
		</el-row>
		<div class="rules" v-for="item, index in state" :key="item.value">
			<h3>规则 {{ index + 1 }}</h3>
			<div v-for="val in item" :key="val.value">
				<p style="display: inline-block; color: #9e9e9e; margin-right: 5px;">当满足条件</p>
				<div style="display: inline-block;" v-for="el, index in val.rule" :key="el.value"> <span v-if="index != 0" style="margin: 0 5px 0 5px;color: #9e9e9e;">{{el.lianjieci }}</span><span class="content">{{ el.name }}</span><span class="content">{{ el.compare}}</span><span class="content">{{ el.value }}</span></div>
				<p style="display: inline-block; color: #9e9e9e;margin-left: 5px;">时</p>
				<div class="action" v-for="ac in val.action"><p style="display: inline-block; color: #9e9e9e;margin-right: 5px;margin-left: 42px;">{{ ac.actions }}</p><span class="content">{{ ac.executor }}</span></div>
			</div>

		</div>
	</div>
</template>
<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import Drawer from './drawer.vue'
import { useFormMessage } from '/@/stores/formMessage'
const formMessage = useFormMessage();
const state = ref<any[]>([])
const chouti = ref();
const drawer = () => {
	chouti.value.open()



};
state.value = formMessage.$state.rules
console.log(state.value);




</script>
<style scoped lang="scss">
.action{
	margin-top: 10px;
}
.content {
	margin: 0 2px 0 2px;
}
h3{
	margin-top: -30px;
	margin-bottom: 20px;
}
.rules {
	border: 1px solid rgb(218, 218, 218);
	border-radius: 6px;
	width: 95%;
	margin: 0 auto;
	height: 104px;
	padding: 20px 0 0 20px;
	margin-top: 30px;
}

.font {
	font-size: 17px;
	font-weight: 700;
	display: inline-block;
	margin-bottom: 5px;
}

.btn {
	font-size: 18px;

	span {
		margin-right: 5px;
	}
}

i {
	color: rgb(173, 173, 173);
}</style>