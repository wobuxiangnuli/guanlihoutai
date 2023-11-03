<template>
  <el-dialog title="提示" v-model="dialogVisible" @close="closeDialog">
    <span>是否保存修改的内容？</span>
    <span  class="dialog-footer">
      <el-button @click="closeDialog">取 消</el-button>
      <el-button type="primary" @click="confirmChanges">确 定</el-button>
    </span>
  </el-dialog>
</template>
<script lang="ts" setup>
import { ref, } from 'vue';
const emit = defineEmits(['confirm']);
const dialogVisible = ref(false);
const open = () =>{
    dialogVisible.value = true   
}
const closeDialog = () => {
  dialogVisible.value = false;
}

const confirmChanges = () => {
  closeDialog();
  emit('confirm',true);
}
defineExpose(
    {open}
)
</script>
<style lang="scss" scoped>
</style>
