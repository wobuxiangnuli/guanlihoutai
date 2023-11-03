<template>
	<el-dialog class="dialog-header" v-model="state.isShowSearch" width="60%" :before-close="closeSearch" :show-close="false">
		<el-form-item v-if="state.pageMessage.type !== 1" label="上级菜单">
			<el-select v-model="state.pageMessage.pid" class="m-2" placeholder="请选择上级菜单">
				<el-option v-for="item in stated.menuList" :key="item.value" :label="item.label" :value="item.value" />
			</el-select>
		</el-form-item>
		<el-radio-group v-model="state.pageMessage.type">
			<el-radio :label="1">分组菜单</el-radio>
			<el-radio :label="4">普通列表</el-radio>
			<el-radio :label="5">自定义页面</el-radio>
			<el-radio :label="6">外部页面</el-radio>
		</el-radio-group>
		<div class="content" v-if="state.pageMessage.type == 1">
			<span>分组菜单名称</span>
			<el-input v-model="state.pageMessage.title" class="content" placeholder="请填写文本内容"></el-input>
			<span>图标选择</span>
			<IconSelector v-model="state.pageMessage.icon"  placeholder="菜单图标" type="all" />
            <span>备注：</span>
			<el-input class="content" v-model="state.pageMessage.remark" type="textarea"></el-input>
		</div>
		<div class="content" v-if="state.pageMessage.type == 4">
			<span>工作表名称</span>
			<el-input v-model="state.pageMessage.title" class="content" placeholder="请填写文本内容"></el-input>
			<span>图标选择</span>
			<IconSelector class="content" v-model="state.pageMessage.icon"  placeholder="菜单图标" type="all" />
            <span>备注：</span>
			<el-input class="content" v-model="state.pageMessage.remark" type="textarea"></el-input>
           
		</div>
		<div class="content" v-if="state.pageMessage.type == 5">
			<span>页面名称</span>
			<el-input v-model="state.pageMessage.title" class="content" placeholder="请填写文本内容"></el-input>
			<span>图标选择</span>
			<IconSelector class="content" v-model="state.pageMessage.icon"  placeholder="菜单图标" type="all" />
		
            <span>备注：</span>
			<el-input v-model="state.pageMessage.remark" type="textarea"></el-input>
          </div>
		<div class="content" v-if="state.pageMessage.type == 6">
			<span>页面名称</span>
			<el-input v-model="state.pageMessage.title" class="content" placeholder="请填写文本内容"></el-input>
			<span>外部链接</span>
			<el-input v-model="state.pageMessage.outLink" class="content" placeholder="请填写文本内容"></el-input>
			<span>单选</span><br />
			<el-radio-group v-model="state.pageMessage.isIframe">
				<el-radio :label="true">嵌入页面</el-radio>
				<el-radio :label="false">新窗口打开</el-radio> </el-radio-group
			><br />
            <span>图标选择</span>
			<IconSelector class="content" v-model="state.pageMessage.icon"  placeholder="菜单图标" type="all" />
			<span>备注：</span>
			<el-input class="content" v-model="state.pageMessage.remark" type="textarea"></el-input>
		</div>
		<div class="action">
			<el-button @click="close">取消</el-button>
			<el-button type="primary" @click="submit">确认</el-button>
		</div>
	</el-dialog>
</template>

<script setup lang="ts" name="layoutBreadcrumbSearch">
import {useFormMessage} from '../../stores/formMessage'
import { useRoutesList } from '/@/stores/routesList'
import IconSelector from '/@/components/iconSelector/index.vue'
import { storeToRefs } from 'pinia';
import { useRouter } from 'vue-router'
import { reactive,getCurrentInstance} from 'vue'
const stores = useRoutesList();
const router = useRouter();
const { routesList } = storeToRefs(stores);
const aaa = useFormMessage();
const stated = reactive<AsideState>({
	menuList: [],
	clientWidth: 0,
});
const { proxy }: any = getCurrentInstance();

// 定义变量内容
const state = reactive<DialogState>({
	isShowSearch: false,
	pageMessage: {
		pid: '',
		type: 1,
		title: '',
		icon: '',
		outLink: '',
		isIframe: true,
		remark: '',
	},
});
const close = () => {
	state.isShowSearch = false;
};
const submit = () => {
	aaa.setPageMessage(state.pageMessage)
	console.log(localStorage.fmjson.list);
	
	if(state.pageMessage.type != 4){
		proxy.$post('api/sysMenu/lowCodeAddMenu', { ...state.pageMessage }).then((res: any) => console.log(res));
	}
	state.isShowSearch = false;
	router.push('/create');

};
// 搜索弹窗打开
const openDialog = () => {
	state.isShowSearch = true;
	routesList.value.map((item: any) => {
		stated.menuList.push({
			value: item.id,
			label: item.meta.title,
		});
	});

};
// 搜索弹窗关闭
const closeSearch = () => {
	state.isShowSearch = false;
};

// 暴露变量
defineExpose({
	openDialog,
});
</script>
<style lang="scss" scoped>
span{
    margin: 10px 0 20px 0;
}
.content {
	margin: 20px 0 10px 0;
}
.dialog-header {
	margin: 10px 0 10px 0;
}
.action {
	display: flex;
	justify-content: center;  
}
</style>
