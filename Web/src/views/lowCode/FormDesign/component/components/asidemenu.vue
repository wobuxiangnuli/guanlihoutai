<template>
	<div>
		<div v-for="group in settings" :key="group.groupName">
			<h4>{{ group.groupName }}</h4>
			<el-card v-for="item in group.items" :key="item.title" class="box-card" :class="{ 'selected': selectedCard === item.title }" @click="switchComponent(item.component,item.title)">
				<div slot="header" class="clearfix">
					<span>{{ item.title }}</span>
				</div>
				<div>
					<p>{{ item.description }}</p>
				</div>
			</el-card>
		</div>
	</div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import  BusinessRules  from "./businessrules.vue";

const emit = defineEmits(['confirm']); 
const selectedCard = ref(''); // 默认没有选中的卡片

const settings = [
	{
		groupName: '基础设置',
		items: [
			{
				title: '基础选项',
				description: '设置表单的基础功能选项',
				component: 'BaseOptionsComponent',
			},
			{
				title: '标签名称',
				description: '设置表单的标签名称、文字颜色、字体大小',
				component: 'LabelOptionsComponent',
			},
			{
				title: '小部件',
				description: '设置表单的小部件功能和属性选项',
				component: 'WidgetOptionsComponent',
			},
		],
	},
	{
		groupName: '高级设置',
		items: [
			{
				title: '业务规则',
				description: '编写表单的逻辑，实现字段的动态显示和隐藏',
				component: BusinessRules,
			},
			{
				title: '自定义动作',
				description: '自定义表单的提交动作和处理逻辑',
				component: 'CustomActionsComponent',
			},
			{
				title: '打印模板',
				description: '自定义表单的打印样式',
				component: 'PrintTemplateComponent',
			},
			{
				title: '高级加密',
				description: '自定义工作表的数据加密和安全设置',
				component: 'AdvancedEncryptionComponent',
			},
		],
	},
];

const switchComponent = (componentName: string,cardTitle:string) => {
    selectedCard.value = cardTitle;
	emit('confirm',componentName)
   // 设置选中的卡片名
};
</script>

<style scoped>
h4{
    margin: 10px 0 20px 20px;
}
.selected {
    background: #f2f9ff;
    box-shadow: inset 0 0 0 1px #2196f3 !important;
    color: #2196f3;
}
.box-card {
	width: 90%;
    margin: 0 auto;
	cursor: pointer;
    border: 0px;
    box-shadow: none;
    border-bottom: 1px solid rgb(224, 224, 224);
   
} 
p{
        color: #aaa;
        font-size: 12px;
    }

</style>
