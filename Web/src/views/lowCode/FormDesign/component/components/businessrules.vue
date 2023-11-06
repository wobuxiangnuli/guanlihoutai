<template>
    <div>
					<el-row>
						<el-col :span="20">
							<span class="font">业务规则</span><br />
							<i>添加业务规则可以规范化表单数据录入。当满足条件时，显示、隐藏、更改字段属性或提示错误。</i>
						</el-col>
						<el-col :span="4">
							<el-button class="btn" @click="drawer"  type="primary"><span>+</span>添加规则</el-button>
							<Drawer ref="chouti"></Drawer>
						</el-col>
					</el-row>
					<el-empty  :image-size="300" />
				</div>
</template>
<script lang="ts" setup>
import { ref } from 'vue';
import Drawer from './drawer.vue'
const chouti = ref();
const drawer = () => {
	chouti.value.open()
};
</script>
<style scoped lang="scss">
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
}
</style>