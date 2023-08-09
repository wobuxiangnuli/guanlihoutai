<template>
  <div class="sys-region-container">
    <el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="80%" style="min-height:600px;">
      <template #header>
        <div style="color: #fff">
          <el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Setting />
          </el-icon>
          <span> {{ props.title }} </span>
        </div>
      </template>
      <!-- <el-card header="基础配置">
        <el-tabs tab-position="left" style="height: 200px" class="demo-tabs">
          <el-tab-pane label="User">User</el-tab-pane>
          <el-tab-pane label="Config">Config</el-tab-pane>
          <el-tab-pane label="Role">Role</el-tab-pane>
          <el-tab-pane label="Task">Task</el-tab-pane>
        </el-tabs>
      </el-card> -->
      <!-- <el-card header="数据源设置">

      </el-card> -->
      <div class="sidel">
        <el-tabs tab-position="left"  class="demo-tabs">
          <el-tab-pane label="基础配置">

            
          </el-tab-pane>
          <el-tab-pane label="数据源设置">数据源设置</el-tab-pane>
          <el-tab-pane label="排列方式">排列方式</el-tab-pane>
          <el-tab-pane label="快速筛选">快速筛选</el-tab-pane>
          <el-tab-pane label="行颜色显示">行颜色显示</el-tab-pane>
          <el-tab-pane label="操作">操作</el-tab-pane>

        </el-tabs>
      </div>
      <template #footer></template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup name="tableSetting">
import { reactive, ref } from 'vue';

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
