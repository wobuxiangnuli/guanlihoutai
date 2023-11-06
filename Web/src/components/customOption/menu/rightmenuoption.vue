<template>
  <!-- 在需要右键菜单的元素，绑定contextmenu事件 -->
  <el-dropdown v-if="globalData.showbtn" @command="handleCommand" trigger="contextmenu" style="position:relative;left: 50px;">
    <span class="el-dropdown-link">
      <el-icon>
        <ele-MoreFilled />
      </el-icon>
    </span>
    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item command="add">增加</el-dropdown-item>
        <el-dropdown-item command="edit">编辑</el-dropdown-item>
        <el-dropdown-item command="del">删除</el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>
  <EditMenu ref="editMenuRef" :title="state.editMenuTitle" :menuData="state.menuData" @handleQuery="handleQuery" />
</template>
<script setup lang="ts" name="navMenuVertical">
import { inject, ref, reactive, getCurrentInstance } from 'vue';
import { ElMessageBox, ElMessage } from 'element-plus';
import { getAPI } from '/@/utils/axios-utils';
import { SysMenuApi } from '/@/api-services/api';
import { SysMenu } from '/@/api-services/models';
import EditMenu from '/@/views/system/menu/component/editMenu.vue';
const { proxy }: any = getCurrentInstance();
const state = reactive({
	menuData: [] as Array<SysMenu>,
	editMenuTitle: '',
});
const editMenuRef = ref<InstanceType<typeof EditMenu>>();
// 使用inject获取全局变量
const globalData: any = inject('globalData');
// 定义父组件传过来的值
const props = defineProps({
	// 菜单列表
	menus: {
		default: () => {},
		// type: {} as any,
	},
});
const handleCommand = (command: string) => {
	// console.log(props.menus);
	switch (command) {
		case 'add':
			state.editMenuTitle = '添加菜单';
			editMenuRef.value?.openDialog({ type: 2, isHide: false, isKeepAlive: true, isAffix: false, isIframe: false, status: 1, orderNo: 100 });
			break;
		case 'edit':
			proxy.$get(`/api/sysMenu/${props.menus.id}`).then((res: any) => {
				if (res.data.result) {
					console.log(res.data.result);
					state.editMenuTitle = '编辑菜单';
					editMenuRef.value?.openDialog(res.data.result);
				}
			});
			break;
		case 'del':
			ElMessageBox.confirm(`确定删除菜单：【${props.menus.meta.title}】?`, '提示', {
				confirmButtonText: '确定',
				cancelButtonText: '取消',
				type: 'warning',
			})
				.then(async () => {
					await getAPI(SysMenuApi).apiSysMenuDeletePost({ id: props.menus.id });
					handleQuery();
					ElMessage.success('删除成功');
				})
				.catch(() => {});
			break;
		default:
			break;
	}
};
const handleQuery = () => {
	window.location.reload();
};
</script>