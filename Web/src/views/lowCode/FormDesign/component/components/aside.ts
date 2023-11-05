
export enum lianjiecilist {
    "&&" = '且',
    "||" = '或'
}
export enum shifoudongtai {
    "fixed" = '固定值',
    "dynamic" = '动态值'
}
export enum FieldStatus {
    DISPLAY = "显示",
    HIDE = "隐藏",
    EDITABLE = "可编辑",
    READ_ONLY = "只读",
    REQUIRED = "必填",
    ERROR = "提示错误",
    LOCK_RECORD = "锁定记录"
}



/**
* 时间段枚举
*/
export enum TimePeriod {
    TODAY = "今天",           // 今天
    YESTERDAY = "昨天",       // 昨天
    TOMORROW = "明天",       // 明天
    THIS_WEEK = "本周",       // 本周
    LAST_WEEK = "上周",       // 上周
    NEXT_WEEK = "下周",       // 下周
    THIS_MONTH = "本月",      // 本月
    LAST_MONTH = "上个月",    // 上个月
    NEXT_MONTH = "下个月",    // 下个月
    THIS_QUARTER = "本季度",   // 本季度
    LAST_QUARTER = "上季度",   // 上季度
    NEXT_QUARTER = "下季度",   // 下季度
    THIS_YEAR = "今年",       // 今年
    LAST_YEAR = "去年",       // 去年
    NEXT_YEAR = "明年",       // 明年
    PAST_DAYS = "过去...天",  // 过去的指定天数
    FUTURE_DAYS = "将来...天", // 将来的指定天数
    PAST_7_DAYS = "过去7天",  // 过去7天
    PAST_14_DAYS = "过去14天",// 过去14天
    PAST_30_DAYS = "过去30天",// 过去30天
    FUTURE_7_DAYS = "将来7天",// 将来7天
    FUTURE_14_DAYS = "将来14天",// 将来14天
    FUTURE_30_DAYS = "将来30天",// 将来30天
    SPECIFIC_DATE = "指定时间"  // 指定的日期
}

export enum ComparisonOperator1 {
    EQUAL = "等于",
    NOT_EQUAL = "不等于",
    INCLUDES = "包含",
    DOES_NOT_INCLUDE = "不包含",
    STARTS_WITH = "开头是",
    DOES_NOT_START_WITH = "开头不是",
    ENDS_WITH = "结尾是",
    DOES_NOT_END_WITH = "结尾不是",
    LESS_THAN = "早于",
    GREATER_THAN = "晚于",
    LESS_THAN_OR_EQUAL = "早于等于",
    GREATER_THAN_OR_EQUAL = "晚于等于",
    IS_NULL = "为空",
    NOT_NULL = "不为空",
}
export enum ComparisonOperator2 {
    EQUALS = "=",
    NOT_EQUALS = "!=",
    GREATER_THAN = ">",
    LESS_THAN = "<",
    GREATER_THAN_OR_EQUAL = ">=",
    LESS_THAN_OR_EQUAL = "<=",
    IN_RANGE = "在范围内",
    NOT_IN_RANGE = "不在范围内",
    IS_NULL = "为空",
    NOT_NULL = "不为空",
}
export enum ComparisonOperator3 {
    EQUAL = "等于",
    NOT_EQUAL = "不等于",
    INCLUDES = "包含",
    DOES_NOT_INCLUDE = "不包含",
    STARTS_WITH = "开头是",
    DOES_NOT_START_WITH = "开头不是",
    ENDS_WITH = "结尾是",
    DOES_NOT_END_WITH = "结尾不是",
    IS_NULL = "为空",
    NOT_NULL = "不为空",
}
export enum ComparisonOperator4 {
    EQUAL = "等于",
    NOT_EQUAL = "不等于",
    LESS_THAN = "早于",
    GREATER_THAN = "晚于",
    LESS_THAN_OR_EQUAL = "早于等于",
    GREATER_THAN_OR_EQUAL = "晚于等于",
    IN_RANGE = "在范围内",
    NOT_IN_RANGE = "不在范围内",
    IS_NULL = "为空",
    NOT_NULL = "不为空",
}
export enum ComparisonOperator5 {
    IS_ONE_OF = "是其中一个",
    IS_NOT_ONE_OF = "不是其中一个",
    BELONGS_TO = "属于",
    DOES_NOT_BELONG_TO = "不属于",
    IS_NULL = "为空",
    NOT_NULL = "不为空",
}
// 获取不同类型的配置地段
export class typeConfigList {
    static getAction(type: string): any {
        return{
            actions:type,
            executor:[]
        }
    }

    static getConfig(type: string): any {
        switch (type) {
            case 'input':
                return {
                    part: '',
                    compare: '等于',
                    isEmpty: false,
                    type: 'input',
                    lianjieci: '且',
                    value: '',
                    isDynamic: 'fixed',
                    name: '文件',
                    options: OptionsHelp.getOptions(type),
                };
            case 'email':
                return {
                    compare: '等于',
                    isEmpty: false,
                    type: 'email',
                    lianjieci: '且',
                    value: '',
                    isDynamic: 'fixed',
                    name: '邮箱',
                    options: OptionsHelp.getOptions(type),
                };
            case 'valnum':
                return {
                    compare: 'IN_RANGE',
                    isEmpty: false,
                    type: 'valnum',
                    lianjieci: '且',
                    value: '',
                    isDynamic: 'fixed',
                    name: '数值',
                    options: OptionsHelp.getOptions(type),
                };
            case 'amount':
                return {
                    compare: 'IN_RANGE',
                    isEmpty: false,
                    type: 'amount',
                    lianjieci: '且',
                    value: '',
                    isDynamic: 'fixed',
                    name: '金额',
                    options: OptionsHelp.getOptions(type),
                };
            case 'phone':
                return {
                    compare: '等于',
                    isEmpty: false,
                    type: 'phone',
                    lianjieci: '且',
                    value: '',
                    isDynamic: 'fixed',
                    name: '电话',
                    options: OptionsHelp.getOptions(type),
                };
            case 'time':
                return {
                    compare: '等于',
                    isEmpty: false,
                    type: 'time',
                    lianjieci: '且',
                    value: '',
                    isDynamic: 'fixed',
                    name: '时间',
                    options: OptionsHelp.getOptions(type),
                };
            case 'data':
                return {
                    compare: 'LESS_THAN_OR_EQUAL',
                    isEmpty: false,
                    type: 'data',
                    lianjieci: '且',
                    value: '',
                    value1: '',
                    isDynamic: 'fixed',
                    name: '日期',
                    options: OptionsHelp.getOptions(type),
                    datalist: OptionsHelp.getDataList(type),
                };
            case 'area':
                return {
                    compare: 'IS_ONE_OF',
                    isEmpty: false,
                    type: 'area',
                    lianjieci: '且',
                    value: '',
                    value1: '',
                    isDynamic: 'fixed',
                    name: '地区',
                    options: OptionsHelp.getOptions(type),
                };
            default:
                return {};
        }

    }
}


//获取不同类型的options列表
export class OptionsHelp {

    static getOptions(type: string): selectOptions[] {
        switch (type) {
            case 'input':
                return (Object.keys(ComparisonOperator1) as (keyof typeof ComparisonOperator1)[])
                    .map(key => ({
                        value: key,
                        label: ComparisonOperator1[key]
                    }));
            case 'email':
                return (Object.keys(ComparisonOperator1) as (keyof typeof ComparisonOperator1)[])
                    .map(key => ({
                        value: key,
                        label: ComparisonOperator1[key]
                    }));
            case 'valnum':
                return (Object.keys(ComparisonOperator2) as (keyof typeof ComparisonOperator2)[])
                    .map(key => ({
                        value: key,
                        label: ComparisonOperator2[key]
                    }));
            case 'amount':
                return (Object.keys(ComparisonOperator2) as (keyof typeof ComparisonOperator2)[])
                    .map(key => ({
                        value: key,
                        label: ComparisonOperator2[key]
                    }));
            case 'phone':
                return (Object.keys(ComparisonOperator3) as (keyof typeof ComparisonOperator3)[])
                    .map(key => ({
                        value: key,
                        label: ComparisonOperator3[key]
                    }));
            case 'time':
            case 'data':
                return (Object.keys(ComparisonOperator4) as (keyof typeof ComparisonOperator4)[])
                    .map(key => ({
                        value: key,
                        label: ComparisonOperator4[key]
                    }));
            case 'area':
                return (Object.keys(ComparisonOperator5) as (keyof typeof ComparisonOperator5)[])
                    .map(key => ({
                        value: key,
                        label: ComparisonOperator5[key]
                    }));
            default:
                return [];
        }

    }
    static getDataList(type: string): selectOptions[] {
        switch (type) {
            case 'data':
                return (Object.keys(TimePeriod) as (keyof typeof TimePeriod)[])
                    .map(key => ({
                        value: key,
                        label: TimePeriod[key]
                    }));
            default:
                return [];
        }
    }


}
