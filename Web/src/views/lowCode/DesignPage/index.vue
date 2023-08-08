<template>
  <div class="pageIndex">
    <div class="hader">
      <el-button type="success" v-if="addOreSave" @click="Design">Design</el-button>
      <el-button type="danger" v-if="!addOreSave" @click="addWidget">Add Widget</el-button>
      <el-button type="primary" v-if="!addOreSave">Save Dashboard</el-button>
      <el-button v-if="!addOreSave" @click="Cancel">Cancel</el-button>
    </div>
    <grid-layout v-model:layout="state.layouts" :col-num="12" :row-height="30" :is-draggable="isdraggable" :is-resizable="isresizable" :is-mirrored="false" :vertical-compact="true" :margin="[10, 10]" :use-css-transforms="true" :static="false">
      <grid-item v-if="state.isTable" x="0" y="0" w="8" h="12" i="0" :isDraggable="true" :isResizable="true" class="gridItem">
        <el-card shadow="hover">
          <Table :url="url" :params="params"></Table>
        </el-card>
      </grid-item>
    </grid-layout>
    <AddWidget ref="addWidgetRef" title="添加组件" @handleQuery="handleQuery" />
    <TableSetting ref="tableSettingRef" title="表格配置" @handleQuery="handleQueryTable" />
  </div>
</template>
<script setup lang="ts" name="index">
import { reactive, onMounted, ref, defineAsyncComponent } from 'vue';
import AddWidget from './components/addWidget.vue';
import TableSetting from './components/tableSetting.vue';

// 定义变量内容
const state = reactive({
	layouts: [{ x: 0, y: 0, w: 8, h: 12, i: '0', isDraggable: true, isResizable: true }],
	isTable: false as boolean,
});
const addWidgetRef = ref<InstanceType<typeof AddWidget>>();
const tableSettingRef = ref<InstanceType<typeof TableSetting>>();
const Table = defineAsyncComponent(() => import('/@/components/lowCode/table/table.vue'));
let addOreSave = ref(true);
const url = ref('/api/Component/table/data' as string);
const params = ref({} as any);
// 页面加载时
onMounted(() => {});
// 启动操作
const Design = () => {
	addOreSave.value = false;
};
// 取消操作
const Cancel = () => {
	addOreSave.value = true;
};
//是否禁用拖拽
let isdraggable = ref(true);
// 标识栅格元素是否可调整大小
let isresizable = ref(true);
const addWidget = () => {
	addWidgetRef.value?.openDialog();
};
const handleQuery = (type: string) => {
	switch (type) {
		case 'table':
			// state.isTable = true;
			tableSettingRef.value?.openDialog();
			break;
	}
};
</script>
<style scoped lang="scss">
.gridItem {
	overflow: scroll;
}
.pageIndex {
	width: 100%;
	height: 100%;
	// border: red solid;
}

.hader {
	padding: 0 20px;
	box-sizing: border-box;
	display: flex;
	justify-content: flex-end;
	align-items: center;
	width: 100%;
	height: 45px;
	background-color: #fff;
}

.grid-layout-container {
	.vue-grid-item {
		background: var(--el-color-primary);
		color: var(--el-color-white);
	}
}
</style>

