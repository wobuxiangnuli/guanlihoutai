<template>
    <div class="pageIndex">
        <div class="hader">
            <el-button type="success" v-if="addOreSave" @click="Design">Design</el-button>
            <el-button type="danger" v-if="!addOreSave">Add Widget</el-button>
            <el-button type="primary" v-if="!addOreSave">Save Dashboard</el-button>
            <el-button v-if="!addOreSave" @click="Cancel">Cancel</el-button>
        </div>
        <grid-layout v-model:layout="state.layouts" :col-num="12" :row-height="30" :is-draggable="isdraggable"
            :is-resizable="isresizable" :is-mirrored="false" :vertical-compact="true" :margin="[10, 10]"
            :use-css-transforms="true" :static="false">
            <grid-item v-for="item in state.layouts" :x="item.x" :y="item.y" :w="item.w" :h="item.h" :i="item.i"
                :key="item.i">
                <div class="w100 h100 flex" style="background-color: brown;">
                    <!-- <span class="flex-margin font14">{{ item.i }}</span> -->
                    <el-table :data="tableData" style="width: 100%">
                        <el-table-column prop="date" label="Date" width="180" />
                        <el-table-column prop="name" label="Name" width="180" />
                        <el-table-column prop="address" label="Address" />
                    </el-table>
                </div>
            </grid-item>
        </grid-layout>
    </div>
</template>

<script setup lang="ts" name="index">
import { reactive, onMounted, ref } from 'vue';
const tableData = [
  {
    date: '2016-05-03',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
  {
    date: '2016-05-02',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
  {
    date: '2016-05-04',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
  {
    date: '2016-05-01',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
]
let addOreSave = ref(true)
// 页面加载时
onMounted(() => {
});
// 启动操作
const Design = () => {
    addOreSave.value = false
}
// 取消操作
const Cancel = () => {
    addOreSave.value = true
}
//是否禁用拖拽
let isdraggable = ref(true)
// 标识栅格元素是否可调整大小
let isresizable = ref(true)
// 定义变量内容
const state = reactive({
    layouts: [
        { x: 0, y: 0, w: 2, h: 12, i: '0' }
    ],
});
</script>
<style scoped lang="scss">
.pageIndex {
    width: 100%;
    height: 100%;
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

