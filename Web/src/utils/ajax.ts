import request from '/@/utils/request';

function requestAxios(url: string, data: any, method: any) {
	return request({
		url: url,
		method: method,
		data: data,
	});
}

// 一下部分都是带旋转图标,并且错误有提示，但不返货具体错误
export function get(url: string, params?: any) {
	return request({
		url: url,
		method: 'get',
		params: params,
	});
}
export function post(url: string, data: any) {
	return requestAxios(url, data, 'post');
}
export function put(url: string, data: any) {
	return requestAxios(url, data, 'put');
}
export function deletes(url: string, data: any) {
	return requestAxios(url, data, 'delete');
}
