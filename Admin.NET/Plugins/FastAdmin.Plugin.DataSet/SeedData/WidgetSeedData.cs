using FastAdmin.Plugin.DataSet.Entity;
using FastAdmin.Plugin.DataSet.Enum;

namespace FastAdmin.Plugin.DataSet.SeedData;

/// <summary>
/// 系统用户表种子数据
/// </summary>
public class WidgetSeedData : ISqlSugarEntitySeedData<Widget>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Widget> HasData()
    {
        var data = new List<Widget>
        {
            new Widget
            {
                Name = "tab表格",
                Ico="",
                VueComponentName = "tab_table",
                Description = "头顶带有tab选项卡的tab,可以自由增加不同列表",
                WidgetType = WidgetType.TabSearchList
            }
        };
        return data;
    }
}