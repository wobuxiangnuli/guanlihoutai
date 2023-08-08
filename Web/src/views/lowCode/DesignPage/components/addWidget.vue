<template>
  <div class="sys-region-container">
    <el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="80%" style="min-height:600px;">
      <template #header>
        <div style="color: #fff">
          <el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Plus /> </el-icon>
          <span> {{ props.title }} </span>
        </div>
      </template>
      <el-row>
        <el-col :span="4" v-for="(item,index) in state.componentList" :key="index">
          <el-card class="box-card" @click="selectCom(item)">
            <div><img :src="item.imgSrc" /></div>
            <div>{{ item.name }}</div>
            <div>{{ item.describe }}</div>
          </el-card>
        </el-col>
      </el-row>
      <template #footer></template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="addWidget">
import { reactive } from 'vue';
import tableImg from '/@/assets/components/table.png';

const props = defineProps({
	title: String,
});
const emits = defineEmits(['handleQuery']);
const state = reactive({
	isShowDialog: false as boolean,
	componentList: [
		{
			imgSrc: tableImg as any,
			name: '表格' as string,
			type: 'table' as string,
			describe: '点击之后会在页面上绘制一个表格' as string,
		},
	] as Array<any>,
});

// 打开弹窗
const openDialog = () => {
	state.isShowDialog = true;
};

// 关闭弹窗
const selectCom = (item: any) => {
	emits('handleQuery', item.type);
	state.isShowDialog = false;
};

// 导出对象
defineExpose({ openDialog });
</script>
