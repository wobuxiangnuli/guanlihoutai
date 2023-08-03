<template>
  <div class="sys-userCenter-container">
    <el-card shadow="hover" style="overflow-y: auto;">
      <el-tabs>
        <el-tab-pane label="系统基础信息设置">
          <el-form :model="model" ref="settingFormBaseRef" label-width="auto" style="width: 40%;margin: 0 auto;">
            <el-form-item label="系统名称" prop="name" :rules="[{ required: true, message: '系统名称不能为空', trigger: 'blur' }]">
              <el-input v-model="model.name" placeholder="系统名称" clearable />
            </el-form-item>
            <el-form-item label="系统域名" prop="url" :rules="[{ required: true, message: '系统域名不能为空', trigger: 'blur' }]">
              <el-input v-model="model.url" placeholder="系统域名" clearable />
            </el-form-item>
            <el-form-item label="系统ico" prop="icoPath" :rules="[{ required: true, message: '请上传系统ico', trigger: 'blur' }]">
              <el-upload ref="uploadRef" :limit="1" :file-list="iconfiles" :action="uploadurl" :on-change="iconChange" :headers="headers" :on-success="iconSuccess" accept=".ico" style="width: 100%;">
                <el-button type="primary">选择ico</el-button>
              </el-upload>
            </el-form-item>
            <el-form-item label="系统图标" prop="imgPath" :rules="[{ required: true, message: '请上传系统图标', trigger: 'blur' }]">
              <el-upload ref="uploadRef" :limit="1" :file-list="imgfiles" :action="uploadurl" :headers="headers" :on-change="imgChange" :on-success="imgSuccess" accept=".svg" style="width: 100%;">
                <el-button type="primary">选择svg图标</el-button>
              </el-upload>
            </el-form-item>
            <el-form-item label="登录滑块图片" prop="loginImgPath" :rules="[{ required: true, message: '请上传登录滑块图片', trigger: 'blur' }]">
              <el-upload ref="uploadRef" :limit="1" :file-list="loginImgfiles" :action="uploadurl" :headers="headers" :on-change="loginImgChange" :on-success="loginImgSuccess" accept=".svg" style="width: 100%;">
                <el-button type="primary">选择svg图片</el-button>
              </el-upload>
            </el-form-item>
            <el-form-item label="登录页轮播图" prop="rotationChart" :rules="[{ required: true, message: '请上传图片', trigger: 'blur' }]">
              <el-upload ref="uploadRef" list-type="picture-card" :file-list="lunbofiles" :action="uploadurl" :headers="headers" :on-change="lunboChange" :on-success="lunboSuccess" :on-preview="handlePictureCardPreview" accept=".svg,.png,.jpg" style="width: 100%;">
                <el-icon>
                  <Plus />
                </el-icon>
              </el-upload>
            </el-form-item>
            <el-form-item label="">
              <el-button icon="ele-SuccessFilled" type="primary" @click="save" v-auth="'sysUser:baseInfo'"> 保存配置 </el-button>
            </el-form-item>
          </el-form>
        </el-tab-pane>
      </el-tabs>
    </el-card>
    <el-dialog v-model="dialogVisible">
      <img w-full :src="dialogImageUrl" alt="Preview Image" />
    </el-dialog>
  </div>
</template>
<script lang="ts" setup name="">
import { onMounted, ref, getCurrentInstance } from 'vue';
import { systemSettingApi } from '/@/api/systemSetting/index';
import { Local } from '/@/utils/storage';
import { ElMessage, ElForm } from 'element-plus';
import { Plus } from '@element-plus/icons-vue';
const { proxy }: any = getCurrentInstance();

const model = ref({
	id: 0,
	name: '',
	url: '',
	icoPath: '',
	imgPath: '',
	loginImgPath: '',
	rotationChart: [] as string[],
});

const settingFormBaseRef = ref<InstanceType<typeof ElForm>>();

const dialogImageUrl = ref('');
const dialogVisible = ref(false);
const handlePictureCardPreview = (uploadFile: any) => {
	dialogImageUrl.value = uploadFile.url!;
	dialogVisible.value = true;
};

const uploadurl = (import.meta.env.VITE_API_URL as any) + '/api/sysfile/uploadfile';
const headers = ref({ Authorization: `Bearer ${Local.get('access-token')}` });
// ico上传方法
const iconfiles = ref([] as any[]);
const iconChange = (file: any, fileList: []) => {
	iconfiles.value = fileList;
	console.log(iconfiles.value);
};
const iconSuccess = (res: any) => {
	if (res.code == 200) {
		ElMessage.success('上传成功');
	}
};
// 图标上传方法
const imgfiles = ref([] as any[]);
const imgChange = (file: any, fileList: []) => {
	imgfiles.value = fileList;
};
const imgSuccess = (res: any) => {
	if (res.code == 200) {
		ElMessage.success('上传成功');
	}
};
// 登录验证图标滑块上传方法
const loginImgfiles = ref([] as any[]);
const loginImgChange = (file: any, fileList: []) => {
	loginImgfiles.value = fileList;
};
const loginImgSuccess = (res: any) => {
	if (res.code == 200) {
		ElMessage.success('上传成功');
	}
};
// 轮播图上传方法
const lunbofiles = ref([] as any[]);
const lunboChange = (file: any, fileList: []) => {
	lunbofiles.value = fileList;
};
const lunboSuccess = (res: any) => {
	if (res.code == 200) {
		ElMessage.success('上传成功');
	}
};

onMounted(() => {
	proxy.$get('/api/systemsetting').then((res: any) => {
		if (res.data.result) {
			var data: any = res.data.result;
			// console.log(data);
			model.value = { ...data.setting };
			iconfiles.value = [
				{
					name: data.icoFile.fileName + data.icoFile.suffix,
					uid: data.icoFile.id,
					response: { result: data.icoFile },
				},
			];
			imgfiles.value = [
				{
					name: data.imgFile.fileName + data.imgFile.suffix,
					uid: data.imgFile.id,
					response: { result: data.imgFile },
				},
			];
			loginImgfiles.value = [
				{
					name: data.loginImgFile.fileName + data.loginImgFile.suffix,
					uid: data.loginImgFile.id,
					response: { result: data.loginImgFile },
				},
			];
			lunbofiles.value = [] as any[];
			data.rotationChartFileList.forEach((d: any) => {
				lunbofiles.value.push({
					name: d.fileName + d.suffix,
					uid: d.id,
					url: d.url,
					response: { result: d },
				});
			});
		}
	});
});

const save = () => {
	let file = null as any;
	if (iconfiles.value.length > 0) {
		file = iconfiles.value[0] as any;
		model.value.icoPath = file.response.result.id;
	} else {
		model.value.icoPath = '';
	}
	if (imgfiles.value.length > 0) {
		file = imgfiles.value[0] as any;
		model.value.imgPath = file.response.result.id;
	} else {
		model.value.imgPath = '';
	}
	if (loginImgfiles.value.length > 0) {
		file = loginImgfiles.value[0] as any;
		model.value.loginImgPath = file.response.result.id;
	} else {
		model.value.loginImgPath = '';
	}
	if (lunbofiles.value.length > 0) {
		model.value.rotationChart = [];
		lunbofiles.value.forEach((d: any) => {
			model.value.rotationChart.push(d.response.result.id);
		});
	} else {
		model.value.rotationChart = [];
	}
	settingFormBaseRef.value?.validate((valid: boolean) => {
		if (!valid) return;
		systemSettingApi.save(model.value).then((res) => {
			if (res.data.code == 200) {
				ElMessage.success('保存成功');
			}
		});
	});
};
</script>