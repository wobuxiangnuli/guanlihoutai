<template>
  <div class="sys-region-container">
    <el-dialog v-model="state.isShowDialog" draggable :close-on-click-modal="false" width="60%">
      <template #header>
        <div style="color: #fff">
          <el-icon size="16" style="margin-right: 3px; display: inline; vertical-align: middle"> <ele-Setting />
          </el-icon>
          <span> {{ props.title }} </span>
        </div>
      </template>
      <div class="sidel" style="min-height:500px;">
        <el-form :model="state.tableModel" ref="tableModelRef" label-width="auto">
          <el-form-item label="规则名称" prop="name" :rules="[{ required: true, message: '规则名称不能为空', trigger: 'blur' }]">
            <el-input v-model="state.tableModel.name" />
          </el-form-item>
          <el-form-item label="当满足条件时">
            <el-row style="width: 100%;line-height: 40px;" v-for="(o,index) in state.tableModel.ruleList" :key="index">
              <el-col :span="4">
                <el-select v-model="o.name" @change="(val:string)=>ruleChange(val,index)">
                  <el-option v-for="(d,index) in state.dataSetData" :key="index" :label="d.displayName" :value="d.columnName" />
                </el-select>
              </el-col>
              <el-col :span="4">
                <el-select v-if="o.columnFieldDataType===1||o.columnFieldDataType===4" v-model="o.filter" @change="(val:string)=>{
                state.tableModel.ruleList[index].isFilterStr = val==='8'||val==='9'
                if(val==='8'||val==='9'){state.tableModel.ruleList[index].filterStr=''}
              }">
                  <el-option label="=" value="0" />
                  <el-option label="≠" value="1" />
                  <el-option label=">" value="2" />
                  <el-option label="<" value="3" />
                  <el-option label="≥" value="4" />
                  <el-option label="≤" value="5" />
                  <el-option label="在范围内" value="6" />
                  <el-option label="不在范围内" value="7" />
                  <el-option label="为空" value="8" />
                  <el-option label="不为空" value="9" />
                </el-select>
                <el-select v-if="o.columnFieldDataType===3" v-model="o.filter">
                  <el-option label="等于" value="0" />
                </el-select>
                <el-select v-if="o.columnFieldDataType===0" v-model="o.filter" @change="(val:string)=>{
                state.tableModel.ruleList[index].isFilterStr = val==='8'||val==='9'
                if(val==='8'||val==='9'){state.tableModel.ruleList[index].filterStr=''}
              }">
                  <el-option label="等于" value="0" />
                  <el-option label="不等于" value="1" />
                  <el-option label="包含" value="2" />
                  <el-option label="不包含" value="3" />
                  <el-option label="开头是" value="4" />
                  <el-option label="开头不是" value="5" />
                  <el-option label="结尾是" value="6" />
                  <el-option label="结尾不是" value="7" />
                  <el-option label="为空" value="8" />
                  <el-option label="不为空" value="9" />
                </el-select>
                <el-select v-if="o.columnFieldDataType===2" v-model="o.filter" @change="(val:string)=>{
                state.tableModel.ruleList[index].isFilterStr = val==='8'||val==='9'
                if(val==='8'||val==='9'){state.tableModel.ruleList[index].filterStr=''}
              }">
                  <el-option label="等于" value="0" />
                  <el-option label="不等于" value="1" />
                  <el-option label="早于" value="2" />
                  <el-option label="晚于" value="3" />
                  <el-option label="早于等于" value="4" />
                  <el-option label="晚于等于" value="5" />
                  <el-option label="在范围内" value="6" />
                  <el-option label="不在范围内" value="7" />
                  <el-option label="为空" value="8" />
                  <el-option label="不为空" value="9" />
                </el-select>
              </el-col>
              <el-col :span="8" v-if="(o.columnFieldDataType===1||o.columnFieldDataType===4)&&(o.filter==='6'||o.filter==='7')">
                <el-input-number controls-position="right" :disabled="o.isFilterStr" v-model="o.minNum" style="width: 50%;" />
                <el-input-number controls-position="right" :disabled="o.isFilterStr" v-model="o.maxNum" style="width: 50%;" />
              </el-col>
              <el-col :span="8" v-else-if="o.columnFieldDataType===2">
                <el-select style="width: 100%;" v-if="o.columnFieldDataType===2&&!(o.filter==='6'||o.filter==='7')" :disabled="o.isFilterStr" v-model="o.filterStr">
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
                  <el-option-group>
                    <el-option label="指定日期" value="21" />
                  </el-option-group>
                </el-select>
                <el-date-picker style="width: 100%;" v-else v-model="o.rangeTime" type="daterange" value-format="YYYY/MM/DD" />
              </el-col>
              <el-col :span="8" v-else>
                <el-input placeholder="请输入参数" :disabled="o.isFilterStr" v-model="o.filterStr" />
              </el-col>
              <el-col :span="5" v-if="o.columnFieldDataType===2&&o.filterStr==='21'&&!(o.filter==='6'||o.filter==='7')">
                <el-date-picker style="width: 100%;" v-model="o.time" value-format="YYYY/MM/DD" type="date" />
              </el-col>
              <el-col :span="1">
                <el-button @click="removeRule(index)" :icon="Delete" type="danger" circle />
              </el-col>
            </el-row>
            <div style="width: 100%;">
              <el-button :icon="Plus" type="primary" style="width: 100%;" @click="addRule">添加条件</el-button>
            </div>
          </el-form-item>
          <el-form-item label="行颜色为" prop="name" :rules="[{ required: true, message: '请选取颜色', trigger: 'blur' }]">
            <el-color-picker size="large" v-model="state.tableModel.color" />
          </el-form-item>
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

<script lang="ts" setup name="rowColorRule">
import { reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import { Delete, Plus } from '@element-plus/icons-vue';
const tableModelRef = ref();
const props = defineProps({
	title: String,
});
const emits = defineEmits(['handleQuery']);
const state = reactive({
	isShowDialog: false as boolean,
	dataSetData: [] as Array<any>,
	index: null as number | null,
	tableModel: {
		name: '' as string,
		ruleList: [] as Array<any>,
		color: '' as string,
	} as any,
});
const addRule = () => {
	state.tableModel.ruleList.push({
		name: state.dataSetData[0].columnName,
		columnFieldDataType: state.dataSetData[0].columnFieldDataType,
		filter: '',
		filterStr: '',
		isFilterStr: false,
		minNum: 0,
		maxNum: 0,
		time: '',
		rangeTime: [],
	});
};
const ruleChange = (val: string, index: number) => {
	state.tableModel.ruleList[index].columnFieldDataType = state.dataSetData.filter((d: any) => d.columnName === val)[0].columnFieldDataType;
};
const removeRule = (index: number) => {
	state.tableModel.ruleList.splice(index, 1);
};
// 打开弹窗
const openDialog = (dataSetData: Array<any>, tableModel: {} | null, index: number | null) => {
	state.dataSetData = dataSetData;
	state.index = index;
	if (tableModel) {
		state.tableModel = tableModel;
	} else {
		tableModelRef.value?.resetFields();
		state.tableModel.name = '';
		state.tableModel.ruleList = [];
		state.tableModel.color = '';
	}
	state.isShowDialog = true;
};
// 关闭弹窗
const closeDialog = () => {
	emits('handleQuery', state.index, state.tableModel);
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
		closeDialog();
	});
};

// 导出对象
defineExpose({ openDialog });
</script>
