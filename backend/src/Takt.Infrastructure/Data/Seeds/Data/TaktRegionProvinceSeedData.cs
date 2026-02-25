// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktRegionProvinceSeedData.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：中国 34 个省/自治区/直辖市/特别行政区种子数据。数据来源仅采用国标 GB/T 2260、国家统计局（https://www.stats.gov.cn/sj/）、民政部（http://www.mca.gov.cn/）行政区划，不得使用维基百科。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Region;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 中国省/自治区/直辖市/特别行政区种子数据（34 条）
/// </summary>
public class TaktRegionProvinceSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（州省在国家地区之后初始化）
    /// </summary>
    public int Order => 103;

    /// <summary>
    /// 初始化中国 34 省级行政区种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var countryRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktRegionCountry>>();
        var provinceRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktRegionProvince>>();

        var china = await countryRepository.GetAsync(x => x.RegionCode == "CN");
        if (china == null)
        {
            return (0, 0);
        }

        int insertCount = 0;
        int updateCount = 0;
        var list = GetProvinceList();

        foreach (var p in list)
        {
            var existing = await provinceRepository.GetAsync(x => x.CountryId == china.Id && x.RegionCode == p.RegionCode);

            if (existing == null)
            {
                var entity = new TaktRegionProvince
                {
                    CountryId = china.Id,
                    RegionCode = p.RegionCode,
                    RegionName = p.RegionName,
                    ShortName = p.ShortName,
                    RegionLevel = 2,
                    OrderNum = p.OrderNum,
                    IsDeleted = 0
                };
                await provinceRepository.CreateAsync(entity);
                insertCount++;
            }
            else
            {
                existing.RegionName = p.RegionName;
                existing.ShortName = p.ShortName;
                existing.OrderNum = p.OrderNum;
                await provinceRepository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 返回中国 34 省级行政区列表（国标 6 位代码、名称、简称）
    /// </summary>
    private static List<ProvinceSeedItem> GetProvinceList()
    {
        return new List<ProvinceSeedItem>
        {
            new("110000", "北京市", "京", 1),
            new("120000", "天津市", "津", 2),
            new("130000", "河北省", "冀", 3),
            new("140000", "山西省", "晋", 4),
            new("150000", "内蒙古自治区", "蒙", 5),
            new("210000", "辽宁省", "辽", 6),
            new("220000", "吉林省", "吉", 7),
            new("230000", "黑龙江省", "黑", 8),
            new("310000", "上海市", "沪", 9),
            new("320000", "江苏省", "苏", 10),
            new("330000", "浙江省", "浙", 11),
            new("340000", "安徽省", "皖", 12),
            new("350000", "福建省", "闽", 13),
            new("360000", "江西省", "赣", 14),
            new("370000", "山东省", "鲁", 15),
            new("410000", "河南省", "豫", 16),
            new("420000", "湖北省", "鄂", 17),
            new("430000", "湖南省", "湘", 18),
            new("440000", "广东省", "粤", 19),
            new("450000", "广西壮族自治区", "桂", 20),
            new("460000", "海南省", "琼", 21),
            new("500000", "重庆市", "渝", 22),
            new("510000", "四川省", "川", 23),
            new("520000", "贵州省", "黔", 24),
            new("530000", "云南省", "云", 25),
            new("540000", "西藏自治区", "藏", 26),
            new("610000", "陕西省", "陕", 27),
            new("620000", "甘肃省", "甘", 28),
            new("630000", "青海省", "青", 29),
            new("640000", "宁夏回族自治区", "宁", 30),
            new("650000", "新疆维吾尔自治区", "新", 31),
            new("710000", "台湾省", "台", 32),
            new("810000", "香港特别行政区", "港", 33),
            new("820000", "澳门特别行政区", "澳", 34),
        };
    }

    /// <summary>
    /// 省级种子项（行政区划代码、名称、简称、排序）
    /// </summary>
    private sealed record ProvinceSeedItem(string RegionCode, string RegionName, string? ShortName, int OrderNum);
}
