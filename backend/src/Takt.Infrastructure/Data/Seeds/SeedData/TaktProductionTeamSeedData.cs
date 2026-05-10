// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktProductionTeamSeedData.cs
// 创建时间：2026-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：TaktProductionTeam 实体种子数据，初始化示例生产班组主数据（用于替代原 prod_team_category 字典）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Logistics.Manufacturing.Output;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// Takt 生产班组种子数据
/// </summary>
public class TaktProductionTeamSeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（放在部门等组织结构之后，生产相关种子之前）
    /// </summary>
    public int Order => 25;

    /// <summary>
    /// 初始化生产班组种子数据
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var teamRepository = serviceProvider.GetRequiredService<ITaktRepository<TaktProductionTeam>>();

        int insertCount = 0;
        int updateCount = 0;

        // 说明：
        // 1. 这里初始化原 prod_team_category 中的生产班组列表，按业务顺序排列；
        // 2. PlantCode 使用实际工厂代码 "C100"，生产线与班次信息如后续有精细规则，可再扩展。

        // 按业务顺序初始化：1~15班 → 加工班 → SMT → 自插 → 手插 → 修正 → IQC → QA
        
        // 1~15班（M=组立）
        var (i1, u1) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "1班",   "1班",   "M", "组立", null); insertCount += i1; updateCount += u1;
        var (i2, u2) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2班",   "2班",   "M", "组立", null); insertCount += i2; updateCount += u2;
        var (i3, u3) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3班",   "3班",   "M", "组立", null); insertCount += i3; updateCount += u3;
        var (i4, u4) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4班",   "4班",   "M", "组立", null); insertCount += i4; updateCount += u4;
        var (i5, u5) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "5班",   "5班",   "M", "组立", null); insertCount += i5; updateCount += u5;
        var (i6, u6) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "6班",   "6班",   "M", "组立", null); insertCount += i6; updateCount += u6;
        var (i7, u7) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "7班",   "7班",   "M", "组立", null); insertCount += i7; updateCount += u7;
        var (i8, u8) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "8班",   "8班",   "M", "组立", null); insertCount += i8; updateCount += u8;
        var (i9, u9) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "9班",   "9班",   "M", "组立", null); insertCount += i9; updateCount += u9;
        var (i10, u10) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "10班",  "10班",  "M", "组立", null); insertCount += i10; updateCount += u10;
        var (i11, u11) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "11班",  "11班",  "M", "组立", null); insertCount += i11; updateCount += u11;
        var (i12, u12) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "12班",  "12班",  "M", "组立", null); insertCount += i12; updateCount += u12;
        var (i13, u13) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "13班",  "13班",  "M", "组立", null); insertCount += i13; updateCount += u13;
        var (i14, u14) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "14班",  "14班",  "M", "组立", null); insertCount += i14; updateCount += u14;
        var (i15, u15) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "15班",  "15班",  "M", "组立", null); insertCount += i15; updateCount += u15;

        // 加工班（M=组立）
        var (iProc, uProc) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "加工班", "加工班", "M", "组立", null); insertCount += iProc; updateCount += uProc;

        // SMT生产线（S=SMT）
        var (i1smt1, u1smt1) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "1SMT1", "1SMT1", "S", "SMT", null); insertCount += i1smt1; updateCount += u1smt1;
        var (i1smt2, u1smt2) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "1SMT2", "1SMT2", "S", "SMT", null); insertCount += i1smt2; updateCount += u1smt2;

        // 2自插A/B/C/D（P=PCBA）
        var (i2a, u2a) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插A", "2自插A", "P", "PCBA", null); insertCount += i2a; updateCount += u2a;
        var (i2b, u2b) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插B", "2自插B", "P", "PCBA", null); insertCount += i2b; updateCount += u2b;
        var (i2c, u2c) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插C", "2自插C", "P", "PCBA", null); insertCount += i2c; updateCount += u2c;
        var (i2d, u2d) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "2自插D", "2自插D", "P", "PCBA", null); insertCount += i2d; updateCount += u2d;

        // 3手插A/B/C（P=PCBA）
        var (i3a, u3a) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3手插A", "3手插A", "P", "PCBA", null); insertCount += i3a; updateCount += u3a;
        var (i3b, u3b) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3手插B", "3手插B", "P", "PCBA", null); insertCount += i3b; updateCount += u3b;
        var (i3c, u3c) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "3手插C", "3手插C", "P", "PCBA", null); insertCount += i3c; updateCount += u3c;

        // 4修正A/B/C（P=PCBA）
        var (i4a, u4a) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4修正A", "4修正A", "P", "PCBA", null); insertCount += i4a; updateCount += u4a;
        var (i4b, u4b) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4修正B", "4修正B", "P", "PCBA", null); insertCount += i4b; updateCount += u4b;
        var (i4c, u4c) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "4修正C", "4修正C", "P", "PCBA", null); insertCount += i4c; updateCount += u4c;

        // IQC, QA（Q=质检）
        var (iIqc, uIqc) = await CreateOrUpdateTeamAsync(teamRepository, "C100", "IQC",   "IQC",   "Q", "质检", null); insertCount += iIqc; updateCount += uIqc;
        var (iQa,  uQa)  = await CreateOrUpdateTeamAsync(teamRepository, "C100", "QA",    "QA",    "Q", "质检", null); insertCount += iQa;  updateCount += uQa;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 创建或更新生产班组
    /// </summary>
    private static async Task<(int InsertCount, int UpdateCount)> CreateOrUpdateTeamAsync(
        ITaktRepository<TaktProductionTeam> teamRepository,
        string plantCode,
        string teamCode,
        string teamName,
        string? teamCategory,
        string? teamCategoryName,
        int? shiftNo)
    {
        var team = await teamRepository.GetAsync(t => t.PlantCode == plantCode && t.TeamCode == teamCode);
        if (team == null)
        {
            team = new TaktProductionTeam
            {
                PlantCode = plantCode,
                TeamCode = teamCode,
                TeamName = teamName,
                TeamCategory = teamCategory,
                TeamCategoryName = teamCategoryName,
                ProductionLine = teamCode, // 默认生产线 = 班组编码
                ShiftNo = shiftNo,
                Status = 1, // 1=启用
                IsDeleted = 0
            };
            await teamRepository.CreateAsync(team);
            return (1, 0);
        }

        team.TeamName = teamName;
        team.TeamCategory = teamCategory;
        team.TeamCategoryName = teamCategoryName;
        team.ProductionLine = teamCode; // 默认生产线 = 班组编码
        team.ShiftNo = shiftNo;
        team.Status = 1; // 1=启用
        await teamRepository.UpdateAsync(team);
        return (0, 1);
    }
}

