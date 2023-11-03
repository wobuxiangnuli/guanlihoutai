/* tslint:disable */
/* eslint-disable */
/**
 * Admin.NET
 * 让 .NET 开发更简单、更通用、更流行。前后端分离架构(.NET6/Vue3)，开箱即用紧随前沿技术。<br/><a href='https://gitee.com/zuohuaijun/Admin.NET/'>https://gitee.com/zuohuaijun/Admin.NET</a>
 *
 * OpenAPI spec version: 1.0.0
 * Contact: 515096995@qq.com
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
/**
 * 系统文件表
 * @export
 * @interface SysFile
 */
export interface SysFile {
    /**
     * 雪花Id
     * @type {number}
     * @memberof SysFile
     */
    id?: number;
    /**
     * 创建时间
     * @type {Date}
     * @memberof SysFile
     */
    createTime?: Date | null;
    /**
     * 更新时间
     * @type {Date}
     * @memberof SysFile
     */
    updateTime?: Date | null;
    /**
     * 创建者Id
     * @type {number}
     * @memberof SysFile
     */
    createUserId?: number | null;
    /**
     * 修改者Id
     * @type {number}
     * @memberof SysFile
     */
    updateUserId?: number | null;
    /**
     * 软删除
     * @type {boolean}
     * @memberof SysFile
     */
    isDelete?: boolean;
    /**
     * 提供者
     * @type {string}
     * @memberof SysFile
     */
    provider?: string | null;
    /**
     * 仓储名称
     * @type {string}
     * @memberof SysFile
     */
    bucketName?: string | null;
    /**
     * 文件名称（源文件名）
     * @type {string}
     * @memberof SysFile
     */
    fileName?: string | null;
    /**
     * 文件后缀
     * @type {string}
     * @memberof SysFile
     */
    suffix?: string | null;
    /**
     * 存储路径
     * @type {string}
     * @memberof SysFile
     */
    filePath?: string | null;
    /**
     * 文件大小KB
     * @type {string}
     * @memberof SysFile
     */
    sizeKb?: string | null;
    /**
     * 文件大小信息-计算后的
     * @type {string}
     * @memberof SysFile
     */
    sizeInfo?: string | null;
    /**
     * 外链地址-OSS上传后生成外链地址方便前端预览
     * @type {string}
     * @memberof SysFile
     */
    url?: string | null;
    /**
     * 文件MD5
     * @type {string}
     * @memberof SysFile
     */
    fileMd5?: string | null;
}
