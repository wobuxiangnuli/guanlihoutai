<template>
  <div class="sys-region-container">
    <el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="80%">
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
      <div class="sidel" style="min-height:600px;">
        <el-form :model="state.tableModel" ref="tableModelRef" label-width="auto">
          <el-tabs tab-position="left" v-model="tabPosition">
            <el-tab-pane label="基础配置" name="0" style="height:600px;">
              <el-form-item label="名字" prop="name" :rules="[{ required: true, message: '名字不能为空', trigger: 'blur' }]">
                <el-input v-model="state.tableModel.name" />
              </el-form-item>
              <el-form-item label="描述" prop="describe">
                <el-input type="textarea" v-model="state.tableModel.describe" />
              </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="数据源设置" name="1" style="height:600px;">
              <el-form-item label="DataSet" prop="dataSetId" :rules="[{ required: true, message: '请选择dataSet', trigger: 'blur' }]">
                <el-select filterable v-model="state.tableModel.dataSetId" @change="dataSetSelect">
                  <el-option label="111" value="1" />
                  <el-option label="222" value="2" />
                  <el-option label="333" value="3" />
                  <el-option label="444" value="4" />
                </el-select>
              </el-form-item>
              <el-form-item label="数据列设置" v-if="state.isChangeDataSet">
                <el-table :data="state.tableModel.dataSetData" row-key="id">
                  <el-table-column prop="columnName" label="列名" align="center" />
                  <el-table-column prop="displayName" label="显示名称" align="center" />
                  <el-table-column prop="align" label="对齐方式" align="center" />
                  <el-table-column prop="maxWidth" label="最大宽度" align="center" />
                  <el-table-column prop="display" label="显示隐藏" align="center">
                    <template #default="scope">
                      <el-switch v-model="scope.row.display" />
                    </template>
                  </el-table-column>
                </el-table>
              </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="排序方式" name="2" style="height:600px;">
              <el-form-item label="" v-if="state.isChangeDataSet">
                <div style="width: 100%;line-height: 60px;" v-for="(o,index) in state.tableModel.orderList" :key="index">
                  <el-select v-model="o.name">
                    <el-option v-for="(d,index) in state.tableModel.dataSetData" :key="index" :label="d.displayName" :value="d.columnName" />
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
            <el-tab-pane label="快速筛选" name="3" style="height:600px;">
              <el-form-item label="">
                <div style="width: 100%;line-height: 60px;" v-for="(o,index) in state.tableModel.filterList" :key="index">
                  <el-select v-model="o.name" @change="(val:string)=>filterChange(val,index)">
                    <el-option v-for="(d,index) in state.tableModel.dataSetData" :key="index" :label="d.displayName" :value="d.columnName" />
                  </el-select>
                  <el-select v-if="o.type==='long'||o.type==='int'" v-model="o.filter">
                    <el-option label="等于" value="0" />
                  </el-select>
                  <el-select v-if="o.type==='string'" v-model="o.filter">
                    <el-option label="模糊搜索" value="0" />
                    <el-option label="精准搜索" value="1" />
                  </el-select>
                  <el-select v-if="o.type==='decimal'||o.type==='double'" v-model="o.filter">
                    <el-option label="等于" value="0" />
                    <el-option label="在范围内 " value="1" />
                  </el-select>
                  <el-select v-if="o.type==='DateTime'" multiple collapse-tags v-model="o.filterList">
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
                  <el-button @click="removeFilter(index)" :icon="Delete" type="danger" circle />
                </div>
                <div style="width: 60%;">
                  <el-button :icon="Plus" type="primary" style="width: 100%;" @click="addFilter">选择字段</el-button>
                </div>
              </el-form-item>
            </el-tab-pane>
            <el-tab-pane label="行颜色显示" name="4" style="height:600px;">行颜色显示</el-tab-pane>
            <el-tab-pane label="操作" name="5">操作</el-tab-pane>

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
  </div>
</template>

<script lang="ts" setup name="tableSetting">
import { reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import { Delete, Plus } from '@element-plus/icons-vue';
const tableModelRef = ref();
const tabPosition = ref('3' as string);
const props = defineProps({
	title: String,
});
const emits = defineEmits(['handleQuery']);
const state = reactive({
	isShowDialog: true as boolean,
	isChangeDataSet: false as boolean,
	tableModel: {
		name: '' as string,
		describe: '' as string,
		dataSetId: '' as string,
		dataSetData: [
			{
				columnName: 'id',
				displayName: '主键',
				align: 'center',
				maxWidth: '100',
				display: true,
				type: 'long',
			},
			{
				columnName: 'name',
				displayName: '姓名',
				align: 'center',
				maxWidth: '100',
				display: true,
				type: 'string',
			},
			{
				columnName: 'price',
				displayName: '金额',
				align: 'center',
				maxWidth: '100',
				display: true,
				type: 'double',
			},
			{
				columnName: 'time',
				displayName: '时间',
				align: 'center',
				maxWidth: '100',
				display: true,
				type: 'DateTime',
			},
		] as Array<any>,
		orderList: [] as Array<any>, // 排序
		filterList: [] as Array<any>, // 快速筛选
	} as any,
});
// 数据源选择后触发
const dataSetSelect = (value: string) => {
	console.log(value);
	state.isChangeDataSet = true;
	state.tableModel.orderList = [
		{
			name: state.tableModel.dataSetData[0].columnName,
			order: 'asc',
		},
	];
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
		order: 'asc',
	});
};
// 添加筛选字段
const addFilter = () => {
	state.tableModel.filterList.push({
		name: state.tableModel.dataSetData[0].columnName,
		type: state.tableModel.dataSetData[0].type,
		filter: '',
	});
};
const filterChange = (val: string, index: number) => {
	state.tableModel.filterList[index].type = state.tableModel.dataSetData.filter((d: any) => d.columnName === val)[0].type;
};
const removeFilter = (index: number) => {
	state.tableModel.filterList.splice(index, 1);
};
// 打开弹窗
const openDialog = () => {
	tabPosition.value = '0';
	tableModelRef.value.resetFields();
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
const submit = () => {
	tableModelRef.value.validate(async (valid: boolean) => {
		if (!valid) return;
		console.log(state.tableModel);
		// closeDialog();
	});
};

// 导出对象
defineExpose({ openDialog });
</script>
