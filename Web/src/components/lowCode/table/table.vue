<template>
  <div class="sys-role-container">
    <el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
      <el-form :model="state.paramDtos" ref="queryForm" :inline="true">
        <el-form-item v-for="(item,index) in state.paramDtos" :key="index" :label="item.displayName">
          <el-input v-if="item.requestType===0" v-model="item.value" :placeholder="item.displayName" clearable />
          <el-select v-if="item.requestType===1" v-model="item.value" :placeholder="item.displayName" clearable>
            <el-option v-for="data in item.selectListItems" :key="data.value" :label="data.text" :value="data.value" />
          </el-select>
          <el-date-picker v-if="item.requestType===2" v-model="item.value" type="daterange" range-separator="-" format="YYYY/MM/DD" value-format="YYYY/MM/DD" clearable />
        </el-form-item>
        <el-form-item>
          <el-button-group>
            <el-button type="primary" icon="ele-Search" @click="handleQuery"> 查询 </el-button>
            <!-- <el-button icon="ele-Refresh" @click="resetQuery"> 重置 </el-button> -->
          </el-button-group>
        </el-form-item>
      </el-form>
    </el-card>
    <el-card class="full-table" shadow="hover" style="margin-top: 8px">
      <el-table :data="state.data" style="width: 100%" v-loading="state.loading" border>
        <!-- <el-table-column type="index" label="序号" width="55" align="center" fixed /> -->
        <el-table-column v-for="(item,index) in state.columnDtos" :key="index" :prop="item.name" :label="item.value" align="center" show-overflow-tooltip />
      </el-table>
      <el-pagination v-model:currentPage="state.tableParams.page" v-model:page-size="state.tableParams.pageSize" :total="state.tableParams.total" :page-sizes="[10, 20, 50, 100]" small background @size-change="handleSizeChange" @current-change="handleCurrentChange" layout="total, sizes, prev, pager, next, jumper" />
    </el-card>
  </div>
</template>

<script lang="ts" setup name="lowCodeTable">
import { onMounted, reactive, getCurrentInstance } from 'vue';
// import { ElMessageBox, ElMessage } from 'element-plus';
const { proxy }: any = getCurrentInstance();

// 定义接收的Props
const props = defineProps<{
	url: string;
	params: any;
}>();
const state = reactive({
	loading: false,
	data: [] as any,
	paramDtos: [] as any,
	columnDtos: [] as any,
	tableParams: {
		page: 1,
		pageSize: 20,
		total: 0 as any,
	},
});

onMounted(async () => {
	state.tableParams.page = 1;
	handleQuery();
	// console.log(props.url);
});

// 查询操作
const handleQuery = async () => {
	state.loading = true;
	// let params = Object.assign(state.paramDtos, state.tableParams);
	let params = state.tableParams as any;
	params.paramDtos = state.paramDtos;
	// console.log(params);
	proxy.$post(props.url, params).then((res: any) => {
		if (res.data.result) {
			// console.log(res.data.result);
			let result = res.data.result;
			state.columnDtos = result.columnDtos;
			state.data = result.dataDtos.items;
			state.tableParams.total = result.dataDtos.total;
			state.paramDtos = result.requestDtos;
		}
		state.loading = false;
	});
};

// 重置操作
// const resetQuery = () => {
// 	handleQuery();
// };

// 改变页面容量
const handleSizeChange = (val: number) => {
	state.tableParams.pageSize = val;
	handleQuery();
};

// 改变页码序号
const handleCurrentChange = (val: number) => {
	state.tableParams.page = val;
	handleQuery();
};
</script>
