<template>	
	<el-container>
		<el-header
			><div class="container-header">
				<el-row>
					<el-col :span="8">
						<el-page-header @back="goBack">
						
							<template #content>
								<span class="text-large font-600 mr-3"> 正在编辑表单：123 </span>
							</template>
						</el-page-header>
					</el-col>
					<el-col :span="8">
						<el-tabs  v-model="activeName" class="demo-tabs" @tab-click="handleClick">
							<el-tab-pane v-for="item in components" :label=item.label :name=item.name :key="item.label"></el-tab-pane>
						</el-tabs>
					</el-col>
					<el-col :span="8">
						<el-button>取消</el-button>
						<el-button type="primary">保存</el-button>
					</el-col>
				</el-row>
			</div></el-header
		>
		<el-main>
			<SaveDialog ref="savedialog" @confirm="handleaction"></SaveDialog>
				<component :is="DyComponents"></component>
		</el-main>
	</el-container>
</template>

<script setup lang="ts">

import { ref , shallowRef } from 'vue';
import rules from './component/rules.vue'
import editfield from './component/editfield.vue'
import SaveDialog from './component/components/SaveDialog.vue';
import PublicPublish from './component/publicpublish.vue'
import { useRouter,} from 'vue-router'
const components = [
  {label:'编辑字段', name: 'EditField', component: editfield },
  {label:'表单设置', name: 'FormSettings', component: rules },
  {label:'公开发布', name: 'PublicPublish', component: PublicPublish }
];
const DyComponents = shallowRef(editfield);
const activeName = ref('EditField');
const router = useRouter();
const savedialog = ref() 


const goBack = () => {
	savedialog.value.open()
}
const handleaction = (val:any )=>{
	if(val){
		router.push('/dashboard/home')
	}
}
const handleClick = (tab: any) => {
	components.forEach(item=>{
		if(item.name == tab.props.name){
			DyComponents.value = item.component
		}
	})	
};
</script>

<style lang="scss" scoped>
.el-header{
	width: 100vw;
	padding: 0;
	font-family: Helvetica Neue,Helvetica,Arial,PingFang SC,Hiragino Sans GB,Microsoft YaHei,WenQuanYi Micro Hei,sans-serif!important;
}
.container-header {
	height: 50px;
	background: var(--el-color-white);
	line-height: 50px;
	font-size: 20px;
	font-weight: bold;
	text-align: center;

	.mr-3 {
		line-height: 50px;
	}
	.demo-tabs {
		margin: 0 auto;
		width: 40%;
	}
	.el-button {
		font-size: 16px;
	}
}
.el-main{
	background: var(--el-color-white);
	height: 96vh;
	padding: 0;
}
</style>
