<template>
	<div class="chouti">
		<el-drawer :modal="false" :size="640" v-model="drawer" :direction="direction" :before-close="handleClose">
			<template #header>
				<span class="title">新建规则</span>
			</template>
			<div class="content">
				<div class="biaoti">规则名称</div>
				<el-input class="mid-input" placeholder="规则1"></el-input>
				<div class="biaoti">当满足以下条件时</div>

				<div class="rulesdiv" v-for="(item, index) in state" :key="index">
					<el-button class="delete" @click="Delete(index)" link>×</el-button>
					<el-row>
						<el-col :span="3"
							><span v-if="index == 0">当</span>
							<el-select class="wubiankuang" style="width: 80%" v-else v-model="item.lianjieci">
								<el-option v-for="(val, key) in lianjiecilist" :key="key" :label="val" :value="key">{{ val }} </el-option></el-select
							></el-col
						>
						<el-col :span="20">
							<div class="biaoti1">
								{{ item.name }}
								<el-select v-model="item.compare" class="wubiankuang" style="width: 23%">
									<el-option v-for="el in item.options" :key="el.value" :label="el.label" :value="el.value"></el-option>
								</el-select>
							</div>
							<div>
								<el-select
									:disabled="item.compare == 'IS_NULL' || item.compare == 'NOT_NULL' || item.compare == 'IN_RANGE' || item.compare == 'NOT_IN_RANGE' || item.type == 'area'"
									class="select"
									v-model="item.isDynamic"
									style="width: 17%"
								>
									<el-option v-for="(item, key) in shifoudongtai" :key="key" :label="item" :value="key">{{ item }}</el-option>
								</el-select>

								<el-radio-group v-if="item.type == 's'" v-model="item.isEmpty">
									<el-radio :label="false" size="large">有</el-radio>
									<el-radio :label="true" size="large">无</el-radio>
								</el-radio-group>
								<!-- 文本框 -->
								<el-input
									v-model="item.value"
									:disabled="item.compare == 'IS_NULL' || item.compare == 'NOT_NULL'"
									v-if="item.type == 'input' || item.type == 'email' || item.type == 'phone'"
									style="width: 80%"
									placeholder="请输入"
								></el-input>
								<!-- 数值框 -->
								<el-input-number
									v-model="item.value"
									:disabled="item.compare == 'IS_NULL' || item.compare == 'NOT_NULL'"
									v-if="(item.type == 'valnum' || item.type == 'amount') && item.compare != 'IN_RANGE' && item.compare != 'NOT_IN_RANGE'"
									:controls="false"
									style="width: 80%"
									placeholder="请输入数值"
								></el-input-number>
								<!-- 范围选择 -->
								<el-input-number
									v-if="item.type == 'valnum' || (item.type == 'amount' && (item.compare == 'NOT_IN_RANGE' || item.compare == 'IN_RANGE'))"
									style="width: 35%"
									placeholder="最小值"
									:controls="false"
								></el-input-number>
								<span v-if="item.type == 'valnum' || (item.type == 'amount' && (item.compare == 'NOT_IN_RANGE' || item.compare == 'IN_RANGE'))" class="jiange">——</span>
								<el-input-number
									v-if="item.type == 'valnum' || (item.type == 'amount' && (item.compare == 'NOT_IN_RANGE' || item.compare == 'IN_RANGE'))"
									style="width: 35%"
									placeholder="最大值"
									:controls="false"
								></el-input-number>
								<!-- 日期下拉框 -->
								<el-select
									:disabled="item.compare == 'IS_NULL' || item.compare == 'NOT_NULL'"
									v-model="item.value"
									v-if="item.type == 'data' && item.compare != 'NOT_IN_RANGE' && item.compare != 'IN_RANGE'"
									style="width: 80%"
								>
									<el-option v-for="(el, index) in item.datalist" :key="index" :label="el.label" :value="el.value"></el-option>
								</el-select>
								<el-input-number
									v-if="item.type == 'data' && (item.value == 'PAST_DAYS' || item.value == 'FUTURE_DAYS')"
									:controls="false"
									style="width: 30%; margin-left: 95px; margin-top: 5px; margin-right: 5px"
								></el-input-number>
								<el-checkbox v-if="item.type == 'data' && (item.value == 'PAST_DAYS' || item.value == 'FUTURE_DAYS')" label="是否包含今天" size="large" />
								<el-date-picker v-if="item.type == 'data' && item.value == 'SPECIFIC_DATE'" v-model="item.value1" type="date" placeholder="请选择" style="width: 100%; margin-top: 5px" />
								<el-date-picker
									v-if="item.type == 'data' && (item.compare == 'NOT_IN_RANGE' || item.compare == 'IN_RANGE')"
									v-model="item.value"
									type="daterange"
									range-separator="To"
									start-placeholder="Start date"
									end-placeholder="End date"
								/>
								<!-- 时间框 -->
								<el-time-picker v-if="item.type == 'time'" :disabled="item.compare == 'IS_NULL' || item.compare == 'NOT_NULL'" v-model="item.time" style="width: 80%" format="HH:mm" />
								<!-- 地区下拉框 -->
								<el-cascader
									v-model="item.value"
									:options="areaOptions"
									:props="props"
									:disabled="item.compare == 'IS_NULL' || item.compare == 'NOT_NULL'"
									change-on-select
									v-if="item.type == 'area'"
									placeholder="请选择地址"
									style="width: 80%"
								>
								</el-cascader>

								<el-checkbox-group v-if="item.type == 's'" style="width: 80%; display: inline-block">
									<el-checkbox label="Option A" />
									<el-checkbox label="Option B" />
									<el-checkbox label="Option C" />
								</el-checkbox-group>

								<el-radio-group v-if="item.type == 's'" style="width: 80%; display: inline-block">
									<el-radio :label="3">Option A</el-radio>
									<el-radio :label="6">Option B</el-radio>
									<el-radio :label="9">Option C</el-radio>
								</el-radio-group>
							</div>
						</el-col>
					</el-row>
				</div>
				<el-dropdown @command="commanded" trigger="click">
					<span class="el-dropdown-link">
						<el-button link><span class="icon">+</span>添加筛选条件</el-button>
					</span>
					<template #dropdown>
						<el-dropdown-menu>
							<el-dropdown-item v-for="item in formDesignerMockData" :key="item.value" :command="item.type">{{ item.label }}</el-dropdown-item>
						</el-dropdown-menu>
					</template>
				</el-dropdown>
				<div class="biaoti">则执行动作</div>
					<div class="rulesdiv" v-for="(val,index) in action" :key="val.actions">
					<el-button class="delete" @click="Delete(index)" link>×</el-button>
					<el-row>
						<el-col :span="6">
							<el-select class="select" v-model="val.actions">
								<el-option v-for="(item, key) in FieldStatus" :key="key" :command="item">{{ item }}</el-option>
							</el-select>
						</el-col>
						<el-col :span="18">
							<el-select class="select" v-model="val.actions" style="width: 80%;">
								<el-option v-for="(item, key) in FieldStatus" :key="key" :command="item">{{ item }}</el-option>
							</el-select>
						</el-col>
					</el-row>
				
				</div>
				<el-dropdown @command="actioned" trigger="click">
					<span class="el-dropdown-link">
						<el-button link><span class="icon">+</span>添加动作</el-button>
					</span>
					<template #dropdown>
						<el-dropdown-menu>
							<el-dropdown-item v-for="(item, key) in FieldStatus" :key="key" :command="item">{{ item }}</el-dropdown-item>
						</el-dropdown-menu>
					</template>
				</el-dropdown>
			</div>
		</el-drawer>
	</div>
</template>
<script setup lang="ts">
import { ref, reactive, getCurrentInstance } from 'vue';
import { ElMessageBox } from 'element-plus';
import { lianjiecilist, shifoudongtai, typeConfigList, FieldStatus } from './aside';
const { proxy }: any = getCurrentInstance();
const direction = ref('rtl');
const drawer = ref(false);

const state = reactive<Rules[]>([]);
const action = reactive<actionList[]>([]);
const areaOptions = ref<any[]>([]);
const props = {
	lazy: true,
	lazyLoad(node: any, resolve: Function) {
		const { level } = node;

		// 这里可以根据不同的level调用不同的API端点。
		let apiEndpoint = '/api/sysRegion/province'; // 假设API端点

		// 根据level判断
		if (level === 0) {
			apiEndpoint = '/api/sysRegion/province';
		} else if (level === 1) {
			apiEndpoint = '/api/sysRegion/childRegionByPid/' + node.value;
		} else if (level === 2) {
			apiEndpoint = '/api/sysRegion/childRegionByPid/' + node.value;
		} else if (level === 3) {
			apiEndpoint = '/api/sysRegion/childRegionByPid/' + node.value;
		}

		proxy.$get(apiEndpoint, {}).then((response: any) => {
			// 根据你的API响应结构处理返回的数据
			const children = response.data.result.map((item: any) => ({
				value: item.id,
				label: item.name,
				leaf: level >= 2, // 当level等于2时，表示没有子级
			}));
			resolve(children);
		});
	},
};

const Delete = (index: number) => {
	state.splice(index, 1);
	action.splice(index,1);
};
const formDesignerMockData = [
	{
		value: 'input',
		label: '文本',
		type: 'input',
	},
	{
		value: 'valnum',
		label: '数值',
		type: 'valnum',
	},
	{
		value: 'amount',
		label: '金额',
		type: 'amount',
	},
	{
		value: 'email',
		label: '邮箱',
		type: 'email',
	},

	{
		value: 'phone',
		label: '电话',
		type: 'phone',
	},
	{
		value: 'data',
		label: '日期',
		type: 'data',
	},
	{
		value: 'time',
		label: '时间',
		type: 'time',
	},
	{
		value: 'area',
		label: '地区',
		type: 'area',
	},
];



const open = () => {
	drawer.value = true;
};
const handleClose = (done: () => void) => {
	ElMessageBox.confirm('Are you sure you want to close this?')
		.then(() => {
			done();
		})
		.catch(() => {
			// catch error
		});
};
const commanded = (command: string) => {
	let arr: any = typeConfigList.getConfig(command);
	state.push(arr);
};
const actioned = (command: string) => {
	let ac: any = typeConfigList.getAction(command);
	action.push(ac)
};
defineExpose({
	open,
});
</script>
<style scoped lang="scss">
.wubiankuang {
	--el-border-color: #ffffff !important;
	--el-select-border-color-hover: #ffffff !important;
	--el-select-input-focus-border-color: #ffffff !important;
}
.rulesdiv{
	position: relative;
}
.delete {
	position: absolute;
	top: 5px;
	right: 50px;
	font-size: 22px;
	color: rgb(190, 190, 190);
	z-index: 2002;
}
.jiange {
	margin: 0 5px 0 5px;
}
.biaoti1 {
	margin-bottom: 10px;
}
.select {
	margin-right: 10px;
	margin-bottom: 5px;
}

.chouti {
	.title {
		height: 20px;
		font-size: 18px;
		font-weight: 700;
	}
	.biaoti {
		font-size: 14px;
		font-weight: 700;
		margin-bottom: 5px;
		margin-top: 20px;
	}
	.content {
		padding-top: 50px;
		padding-left: 40px;
	}
	.mid-input {
		width: 30%;
	}
}
</style>
