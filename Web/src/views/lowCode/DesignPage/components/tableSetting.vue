<template>
  <div class="sys-region-container">
    <el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="80%" style="min-height:600px;">
      <template #header>
        <div style="color: #fff">
          <el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Setting /> </el-icon>
          <span> {{ props.title }} </span>
        </div>
      </template>
      <el-card header="基础配置">

      </el-card>
      <el-card header="数据源设置">

      </el-card>
      <template #footer></template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="tableSetting">
import { reactive } from 'vue';

const props = defineProps({
	title: String,
});
const emits = defineEmits(['handleQuery']);
const state = reactive({
	isShowDialog: false as boolean,
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
