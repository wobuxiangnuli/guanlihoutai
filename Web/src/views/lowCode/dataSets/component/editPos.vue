<template>
	<div class="sys-pos-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="70%">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Edit />
					</el-icon>
					<span> {{ props.title }} </span>
				</div>
			</template>
			<el-form :model="state.ruleForm" ref="ruleFormRef" label-width="auto">
				<el-row :gutter="35">
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="名称" prop="name"
							:rules="[{ required: true, message: '名称不能为空', trigger: 'blur' }]">
							<el-input v-model="state.ruleForm.name" placeholder="请输入名称" clearable />
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="SQL" prop="sql"
							:rules="[{ required: true, message: 'SQL语句不能为空', trigger: 'blur' }]">
							<el-input v-model="state.ruleForm.sql" placeholder="请输入SQL语句" rows="10" clearable
								type="textarea" />
						</el-form-item>
					</el-col>

					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="备注">
							<el-input v-model="state.ruleForm.description" placeholder="请输入备注内容" rows="7" clearable
								type="textarea" />
						</el-form-item>
					</el-col>
				</el-row>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="cancel">取 消</el-button>
					<el-button type="primary" @click="submitapi">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script lang="ts" setup name="sysEditPos">
import { reactive, ref, getCurrentInstance } from 'vue';
import { ElMessage } from 'element-plus';

const { proxy }: any = getCurrentInstance();

const props = defineProps({
	title: String,
});
const emits = defineEmits(['handleQuery']);
const ruleFormRef = ref();
const state = reactive({
	isShowDialog: false,
	ruleForm: {
		id: null,
		name: '',
		sql: '',
		description: ''
	},
});

// 打开弹窗
const openDialog = (row: any) => {
	state.ruleForm.id = row.id
	state.ruleForm.name = row.name
	state.ruleForm.sql = row.sql
	state.ruleForm.description = row.description
	console.log(state.ruleForm, 'resres');

	// state.ruleForm = {
	// 	...JSON.parse(JSON.stringify(row))
	// };
	console.log();

	state.isShowDialog = true;
};

// 关闭弹窗
const closeDialog = () => {
	emits('handleQuery');
	state.isShowDialog = false;
};

// 取消
const cancel = () => {
	state.isShowDialog = false;
};

// 提交
const submitapi = () => {
	ruleFormRef.value.validate(async (valid: boolean) => {
		if (!valid) return;

		if (state.ruleForm.id != undefined && state.ruleForm.id > 0) {

			// 编辑
			proxy.$put(`/api/DataSet/sql/updata`, state.ruleForm).then((res: any) => {
				if (res.data.code == 200) {
					ElMessage.success('编辑成功');
					closeDialog();
					const $emit = defineEmits(['handleQuery'])
					$emit('handleQuery')
				}
			});

		} else {
			// 添加
			proxy.$post(`/api/DataSet/sql/add`, state.ruleForm).then((res: any) => {
				if (res.data.code == 200) {
					ElMessage.success('添加成功');
					closeDialog();
					const $emit = defineEmits(['handleQuery'])
					$emit('handleQuery')
				}
			});

		}
	});
};

// 导出对象
defineExpose({ openDialog });
</script>
