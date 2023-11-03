<template>
	<div class="sys-region-container">
		<el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="80%">
			<template #header>
				<div style="color: #fff">
					<el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Setting /> </el-icon>
					<span> {{ props.title }} </span>
				</div>
			</template>
			<div class="sidel" style="min-height: 600px">
				<el-form :model="state.tableModel" ref="tableModelRef" label-width="auto">
					<el-tabs tab-position="left" v-model="tabPosition">
						<el-tab-pane label="基础配置" name="0" style="height: 600px">
							<el-form-item label="名字" prop="name" :rules="[{ required: true, message: '名字不能为空', trigger: 'blur' }]">
								<el-input v-model="state.tableModel.name" />
							</el-form-item>
							<el-form-item label="描述" prop="describe">
								<el-input type="textarea" v-model="state.tableModel.describe" />
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="数据源设置" name="1" style="height: 600px; overflow-y: auto">
							<el-form-item label="DataSet" prop="dataSetId" :rules="[{ required: true, message: '请选择dataSet', trigger: 'blur' }]">
								<el-select
									filterable
									remote
									:loading="state.dataSetLoading"
									:remote-method="(value:string)=>remoteMethod(value,'0')"
									v-model="state.tableModel.dataSetId"
									@change="(value:string)=>dataSetSelect(value,'0')"
								>
									<el-option v-for="(d, index) in state.dataSetList" :key="index" :label="d.name" :value="d.id" />
								</el-select>
							</el-form-item>
							<el-form-item label="数据列设置" v-if="state.isChangeDataSet">
								<el-table :data="state.tableModel.dataSetData" row-key="id">
									<el-table-column prop="columnName" label="列名" align="center" />
									<el-table-column prop="displayName" label="显示名称" align="center">
										<template #default="scope">
											<el-input v-model="scope.row.displayName" />
										</template>
									</el-table-column>
									<el-table-column prop="align" label="对齐方式" align="center">
										<template #default="scope">
											<el-select v-model="scope.row.align">
												<el-option label="left" value="left" />
												<el-option label="center" value="center" />
												<el-option label="right" value="right" />
											</el-select>
										</template>
									</el-table-column>
									<el-table-column prop="maxWidth" label="最大宽度" align="center">
										<template #default="scope">
											<el-input v-model="scope.row.maxWidth" />
										</template>
									</el-table-column>
									<el-table-column prop="display" label="显示隐藏" align="center">
										<template #default="scope">
											<el-switch v-model="scope.row.display" />
										</template>
									</el-table-column>
								</el-table>
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="排序方式" name="2" style="height: 600px; overflow-y: auto">
							<el-form-item label="" v-if="state.isChangeDataSet">
								<div style="width: 100%; line-height: 60px" v-for="(o, index) in state.tableModel.orderList" :key="index">
									<el-select v-model="o.name">
										<el-option v-for="(d, index) in state.tableModel.dataSetData" :key="index" :label="d.displayName" :value="d.columnName" />
									</el-select>
									<el-select v-model="o.order">
										<el-option label="升序" value="asc" />
										<el-option label="降序" value="desc" />
									</el-select>
									<el-button @click="removeOrder(index)" :icon="Delete" type="danger" circle />
									<el-button @click="addOrder" :icon="Plus" type="primary" circle />
								</div>
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="快速筛选" name="3" style="height: 600px; overflow-y: auto">
							<el-form-item label="">
								<div style="width: 100%; line-height: 60px" v-for="(o, index) in state.tableModel.filterList" :key="index">
									<el-select v-model="o.name" @change="(val:string)=>filterChange(val,index)">
										<el-option v-for="(d, index) in state.tableModel.dataSetData" :key="index" :label="d.displayName" :value="d.columnName" />
									</el-select>
									<el-select v-if="o.columnFieldDataType === 1 || o.columnFieldDataType === 3" v-model="o.filter">
										<el-option label="等于" value="0" />
									</el-select>
									<el-select v-if="o.columnFieldDataType === 0" v-model="o.filter">
										<el-option label="模糊搜索" value="0" />
										<el-option label="精准搜索" value="1" />
									</el-select>
									<el-select v-if="o.columnFieldDataType === 4" v-model="o.filter">
										<el-option label="等于" value="0" />
										<el-option label="在范围内 " value="1" />
									</el-select>
									<el-select v-if="o.columnFieldDataType === 2" multiple collapse-tags v-model="o.filterList">
										<el-option-group>
											<el-option label="昨天" value="0" />
											<el-option label="今天" value="1" />
											<el-option label="明天" value="2" />
										</el-option-group>
										<el-option-group>
											<el-option label="上周" value="3" />
											<el-option label="本周" value="4" />
											<el-option label="下周" value="5" />
										</el-option-group>
										<el-option-group>
											<el-option label="上月" value="6" />
											<el-option label="本月" value="7" />
											<el-option label="下月" value="8" />
										</el-option-group>
										<el-option-group>
											<el-option label="上季度" value="9" />
											<el-option label="本季度" value="10" />
											<el-option label="下季度" value="11" />
										</el-option-group>
										<el-option-group>
											<el-option label="去年" value="12" />
											<el-option label="今年" value="13" />
											<el-option label="明年" value="14" />
										</el-option-group>
										<el-option-group>
											<el-option label="过去7天" value="15" />
											<el-option label="过去14天" value="16" />
											<el-option label="过去30天" value="17" />
										</el-option-group>
										<el-option-group>
											<el-option label="将来7天" value="18" />
											<el-option label="将来14天" value="19" />
											<el-option label="将来30天" value="20" />
										</el-option-group>
									</el-select>
									<el-select v-if="o.columnFieldDataType !== 2" v-model="o.filterType">
										<el-option label="文本框" value="0" />
										<el-option label="下拉框" value="1" />
									</el-select>
									<span v-if="o.columnFieldDataType !== 2 && o.filterType === '1'">
										<el-select v-model="o.filterSelectType">
											<el-option label="固定选项" value="0" />
											<el-option label="指定数据源" value="1" />
										</el-select>
										<el-button type="primary" v-if="o.filterSelectType === '0'" @click="setGuDing(index)">设置选项</el-button>
										<el-button type="primary" v-if="o.filterSelectType === '1'" @click="setZhiDing(index)">指定数据源</el-button>
									</span>
									<el-button @click="removeFilter(index)" :icon="Delete" type="danger" circle />
								</div>
								<div style="width: 60%" v-if="state.isChangeDataSet">
									<el-button :icon="Plus" type="primary" style="width: 100%" @click="addFilter">选择字段</el-button>
								</div>
							</el-form-item>
						</el-tab-pane>
						<el-tab-pane label="行颜色显示" name="4" style="height: 600px; overflow-y: auto">
							<div v-if="state.isChangeDataSet" style="width: 60%">
								<el-card class="box-card" v-for="(row, index) in state.tableModel.rowColorList" :key="index">
									<template #header>
										<div class="card-header">
											<span>{{ row.name }}</span>
											<el-button
												class="button"
												@click="
													() => {
														state.tableModel.rowColorList.splice(index, 1);
													}
												"
												>删除</el-button
											>
										</div>
									</template>
									<div>当：</div>
									<div v-for="(rule, index) in row.ruleList" :key="index" class="text">
										{{ rule.name }} {{ convertStr(rule.columnFieldDataType, rule.filter) }}
										<span v-if="rule.columnFieldDataType === 1 || rule.columnFieldDataType === 4">
											<span v-if="parseInt(rule.filter) < 5">{{ rule.filterStr }}</span>
											<span v-if="parseInt(rule.filter) > 5 && parseInt(rule.filter) < 8">{{ rule.minNum }}至{{ rule.maxNum }}</span>
										</span>
										<span v-if="rule.columnFieldDataType === 0">
											<span v-if="parseInt(rule.filter) < 8">{{ rule.filterStr }}</span>
										</span>
										<span v-if="rule.columnFieldDataType === 3">
											<span>{{ rule.filterStr }}</span>
										</span>
										<span v-if="rule.columnFieldDataType === 2">
											<span>{{ convertDateStr(rule.filterStr) }}</span>
											<span v-if="rule.filterStr === '21'">：{{ rule.time }}</span>
											<span v-if="parseInt(rule.filter) > 5 && parseInt(rule.filter) < 8">{{ rule.rangeTime[0] }}至{{ rule.rangeTime[1] }}</span>
										</span>
									</div>
									<div>显示颜色：<el-button :color="row.color"></el-button></div>
								</el-card>
								<el-button :icon="Plus" type="primary" style="width: 100%" @click="addRowColorRule">添加规则</el-button>
							</div>
						</el-tab-pane>
						<el-tab-pane label="操作" name="5" style="height: 600px; overflow-y: auto">暂时搁置</el-tab-pane>
					</el-tabs>
				</el-form>
			</div>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="cancel">取 消</el-button>
					<el-button type="primary" @click="submit">确 定</el-button>
				</span>
			</template>
		</el-dialog>
		<RowColorRule ref="rowColorRuleRef" :title="state.rowColorRuleTitle" @handleQuery="handleQueryRule" />
		<el-dialog v-model="state.isGuDing">
			<template #header>
				<div style="color: #fff">设置固定选项</div>
			</template>
			<div class="sidel" style="height: 400px; overflow-y: auto">
				<div v-for="(g, index) in state.guDingList" :key="index" style="line-height: 50px">
					value:<el-input v-model="g.value" style="width: 30%" /> label:<el-input v-model="g.label" style="width: 30%" />
					<el-button @click="removeGuDing(index)" :icon="Delete" type="danger" circle />
				</div>
				<div>
					<el-button type="primary" style="width: 100%" :icon="Plus" @click="addGuDing">添加选项</el-button>
				</div>
			</div>
			<template #footer>
				<span class="dialog-footer">
					<el-button
						@click="
							() => {
								state.isGuDing = false;
							}
						"
						>取 消</el-button
					>
					<el-button type="primary" @click="saveGuDing">确 定</el-button>
				</span>
			</template>
		</el-dialog>
		<el-dialog v-model="state.isZhiDing">
			<template #header>
				<div style="color: #fff">指定数据源</div>
			</template>
			<div class="sidel" style="height: 400px; overflow-y: auto">
				<div>
					<el-select
						filterable
						remote
						:loading="state.dataSetLoading"
						:remote-method="(value:string)=>remoteMethod(value,'1')"
						v-model="state.zhiDingObj.dataSetId"
						@change="(value:string)=>dataSetSelect(value,'1')"
					>
						<el-option v-for="(d, index) in state.dataSetListZhiDing" :key="index" :label="d.name" :value="d.id" />
					</el-select>
				</div>
				<div style="line-height: 50px" v-if="state.zhiDingObj.dataSetId">
					value:<el-select v-model="state.zhiDingObj.value">
						<el-option v-for="(d, index) in state.dataSetData" :key="index" :label="d.displayName" :value="d.columnName" />
					</el-select>
					label:<el-select v-model="state.zhiDingObj.label">
						<el-option v-for="(d, index) in state.dataSetData" :key="index" :label="d.displayName" :value="d.columnName" />
					</el-select>
				</div>
			</div>
			<template #footer>
				<span class="dialog-footer">
					<el-button
						@click="
							() => {
								state.isZhiDing = false;
							}
						"
						>取 消</el-button
					>
					<el-button type="primary" @click="saveZhiDing">确 定</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script lang="ts" setup name="tableSetting">
import { reactive, ref, getCurrentInstance, onMounted } from 'vue';
import { ElMessage } from 'element-plus';
import { Delete, Plus } from '@element-plus/icons-vue';
import RowColorRule from './rowColorRule.vue';
const { proxy }: any = getCurrentInstance();
const rowColorRuleRef = ref<InstanceType<typeof RowColorRule>>();
const tableModelRef = ref();
const tabPosition = ref('0' as string);
const props = defineProps({
	title: String,
});
const emits = defineEmits(['handleQuery']);
const state = reactive({
	isZhiDing: false as boolean,
	zhiDingIndex: 0 as number,
	dataSetListZhiDing: [] as Array<any>,
	dataSetData: [] as Array<any>,
	zhiDingObj: {} as any,
	isGuDing: false as boolean,
	guDingIndex: 0 as number,
	guDingList: [] as Array<any>,
	isShowDialog: false as boolean,
	isChangeDataSet: false as boolean,
	dataSetList: [] as Array<any>,
	dataSetLoading: false as boolean,
	tableModel: {
		name: '' as string,
		describe: '' as string,
		dataSetId: '' as string,
		dataSetData: [] as Array<any>,
		orderList: [] as Array<any>, // 排序
		filterList: [] as Array<any>, // 快速筛选
		rowColorList: [] as Array<any>, // 行颜色
	} as any,
	rowColorRuleTitle: '' as string,
});
// 页面加载时
onMounted(() => {});
// 远程搜索数据源
const remoteMethod = (query: string, type: string) => {
	state.dataSetList = [];
	if (query) {
		state.dataSetLoading = true;
		proxy.$get(`/api/DataSet/sql/list/${query}`).then((res: any) => {
			setTimeout(() => {
				state.dataSetLoading = false;
				if (res.data.code === 200) {
					if (type === '0') {
						state.dataSetList = res.data.data;
					} else {
						state.dataSetListZhiDing = res.data.data;
					}
					// console.log(state.dataSetList);
				}
			}, 200);
		});
	}
};
// 数据源选择后触发
const dataSetSelect = (value: string, type: string) => {
	var arr = [];
	if (type === '0') {
		arr = state.dataSetList.filter((d) => d.id === value)[0].dataSetColumnInfos;
	} else {
		arr = state.dataSetListZhiDing.filter((d) => d.id === value)[0].dataSetColumnInfos;
	}
	if (arr && arr.length > 0) {
		if (type === '0') {
			state.tableModel.dataSetData = arr;
			state.isChangeDataSet = true;
			state.tableModel.orderList = [
				{
					name: state.tableModel.dataSetData[0].columnName,
					order: 'desc',
				},
			];
		} else {
			state.dataSetData = arr;
		}
	}
};
const removeOrder = (index: number) => {
	if (state.tableModel.orderList.length <= 1) {
		ElMessage.error('至少保留一个');
		return false;
	}
	state.tableModel.orderList.splice(index, 1);
};
const addOrder = () => {
	state.tableModel.orderList.push({
		name: state.tableModel.dataSetData[0].columnName,
		order: 'desc',
	});
};
// 添加筛选字段
const addFilter = () => {
	state.tableModel.filterList.push({
		name: state.tableModel.dataSetData[0].columnName,
		columnFieldDataType: state.tableModel.dataSetData[0].columnFieldDataType,
		filter: '',
		filterType: '0',
		filterSelectListGuDing: [],
	});
};
const filterChange = (val: string, index: number) => {
	state.tableModel.filterList[index].columnFieldDataType = state.tableModel.dataSetData.filter((d: any) => d.columnName === val)[0].columnFieldDataType;
};
const setGuDing = (index: number) => {
	state.guDingIndex = index;
	if (state.tableModel.filterList[index].filterSelectListGuDing) {
		state.guDingList = state.tableModel.filterList[index].filterSelectListGuDing;
	} else {
		state.guDingList = [];
	}
	state.isGuDing = true;
};
const addGuDing = () => {
	state.guDingList.push({ value: '', label: '' });
};
const removeGuDing = (index: number) => {
	state.guDingList.splice(index, 1);
};
const saveGuDing = () => {
	state.tableModel.filterList[state.guDingIndex].filterSelectListGuDing = state.guDingList;
	state.isGuDing = false;
};
const setZhiDing = (index: number) => {
	state.zhiDingIndex = index;
	if (state.tableModel.filterList[index].filterSelectZhiDing) {
		state.zhiDingObj = state.tableModel.filterList[index].filterSelectZhiDing;
	} else {
		state.zhiDingObj = {};
	}
	state.isZhiDing = true;
};
const saveZhiDing = () => {
	state.tableModel.filterList[state.zhiDingIndex].filterSelectZhiDing = state.zhiDingObj;
	state.isZhiDing = false;
};
const removeFilter = (index: number) => {
	state.tableModel.filterList.splice(index, 1);
};
const addRowColorRule = () => {
	state.rowColorRuleTitle = '添加规则';
	rowColorRuleRef.value?.openDialog(state.tableModel.dataSetData, null, null);
};
const handleQueryRule = (index: number | null, rule: any) => {
	var pbj = { ...rule };
	state.tableModel.rowColorList.push(pbj);
	// console.log(state.tableModel.rowColorList);
};
const convertStr = (columnFieldDataType: number, filter: string) => {
	if (columnFieldDataType === 1 || columnFieldDataType === 4) {
		switch (filter) {
			case '0':
				return '=';
			case '1':
				return '≠';
			case '2':
				return '>';
			case '3':
				return '<';
			case '4':
				return '≥';
			case '5':
				return '≤';
			case '6':
				return '在范围内';
			case '7':
				return '不在范围内';
			case '8':
				return '为空';
			case '9':
				return '不为空';
		}
	} else if (columnFieldDataType === 3) {
		return '等于';
	} else if (columnFieldDataType === 0) {
		switch (filter) {
			case '0':
				return '等于';
			case '1':
				return '不等于';
			case '2':
				return '包含';
			case '3':
				return '不包含';
			case '4':
				return '开头是';
			case '5':
				return '开头不是';
			case '6':
				return '结尾是';
			case '7':
				return '结尾不是';
			case '8':
				return '为空';
			case '9':
				return '不为空';
		}
	} else if (columnFieldDataType === 2) {
		switch (filter) {
			case '0':
				return '等于';
			case '1':
				return '不等于';
			case '2':
				return '早于';
			case '3':
				return '晚于';
			case '4':
				return '早于等于';
			case '5':
				return '晚于等于';
			case '6':
				return '在范围内';
			case '7':
				return '不在范围内';
			case '8':
				return '为空';
			case '9':
				return '不为空';
		}
	}
};
const convertDateStr = (date: string) => {
	switch (date) {
		case '0':
			return '昨天';
		case '1':
			return '今天';
		case '2':
			return '明天';
		case '3':
			return '上周';
		case '4':
			return '本周';
		case '5':
			return '下周';
		case '6':
			return '上月';
		case '7':
			return '本月';
		case '8':
			return '下月';
		case '9':
			return '上季度';
		case '10':
			return '本季度';
		case '11':
			return '下季度';
		case '12':
			return '去年';
		case '13':
			return '今年';
		case '14':
			return '明年';
		case '15':
			return '过去7天';
		case '16':
			return '过去14天';
		case '17':
			return '过去30天';
		case '18':
			return '将来7天';
		case '19':
			return '将来14天';
		case '20':
			return '将来30天';
		case '21':
			return '指定日期';
	}
};
// 打开弹窗
const openDialog = () => {
	tabPosition.value = '0';
	tableModelRef.value?.resetFields();
	state.tableModel = {
		name: '' as string,
		describe: '' as string,
		dataSetId: '' as string,
		dataSetData: [] as Array<any>,
		orderList: [] as Array<any>, // 排序
		filterList: [] as Array<any>, // 快速筛选
		rowColorList: [] as Array<any>, // 行颜色
	} as any;
	state.isChangeDataSet = false;
	state.isShowDialog = true;
};

// 关闭弹窗
const closeDialog = () => {
	console.log(state.tableModel);
	return;
	emits('handleQuery');
	state.isShowDialog = false;
};
// 取消
const cancel = () => {
	state.isShowDialog = false;
};
// 提交
const submit = () => {
	tableModelRef.value.validate(async (valid: boolean) => {
		console.log(state.tableModel);
		if (!valid) return;
		proxy.$post(`/api/DataSet/sql/list`, state.tableModel).then((res: any) => {
			state.dataSetLoading = false;
			// if (res.data.code === 200) {
			// 	if (type === '0') {
			// 		state.dataSetList = res.data.data;
			// 	} else {
			// 		state.dataSetListZhiDing = res.data.data;
			// 	}
			// 	// console.log(state.dataSetList);
			// }
		});
		// closeDialog();
	});
};

// 导出对象
defineExpose({ openDialog });
</script>
<style scoped>
.card-header {
	display: flex;
	justify-content: space-between;
	align-items: center;
}

.text {
	font-size: 14px;
}

.box-card {
	width: 100%;
}
</style>
