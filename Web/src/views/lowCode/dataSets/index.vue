<template>
	<div class="sys-pos-container">
		<el-card shadow="hover" :body-style="{ paddingBottom: '0' }">
			<el-form :model="state.queryParams" ref="queryForm" :inline="true">
				<el-form-item label="名称">
					<el-input v-model="state.queryParams.name" placeholder="请输入名称" clearable />
				</el-form-item>
				<!-- <el-form-item label="SQL">
					<el-input v-model="state.queryParams.code" placeholder="" clearable />
				</el-form-item> -->
				<el-form-item>
					<el-button-group>
						<el-button type="primary" icon="ele-Search" @click="handleQuery" v-auth="'sysPos:list'"> 查询
						</el-button>
						<el-button icon="ele-Refresh" @click="resetQuery"> 重置 </el-button>
					</el-button-group>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" icon="ele-Plus" @click="openAddPos" v-auth="'sysPos:add'"> 新增 </el-button>
				</el-form-item>
			</el-form>
		</el-card>

		<el-card class="full-table" shadow="hover" style="margin-top: 8px">
			<el-table :data="state.posData" style="width: 100%" v-loading="state.loading" border>
				<el-table-column type="index" label="序号" width="55" align="center" />
				<el-table-column prop="name" label="名称" align="center" show-overflow-tooltip />
				<el-table-column prop="sql" label="SQL" align="center" show-overflow-tooltip />
				<el-table-column prop="createTime" label="创建时间" align="center" show-overflow-tooltip />
				<el-table-column prop="description" label="备注" header-align="center" show-overflow-tooltip />
				<el-table-column label="操作" width="140" fixed="right" align="center" show-overflow-tooltip>
					<template #default="scope">
						<el-button icon="ele-Edit" size="small" text type="primary" @click="openEditPos(scope.row)"
							v-auth="'sysPos:update'"> 编辑 </el-button>
						<el-button icon="ele-Delete" size="small" text type="danger" @click="delPos(scope.row)"
							v-auth="'sysPos:delete'"> 删除 </el-button>
					</template>
				</el-table-column>
			</el-table>
			<div class="pages">
				<el-pagination background layout="prev, pager, next" :total="total" @current-change="currentChange"
					:page-size="limit" />
			</div>

		</el-card>

		<EditPos ref="editPosRef" :title="state.editPosTitle" @handleQuery="handleQuery" />
	</div>
</template>

<script lang="ts" setup name="sysPos">
import { onMounted, reactive, ref, getCurrentInstance } from 'vue';
import { ElMessageBox, ElMessage } from 'element-plus';
import EditPos from '/@/views/lowCode/dataSets/component/editPos.vue';
const { proxy }: any = getCurrentInstance();

const editPosRef = ref<InstanceType<typeof EditPos>>();
const state = reactive({
	loading: false,
	posData: [] ,
	queryParams: {
		name: undefined,
	},
	editPosTitle: '',
});

onMounted(async () => {
	handleQuery();
});
// 每页20条数据
const limit = ref(20)
// 当前页码
const newPage = ref(1)
// 总条数
const total = ref(100)
// 触发分页
const currentChange = (val: number) => {
	newPage.value = val
	handleQuery()
}
// 查询操作
const handleQuery = async () => {
	state.loading = true;
	proxy.$get(`/api/DataSet/sql/pageList?page=${newPage.value}&limit=${limit.value}`).then((res: any) => {
		if (res.data.data) {
			// console.log(res, '422222222');
			state.posData = res.data.data ?? [];
		}
	});

	state.loading = false;
};

// 重置操作
const resetQuery = () => {
	state.queryParams.name = undefined;
	handleQuery();
};

// 打开新增页面
const openAddPos = () => {
	state.editPosTitle = '添加dataSets';
	editPosRef.value?.openDialog({ status: 1, orderNo: 100 });
};

// 打开编辑页面
const openEditPos = (row: any) => {
	state.editPosTitle = '编辑dataSets';
	editPosRef.value?.openDialog(row);
};

// 删除
const delPos = (row: any) => {
	ElMessageBox.confirm(`确定删除该条记录：【${row.name}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			proxy.$delete(`/api/DataSet/sql/${row.id}`).then((res: any) => {
				if (res.data.code == 200) {
					handleQuery();
					ElMessage.success('删除成功');
				}
			});

		})
		.catch(() => { });
};
</script>
