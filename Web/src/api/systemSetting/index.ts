import request from '/@/utils/request';

/**
 * （不建议写成 request.post(xxx)，因为这样 post 时，无法 params 与 data 同时传参）
 *
 * 系统设定api接口集合
 * @method get 获取配置
 * @method save 保存配置
 */
export const systemSettingApi={
		get: () => {
			return request({
				url: '/api/systemsetting',
				method: 'get',
			});
		},
		save: (data: object) => {
			return request({
				url: '/api/systemsetting/save',
				method: 'post',
				data,
			});
		},
}
