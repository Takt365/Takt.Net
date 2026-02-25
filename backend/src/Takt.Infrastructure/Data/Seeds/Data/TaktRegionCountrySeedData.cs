// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktRegionCountrySeedData.cs
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：国家地区种子数据，初始化 ISO 3166-1 国家地区（195 条）。数据来源采用国际官方标准，不得使用维基百科。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.Routine.Region;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 国家地区种子数据
/// </summary>
public class TaktRegionCountrySeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序（国家地区在字典、语言之后初始化）
    /// </summary>
    public int Order => 102;

    /// <summary>
    /// 初始化国家地区种子数据
    /// </summary>
    /// <param name="serviceProvider">服务提供者</param>
    /// <param name="configId">当前数据库配置ID</param>
    /// <returns>返回插入和更新的记录数（插入数, 更新数）</returns>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var repository = serviceProvider.GetRequiredService<ITaktRepository<TaktRegionCountry>>();

        int insertCount = 0;
        int updateCount = 0;

        // 定义国家地区数据（ISO 3166-1：Alpha-2、Alpha-3、数字代码、名称等；195 条）
        var countries = GetCountryList();

        foreach (var c in countries)
        {
            var existing = await repository.GetAsync(x => x.RegionCode == c.RegionCode);

            if (existing == null)
            {
                var entity = new TaktRegionCountry
                {
                    RegionCode = c.RegionCode,
                    Alpha3Code = c.Alpha3Code,
                    NumericCode = c.NumericCode,
                    FullName = c.FullName,
                    RegionName = c.RegionName,
                    EnglishName = c.EnglishName,
                    ShortName = c.ShortName,
                    Capital = c.Capital,
                    Continent = c.Continent,
                    RegionType = c.RegionType,
                    CountryDomain = c.CountryDomain,
                    AreaCode = c.AreaCode,
                    TimeZone = c.TimeZone,
                    CurrencyCode = c.CurrencyCode,
                    LanguageCode = c.LanguageCode,
                    RegionLevel = 1,
                    OrderNum = c.OrderNum,
                    IsDeleted = 0
                };
                await repository.CreateAsync(entity);
                insertCount++;
            }
            else
            {
                existing.Alpha3Code = c.Alpha3Code;
                existing.NumericCode = c.NumericCode;
                existing.FullName = c.FullName;
                existing.RegionName = c.RegionName;
                existing.EnglishName = c.EnglishName;
                existing.ShortName = c.ShortName;
                existing.Capital = c.Capital;
                existing.Continent = c.Continent;
                existing.RegionType = c.RegionType;
                existing.CountryDomain = c.CountryDomain;
                existing.AreaCode = c.AreaCode;
                existing.TimeZone = c.TimeZone;
                existing.CurrencyCode = c.CurrencyCode;
                existing.LanguageCode = c.LanguageCode;
                existing.OrderNum = c.OrderNum;
                await repository.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 返回国家地区列表（195 条，与现有种子风格一致：匿名类型改为具名结构，便于维护）
    /// </summary>
    private static List<CountrySeedItem> GetCountryList()
    {
        return new List<CountrySeedItem>
        {
            new("AF", "AFG", 4, "阿富汗伊斯兰共和国", "阿富汗", "Afghanistan", "阿", "亚洲", "Kabul", 0, ".af", "+93", "Asia/Kabul", "AFN", "fa-AF", 1),
            new("AL", "ALB", 8, "阿尔巴尼亚共和国", "阿尔巴尼亚", "Albania", "阿", "欧洲", "Tirana", 0, ".al", "+355", "Europe/Tirane", "ALL", "sq", 2),
            new("DZ", "DZA", 12, "阿尔及利亚民主人民共和国", "阿尔及利亚", "Algeria", "阿", "非洲", "Algiers", 0, ".dz", "+213", "Africa/Algiers", "DZD", "ar-DZ", 3),
            new("AD", "AND", 20, "安道尔公国", "安道尔", "Andorra", "安", "欧洲", "Andorra la Vella", 0, ".ad", "+376", "Europe/Andorra", "EUR", "ca", 4),
            new("AO", "AGO", 24, "安哥拉共和国", "安哥拉", "Angola", "安", "非洲", "Luanda", 0, ".ao", "+244", "Africa/Luanda", "AOA", "pt-AO", 5),
            new("AG", "ATG", 28, "安提瓜和巴布达", "安提瓜和巴布达", "Antigua and Barbuda", "安", "北美洲", "Saint John's", 0, ".ag", "+1268", "America/Antigua", "XCD", "en-AG", 6),
            new("AR", "ARG", 32, "阿根廷共和国", "阿根廷", "Argentina", "阿", "南美洲", "Buenos Aires", 0, ".ar", "+54", "America/Argentina/Buenos_Aires", "ARS", "es-AR", 7),
            new("AM", "ARM", 51, "亚美尼亚共和国", "亚美尼亚", "Armenia", "亚", "亚洲", "Yerevan", 0, ".am", "+374", "Asia/Yerevan", "AMD", "hy", 8),
            new("AU", "AUS", 36, "澳大利亚联邦", "澳大利亚", "Australia", "澳", "大洋洲", "Canberra", 0, ".au", "+61", "Australia/Sydney", "AUD", "en-AU", 9),
            new("AT", "AUT", 40, "奥地利共和国", "奥地利", "Austria", "奥", "欧洲", "Vienna", 0, ".at", "+43", "Europe/Vienna", "EUR", "de-AT", 10),
            new("AZ", "AZE", 31, "阿塞拜疆共和国", "阿塞拜疆", "Azerbaijan", "阿", "亚洲", "Baku", 0, ".az", "+994", "Asia/Baku", "AZN", "az", 11),
            new("BS", "BHS", 44, "巴哈马国", "巴哈马", "Bahamas", "巴", "北美洲", "Nassau", 0, ".bs", "+1242", "America/Nassau", "BSD", "en-BS", 12),
            new("BH", "BHR", 48, "巴林王国", "巴林", "Bahrain", "巴", "亚洲", "Manama", 0, ".bh", "+973", "Asia/Bahrain", "BHD", "ar-BH", 13),
            new("BD", "BGD", 50, "孟加拉人民共和国", "孟加拉国", "Bangladesh", "孟", "亚洲", "Dhaka", 0, ".bd", "+880", "Asia/Dhaka", "BDT", "bn-BD", 14),
            new("BB", "BRB", 52, "巴巴多斯", "巴巴多斯", "Barbados", "巴", "北美洲", "Bridgetown", 0, ".bb", "+1246", "America/Barbados", "BBD", "en-BB", 15),
            new("BY", "BLR", 112, "白俄罗斯共和国", "白俄罗斯", "Belarus", "白", "欧洲", "Minsk", 0, ".by", "+375", "Europe/Minsk", "BYN", "be", 16),
            new("BE", "BEL", 56, "比利时王国", "比利时", "Belgium", "比", "欧洲", "Brussels", 0, ".be", "+32", "Europe/Brussels", "EUR", "nl-BE", 17),
            new("BZ", "BLZ", 84, "伯利兹", "伯利兹", "Belize", "伯", "北美洲", "Belmopan", 0, ".bz", "+501", "America/Belize", "BZD", "en-BZ", 18),
            new("BJ", "BEN", 204, "贝宁共和国", "贝宁", "Benin", "贝", "非洲", "Porto-Novo", 0, ".bj", "+229", "Africa/Porto-Novo", "XOF", "fr-BJ", 19),
            new("BT", "BTN", 64, "不丹王国", "不丹", "Bhutan", "不", "亚洲", "Thimphu", 0, ".bt", "+975", "Asia/Thimphu", "BTN", "dz", 20),
            new("BO", "BOL", 68, "玻利维亚多民族国", "玻利维亚", "Bolivia", "玻", "南美洲", "Sucre", 0, ".bo", "+591", "America/La_Paz", "BOB", "es-BO", 21),
            new("BA", "BIH", 70, "波斯尼亚和黑塞哥维那", "波黑", "Bosnia and Herzegovina", "波", "欧洲", "Sarajevo", 0, ".ba", "+387", "Europe/Sarajevo", "BAM", "bs", 22),
            new("BW", "BWA", 72, "博茨瓦纳共和国", "博茨瓦纳", "Botswana", "博", "非洲", "Gaborone", 0, ".bw", "+267", "Africa/Gaborone", "BWP", "en-BW", 23),
            new("BR", "BRA", 76, "巴西联邦共和国", "巴西", "Brazil", "巴", "南美洲", "Brasília", 0, ".br", "+55", "America/Sao_Paulo", "BRL", "pt-BR", 24),
            new("BN", "BRN", 96, "文莱达鲁萨兰国", "文莱", "Brunei", "文", "亚洲", "Bandar Seri Begawan", 0, ".bn", "+673", "Asia/Brunei", "BND", "ms-BN", 25),
            new("BG", "BGR", 100, "保加利亚共和国", "保加利亚", "Bulgaria", "保", "欧洲", "Sofia", 0, ".bg", "+359", "Europe/Sofia", "BGN", "bg", 26),
            new("BF", "BFA", 854, "布基纳法索", "布基纳法索", "Burkina Faso", "布", "非洲", "Ouagadougou", 0, ".bf", "+226", "Africa/Ouagadougou", "XOF", "fr-BF", 27),
            new("BI", "BDI", 108, "布隆迪共和国", "布隆迪", "Burundi", "布", "非洲", "Gitega", 0, ".bi", "+257", "Africa/Bujumbura", "BIF", "fr-BI", 28),
            new("CV", "CPV", 132, "佛得角共和国", "佛得角", "Cabo Verde", "佛", "非洲", "Praia", 0, ".cv", "+238", "Atlantic/Cape_Verde", "CVE", "pt-CV", 29),
            new("KH", "KHM", 116, "柬埔寨王国", "柬埔寨", "Cambodia", "柬", "亚洲", "Phnom Penh", 0, ".kh", "+855", "Asia/Phnom_Penh", "KHR", "km", 30),
            new("CM", "CMR", 120, "喀麦隆共和国", "喀麦隆", "Cameroon", "喀", "非洲", "Yaoundé", 0, ".cm", "+237", "Africa/Douala", "XAF", "fr-CM", 31),
            new("CA", "CAN", 124, "加拿大", "加拿大", "Canada", "加", "北美洲", "Ottawa", 0, ".ca", "+1", "America/Toronto", "CAD", "en-CA", 32),
            new("CF", "CAF", 140, "中非共和国", "中非", "Central African Republic", "中", "非洲", "Bangui", 0, ".cf", "+236", "Africa/Bangui", "XAF", "fr-CF", 33),
            new("TD", "TCD", 148, "乍得共和国", "乍得", "Chad", "乍", "非洲", "N'Djamena", 0, ".td", "+235", "Africa/Ndjamena", "XAF", "fr-TD", 34),
            new("CL", "CHL", 152, "智利共和国", "智利", "Chile", "智", "南美洲", "Santiago", 0, ".cl", "+56", "America/Santiago", "CLP", "es-CL", 35),
            new("CN", "CHN", 156, "中华人民共和国", "中国", "China", "中", "亚洲", "北京", 0, ".cn", "+86", "Asia/Shanghai", "CNY", "zh-CN", 36),
            new("CO", "COL", 170, "哥伦比亚共和国", "哥伦比亚", "Colombia", "哥", "南美洲", "Bogotá", 0, ".co", "+57", "America/Bogota", "COP", "es-CO", 37),
            new("KM", "COM", 174, "科摩罗联盟", "科摩罗", "Comoros", "科", "非洲", "Moroni", 0, ".km", "+269", "Indian/Comoro", "KMF", "ar-KM", 38),
            new("CG", "COG", 178, "刚果共和国", "刚果(布)", "Congo", "刚", "非洲", "Brazzaville", 0, ".cg", "+242", "Africa/Brazzaville", "XAF", "fr-CG", 39),
            new("CD", "COD", 180, "刚果民主共和国", "刚果(金)", "Congo (Democratic Republic)", "刚", "非洲", "Kinshasa", 0, ".cd", "+243", "Africa/Kinshasa", "CDF", "fr-CD", 40),
            new("CR", "CRI", 188, "哥斯达黎加共和国", "哥斯达黎加", "Costa Rica", "哥", "北美洲", "San José", 0, ".cr", "+506", "America/Costa_Rica", "CRC", "es-CR", 41),
            new("CI", "CIV", 384, "科特迪瓦共和国", "科特迪瓦", "Côte d'Ivoire", "科", "非洲", "Yamoussoukro", 0, ".ci", "+225", "Africa/Abidjan", "XOF", "fr-CI", 42),
            new("HR", "HRV", 191, "克罗地亚共和国", "克罗地亚", "Croatia", "克", "欧洲", "Zagreb", 0, ".hr", "+385", "Europe/Zagreb", "EUR", "hr", 43),
            new("CU", "CUB", 192, "古巴共和国", "古巴", "Cuba", "古", "北美洲", "Havana", 0, ".cu", "+53", "America/Havana", "CUP", "es-CU", 44),
            new("CY", "CYP", 196, "塞浦路斯共和国", "塞浦路斯", "Cyprus", "塞", "亚洲", "Nicosia", 0, ".cy", "+357", "Asia/Nicosia", "EUR", "el-CY", 45),
            new("CZ", "CZE", 203, "捷克共和国", "捷克", "Czechia", "捷", "欧洲", "Prague", 0, ".cz", "+420", "Europe/Prague", "CZK", "cs", 46),
            new("DK", "DNK", 208, "丹麦王国", "丹麦", "Denmark", "丹", "欧洲", "Copenhagen", 0, ".dk", "+45", "Europe/Copenhagen", "DKK", "da-DK", 47),
            new("DJ", "DJI", 262, "吉布提共和国", "吉布提", "Djibouti", "吉", "非洲", "Djibouti", 0, ".dj", "+253", "Africa/Djibouti", "DJF", "fr-DJ", 48),
            new("DM", "DMA", 212, "多米尼克国", "多米尼克", "Dominica", "多", "北美洲", "Roseau", 0, ".dm", "+1767", "America/Dominica", "XCD", "en-DM", 49),
            new("DO", "DOM", 214, "多米尼加共和国", "多米尼加", "Dominican Republic", "多", "北美洲", "Santo Domingo", 0, ".do", "+1809", "America/Santo_Domingo", "DOP", "es-DO", 50),
            new("EC", "ECU", 218, "厄瓜多尔共和国", "厄瓜多尔", "Ecuador", "厄", "南美洲", "Quito", 0, ".ec", "+593", "America/Guayaquil", "USD", "es-EC", 51),
            new("EG", "EGY", 818, "阿拉伯埃及共和国", "埃及", "Egypt", "埃", "非洲", "Cairo", 0, ".eg", "+20", "Africa/Cairo", "EGP", "ar-EG", 52),
            new("SV", "SLV", 222, "萨尔瓦多共和国", "萨尔瓦多", "El Salvador", "萨", "北美洲", "San Salvador", 0, ".sv", "+503", "America/El_Salvador", "USD", "es-SV", 53),
            new("GQ", "GNQ", 226, "赤道几内亚共和国", "赤道几内亚", "Equatorial Guinea", "赤", "非洲", "Malabo", 0, ".gq", "+240", "Africa/Malabo", "XAF", "es-GQ", 54),
            new("ER", "ERI", 232, "厄立特里亚国", "厄立特里亚", "Eritrea", "厄", "非洲", "Asmara", 0, ".er", "+291", "Africa/Asmara", "ERN", "ti-ER", 55),
            new("EE", "EST", 233, "爱沙尼亚共和国", "爱沙尼亚", "Estonia", "爱", "欧洲", "Tallinn", 0, ".ee", "+372", "Europe/Tallinn", "EUR", "et", 56),
            new("SZ", "SWZ", 748, "斯威士兰王国", "斯威士兰", "Eswatini", "斯", "非洲", "Mbabane", 0, ".sz", "+268", "Africa/Mbabane", "SZL", "en-SZ", 57),
            new("ET", "ETH", 231, "埃塞俄比亚联邦民主共和国", "埃塞俄比亚", "Ethiopia", "埃", "非洲", "Addis Ababa", 0, ".et", "+251", "Africa/Addis_Ababa", "ETB", "am", 58),
            new("FJ", "FJI", 242, "斐济共和国", "斐济", "Fiji", "斐", "大洋洲", "Suva", 0, ".fj", "+679", "Pacific/Fiji", "FJD", "en-FJ", 59),
            new("FI", "FIN", 246, "芬兰共和国", "芬兰", "Finland", "芬", "欧洲", "Helsinki", 0, ".fi", "+358", "Europe/Helsinki", "EUR", "fi", 60),
            new("FR", "FRA", 250, "法兰西共和国", "法国", "France", "法", "欧洲", "Paris", 0, ".fr", "+33", "Europe/Paris", "EUR", "fr-FR", 61),
            new("GA", "GAB", 266, "加蓬共和国", "加蓬", "Gabon", "加", "非洲", "Libreville", 0, ".ga", "+241", "Africa/Libreville", "XAF", "fr-GA", 62),
            new("GM", "GMB", 270, "冈比亚共和国", "冈比亚", "Gambia", "冈", "非洲", "Banjul", 0, ".gm", "+220", "Africa/Banjul", "GMD", "en-GM", 63),
            new("GE", "GEO", 268, "格鲁吉亚", "格鲁吉亚", "Georgia", "格", "亚洲", "Tbilisi", 0, ".ge", "+995", "Asia/Tbilisi", "GEL", "ka", 64),
            new("DE", "DEU", 276, "德意志联邦共和国", "德国", "Germany", "德", "欧洲", "Berlin", 0, ".de", "+49", "Europe/Berlin", "EUR", "de-DE", 65),
            new("GH", "GHA", 288, "加纳共和国", "加纳", "Ghana", "加", "非洲", "Accra", 0, ".gh", "+233", "Africa/Accra", "GHS", "en-GH", 66),
            new("GR", "GRC", 300, "希腊共和国", "希腊", "Greece", "希", "欧洲", "Athens", 0, ".gr", "+30", "Europe/Athens", "EUR", "el-GR", 67),
            new("GD", "GRD", 308, "格林纳达", "格林纳达", "Grenada", "格", "北美洲", "Saint George's", 0, ".gd", "+1473", "America/Grenada", "XCD", "en-GD", 68),
            new("GT", "GTM", 320, "危地马拉共和国", "危地马拉", "Guatemala", "危", "北美洲", "Guatemala City", 0, ".gt", "+502", "America/Guatemala", "GTQ", "es-GT", 69),
            new("GN", "GIN", 324, "几内亚共和国", "几内亚", "Guinea", "几", "非洲", "Conakry", 0, ".gn", "+224", "Africa/Conakry", "GNF", "fr-GN", 70),
            new("GW", "GNB", 624, "几内亚比绍共和国", "几内亚比绍", "Guinea-Bissau", "几", "非洲", "Bissau", 0, ".gw", "+245", "Africa/Bissau", "XOF", "pt-GW", 71),
            new("GY", "GUY", 328, "圭亚那合作共和国", "圭亚那", "Guyana", "圭", "南美洲", "Georgetown", 0, ".gy", "+592", "America/Guyana", "GYD", "en-GY", 72),
            new("HT", "HTI", 332, "海地共和国", "海地", "Haiti", "海", "北美洲", "Port-au-Prince", 0, ".ht", "+509", "America/Port-au-Prince", "HTG", "fr-HT", 73),
            new("VA", "VAT", 336, "梵蒂冈城国", "梵蒂冈", "Holy See", "梵", "欧洲", "Vatican City", 0, ".va", "+379", "Europe/Vatican", "EUR", "la", 74),
            new("HN", "HND", 340, "洪都拉斯共和国", "洪都拉斯", "Honduras", "洪", "北美洲", "Tegucigalpa", 0, ".hn", "+504", "America/Tegucigalpa", "HNL", "es-HN", 75),
            new("HU", "HUN", 348, "匈牙利", "匈牙利", "Hungary", "匈", "欧洲", "Budapest", 0, ".hu", "+36", "Europe/Budapest", "HUF", "hu", 76),
            new("IS", "ISL", 352, "冰岛共和国", "冰岛", "Iceland", "冰", "欧洲", "Reykjavik", 0, ".is", "+354", "Atlantic/Reykjavik", "ISK", "is", 77),
            new("IN", "IND", 356, "印度共和国", "印度", "India", "印", "亚洲", "New Delhi", 0, ".in", "+91", "Asia/Kolkata", "INR", "hi-IN", 78),
            new("ID", "IDN", 360, "印度尼西亚共和国", "印度尼西亚", "Indonesia", "印", "亚洲", "Jakarta", 0, ".id", "+62", "Asia/Jakarta", "IDR", "id", 79),
            new("IR", "IRN", 364, "伊朗伊斯兰共和国", "伊朗", "Iran", "伊", "亚洲", "Tehran", 0, ".ir", "+98", "Asia/Tehran", "IRR", "fa-IR", 80),
            new("IQ", "IRQ", 368, "伊拉克共和国", "伊拉克", "Iraq", "伊", "亚洲", "Baghdad", 0, ".iq", "+964", "Asia/Baghdad", "IQD", "ar-IQ", 81),
            new("IE", "IRL", 372, "爱尔兰", "爱尔兰", "Ireland", "爱", "欧洲", "Dublin", 0, ".ie", "+353", "Europe/Dublin", "EUR", "en-IE", 82),
            new("IL", "ISR", 376, "以色列国", "以色列", "Israel", "以", "亚洲", "Jerusalem", 0, ".il", "+972", "Asia/Jerusalem", "ILS", "he", 83),
            new("IT", "ITA", 380, "意大利共和国", "意大利", "Italy", "意", "欧洲", "Rome", 0, ".it", "+39", "Europe/Rome", "EUR", "it-IT", 84),
            new("JM", "JAM", 388, "牙买加", "牙买加", "Jamaica", "牙", "北美洲", "Kingston", 0, ".jm", "+1876", "America/Jamaica", "JMD", "en-JM", 85),
            new("JP", "JPN", 392, "日本国", "日本", "Japan", "日", "亚洲", "东京", 0, ".jp", "+81", "Asia/Tokyo", "JPY", "ja", 86),
            new("JO", "JOR", 400, "约旦哈希姆王国", "约旦", "Jordan", "约", "亚洲", "Amman", 0, ".jo", "+962", "Asia/Amman", "JOD", "ar-JO", 87),
            new("KZ", "KAZ", 398, "哈萨克斯坦共和国", "哈萨克斯坦", "Kazakhstan", "哈", "亚洲", "Astana", 0, ".kz", "+7", "Asia/Almaty", "KZT", "kk", 88),
            new("KE", "KEN", 404, "肯尼亚共和国", "肯尼亚", "Kenya", "肯", "非洲", "Nairobi", 0, ".ke", "+254", "Africa/Nairobi", "KES", "en-KE", 89),
            new("KI", "KIR", 296, "基里巴斯共和国", "基里巴斯", "Kiribati", "基", "大洋洲", "Tarawa", 0, ".ki", "+686", "Pacific/Tarawa", "AUD", "en-KI", 90),
            new("KP", "PRK", 408, "朝鲜民主主义人民共和国", "朝鲜", "Korea (North)", "朝", "亚洲", "平壤", 0, ".kp", "+850", "Asia/Pyongyang", "KPW", "ko-KP", 91),
            new("KR", "KOR", 410, "大韩民国", "韩国", "Korea (South)", "韩", "亚洲", "首尔", 0, ".kr", "+82", "Asia/Seoul", "KRW", "ko-KR", 92),
            new("KW", "KWT", 414, "科威特国", "科威特", "Kuwait", "科", "亚洲", "Kuwait City", 0, ".kw", "+965", "Asia/Kuwait", "KWD", "ar-KW", 93),
            new("KG", "KGZ", 417, "吉尔吉斯斯坦共和国", "吉尔吉斯斯坦", "Kyrgyzstan", "吉", "亚洲", "Bishkek", 0, ".kg", "+996", "Asia/Bishkek", "KGS", "ky", 94),
            new("LA", "LAO", 418, "老挝人民民主共和国", "老挝", "Laos", "老", "亚洲", "Vientiane", 0, ".la", "+856", "Asia/Vientiane", "LAK", "lo", 95),
            new("LV", "LVA", 428, "拉脱维亚共和国", "拉脱维亚", "Latvia", "拉", "欧洲", "Riga", 0, ".lv", "+371", "Europe/Riga", "EUR", "lv", 96),
            new("LB", "LBN", 422, "黎巴嫩共和国", "黎巴嫩", "Lebanon", "黎", "亚洲", "Beirut", 0, ".lb", "+961", "Asia/Beirut", "LBP", "ar-LB", 97),
            new("LS", "LSO", 426, "莱索托王国", "莱索托", "Lesotho", "莱", "非洲", "Maseru", 0, ".ls", "+266", "Africa/Maseru", "LSL", "en-LS", 98),
            new("LR", "LBR", 430, "利比里亚共和国", "利比里亚", "Liberia", "利", "非洲", "Monrovia", 0, ".lr", "+231", "Africa/Monrovia", "LRD", "en-LR", 99),
            new("LY", "LBY", 434, "利比亚国", "利比亚", "Libya", "利", "非洲", "Tripoli", 0, ".ly", "+218", "Africa/Tripoli", "LYD", "ar-LY", 100),
            new("LI", "LIE", 438, "列支敦士登公国", "列支敦士登", "Liechtenstein", "列", "欧洲", "Vaduz", 0, ".li", "+423", "Europe/Vaduz", "CHF", "de-LI", 101),
            new("LT", "LTU", 440, "立陶宛共和国", "立陶宛", "Lithuania", "立", "欧洲", "Vilnius", 0, ".lt", "+370", "Europe/Vilnius", "EUR", "lt", 102),
            new("LU", "LUX", 442, "卢森堡大公国", "卢森堡", "Luxembourg", "卢", "欧洲", "Luxembourg", 0, ".lu", "+352", "Europe/Luxembourg", "EUR", "lb", 103),
            new("MG", "MDG", 450, "马达加斯加共和国", "马达加斯加", "Madagascar", "马", "非洲", "Antananarivo", 0, ".mg", "+261", "Indian/Antananarivo", "MGA", "fr-MG", 104),
            new("MW", "MWI", 454, "马拉维共和国", "马拉维", "Malawi", "马", "非洲", "Lilongwe", 0, ".mw", "+265", "Africa/Blantyre", "MWK", "en-MW", 105),
            new("MY", "MYS", 458, "马来西亚", "马来西亚", "Malaysia", "马", "亚洲", "Kuala Lumpur", 0, ".my", "+60", "Asia/Kuala_Lumpur", "MYR", "ms-MY", 106),
            new("MV", "MDV", 462, "马尔代夫共和国", "马尔代夫", "Maldives", "马", "亚洲", "Malé", 0, ".mv", "+960", "Indian/Maldives", "MVR", "dv", 107),
            new("ML", "MLI", 466, "马里共和国", "马里", "Mali", "马", "非洲", "Bamako", 0, ".ml", "+223", "Africa/Bamako", "XOF", "fr-ML", 108),
            new("MT", "MLT", 470, "马耳他共和国", "马耳他", "Malta", "马", "欧洲", "Valletta", 0, ".mt", "+356", "Europe/Malta", "EUR", "mt", 109),
            new("MH", "MHL", 584, "马绍尔群岛共和国", "马绍尔群岛", "Marshall Islands", "马", "大洋洲", "Majuro", 0, ".mh", "+692", "Pacific/Majuro", "USD", "en-MH", 110),
            new("MR", "MRT", 478, "毛里塔尼亚伊斯兰共和国", "毛里塔尼亚", "Mauritania", "毛", "非洲", "Nouakchott", 0, ".mr", "+222", "Africa/Nouakchott", "MRU", "ar-MR", 111),
            new("MU", "MUS", 480, "毛里求斯共和国", "毛里求斯", "Mauritius", "毛", "非洲", "Port Louis", 0, ".mu", "+230", "Indian/Mauritius", "MUR", "en-MU", 112),
            new("MX", "MEX", 484, "墨西哥合众国", "墨西哥", "Mexico", "墨", "北美洲", "Mexico City", 0, ".mx", "+52", "America/Mexico_City", "MXN", "es-MX", 113),
            new("FM", "FSM", 583, "密克罗尼西亚联邦", "密克罗尼西亚", "Micronesia", "密", "大洋洲", "Palikir", 0, ".fm", "+691", "Pacific/Pohnpei", "USD", "en-FM", 114),
            new("MD", "MDA", 498, "摩尔多瓦共和国", "摩尔多瓦", "Moldova", "摩", "欧洲", "Chișinău", 0, ".md", "+373", "Europe/Chisinau", "MDL", "ro", 115),
            new("MC", "MCO", 492, "摩纳哥公国", "摩纳哥", "Monaco", "摩", "欧洲", "Monaco", 0, ".mc", "+377", "Europe/Monaco", "EUR", "fr-MC", 116),
            new("MN", "MNG", 496, "蒙古国", "蒙古", "Mongolia", "蒙", "亚洲", "乌兰巴托", 0, ".mn", "+976", "Asia/Ulaanbaatar", "MNT", "mn", 117),
            new("ME", "MNE", 499, "黑山", "黑山", "Montenegro", "黑", "欧洲", "Podgorica", 0, ".me", "+382", "Europe/Podgorica", "EUR", "sr-ME", 118),
            new("MA", "MAR", 504, "摩洛哥王国", "摩洛哥", "Morocco", "摩", "非洲", "Rabat", 0, ".ma", "+212", "Africa/Casablanca", "MAD", "ar-MA", 119),
            new("MZ", "MOZ", 508, "莫桑比克共和国", "莫桑比克", "Mozambique", "莫", "非洲", "Maputo", 0, ".mz", "+258", "Africa/Maputo", "MZN", "pt-MZ", 120),
            new("MM", "MMR", 104, "缅甸联邦共和国", "缅甸", "Myanmar", "缅", "亚洲", "Naypyidaw", 0, ".mm", "+95", "Asia/Yangon", "MMK", "my", 121),
            new("NA", "NAM", 516, "纳米比亚共和国", "纳米比亚", "Namibia", "纳", "非洲", "Windhoek", 0, ".na", "+264", "Africa/Windhoek", "NAD", "en-NA", 122),
            new("NR", "NRU", 520, "瑙鲁共和国", "瑙鲁", "Nauru", "瑙", "大洋洲", "Yaren", 0, ".nr", "+674", "Pacific/Nauru", "AUD", "en-NR", 123),
            new("NP", "NPL", 524, "尼泊尔联邦民主共和国", "尼泊尔", "Nepal", "尼", "亚洲", "Kathmandu", 0, ".np", "+977", "Asia/Kathmandu", "NPR", "ne", 124),
            new("NL", "NLD", 528, "荷兰王国", "荷兰", "Netherlands", "荷", "欧洲", "Amsterdam", 0, ".nl", "+31", "Europe/Amsterdam", "EUR", "nl-NL", 125),
            new("NZ", "NZL", 554, "新西兰", "新西兰", "New Zealand", "新", "大洋洲", "Wellington", 0, ".nz", "+64", "Pacific/Auckland", "NZD", "en-NZ", 126),
            new("NI", "NIC", 558, "尼加拉瓜共和国", "尼加拉瓜", "Nicaragua", "尼", "北美洲", "Managua", 0, ".ni", "+505", "America/Managua", "NIO", "es-NI", 127),
            new("NE", "NER", 562, "尼日尔共和国", "尼日尔", "Niger", "尼", "非洲", "Niamey", 0, ".ne", "+227", "Africa/Niamey", "XOF", "fr-NE", 128),
            new("NG", "NGA", 566, "尼日利亚联邦共和国", "尼日利亚", "Nigeria", "尼", "非洲", "Abuja", 0, ".ng", "+234", "Africa/Lagos", "NGN", "en-NG", 129),
            new("MK", "MKD", 807, "北马其顿共和国", "北马其顿", "North Macedonia", "北", "欧洲", "Skopje", 0, ".mk", "+389", "Europe/Skopje", "MKD", "mk", 130),
            new("NO", "NOR", 578, "挪威王国", "挪威", "Norway", "挪", "欧洲", "Oslo", 0, ".no", "+47", "Europe/Oslo", "NOK", "nb", 131),
            new("OM", "OMN", 512, "阿曼苏丹国", "阿曼", "Oman", "阿", "亚洲", "Muscat", 0, ".om", "+968", "Asia/Muscat", "OMR", "ar-OM", 132),
            new("PK", "PAK", 586, "巴基斯坦伊斯兰共和国", "巴基斯坦", "Pakistan", "巴", "亚洲", "Islamabad", 0, ".pk", "+92", "Asia/Karachi", "PKR", "ur-PK", 133),
            new("PW", "PLW", 585, "帕劳共和国", "帕劳", "Palau", "帕", "大洋洲", "Ngerulmud", 0, ".pw", "+680", "Pacific/Palau", "USD", "en-PW", 134),
            new("PS", "PSE", 275, "巴勒斯坦国", "巴勒斯坦", "Palestine", "巴", "亚洲", "Ramallah", 0, ".ps", "+970", "Asia/Gaza", "ILS", "ar-PS", 135),
            new("PA", "PAN", 591, "巴拿马共和国", "巴拿马", "Panama", "巴", "北美洲", "Panama City", 0, ".pa", "+507", "America/Panama", "PAB", "es-PA", 136),
            new("PG", "PNG", 598, "巴布亚新几内亚独立国", "巴布亚新几内亚", "Papua New Guinea", "巴", "大洋洲", "Port Moresby", 0, ".pg", "+675", "Pacific/Port_Moresby", "PGK", "en-PG", 137),
            new("PY", "PRY", 600, "巴拉圭共和国", "巴拉圭", "Paraguay", "巴", "南美洲", "Asunción", 0, ".py", "+595", "America/Asuncion", "PYG", "es-PY", 138),
            new("PE", "PER", 604, "秘鲁共和国", "秘鲁", "Peru", "秘", "南美洲", "Lima", 0, ".pe", "+51", "America/Lima", "PEN", "es-PE", 139),
            new("PH", "PHL", 608, "菲律宾共和国", "菲律宾", "Philippines", "菲", "亚洲", "Manila", 0, ".ph", "+63", "Asia/Manila", "PHP", "fil", 140),
            new("PL", "POL", 616, "波兰共和国", "波兰", "Poland", "波", "欧洲", "Warsaw", 0, ".pl", "+48", "Europe/Warsaw", "PLN", "pl", 141),
            new("PT", "PRT", 620, "葡萄牙共和国", "葡萄牙", "Portugal", "葡", "欧洲", "Lisbon", 0, ".pt", "+351", "Europe/Lisbon", "EUR", "pt-PT", 142),
            new("QA", "QAT", 634, "卡塔尔国", "卡塔尔", "Qatar", "卡", "亚洲", "Doha", 0, ".qa", "+974", "Asia/Qatar", "QAR", "ar-QA", 143),
            new("RO", "ROU", 642, "罗马尼亚", "罗马尼亚", "Romania", "罗", "欧洲", "Bucharest", 0, ".ro", "+40", "Europe/Bucharest", "RON", "ro", 144),
            new("RU", "RUS", 643, "俄罗斯联邦", "俄罗斯", "Russia", "俄", "欧洲", "莫斯科", 0, ".ru", "+7", "Europe/Moscow", "RUB", "ru", 145),
            new("RW", "RWA", 646, "卢旺达共和国", "卢旺达", "Rwanda", "卢", "非洲", "Kigali", 0, ".rw", "+250", "Africa/Kigali", "RWF", "rw", 146),
            new("KN", "KNA", 659, "圣基茨和尼维斯联邦", "圣基茨和尼维斯", "Saint Kitts and Nevis", "圣", "北美洲", "Basseterre", 0, ".kn", "+1869", "America/St_Kitts", "XCD", "en-KN", 147),
            new("LC", "LCA", 662, "圣卢西亚", "圣卢西亚", "Saint Lucia", "圣", "北美洲", "Castries", 0, ".lc", "+1758", "America/St_Lucia", "XCD", "en-LC", 148),
            new("VC", "VCT", 670, "圣文森特和格林纳丁斯", "圣文森特和格林纳丁斯", "Saint Vincent and the Grenadines", "圣", "北美洲", "Kingstown", 0, ".vc", "+1784", "America/St_Vincent", "XCD", "en-VC", 149),
            new("WS", "WSM", 882, "萨摩亚独立国", "萨摩亚", "Samoa", "萨", "大洋洲", "Apia", 0, ".ws", "+685", "Pacific/Apia", "WST", "en-WS", 150),
            new("SM", "SMR", 674, "圣马力诺共和国", "圣马力诺", "San Marino", "圣", "欧洲", "San Marino", 0, ".sm", "+378", "Europe/San_Marino", "EUR", "it-SM", 151),
            new("ST", "STP", 678, "圣多美和普林西比民主共和国", "圣多美和普林西比", "Sao Tome and Principe", "圣", "非洲", "São Tomé", 0, ".st", "+239", "Africa/Sao_Tome", "STN", "pt-ST", 152),
            new("SA", "SAU", 682, "沙特阿拉伯王国", "沙特阿拉伯", "Saudi Arabia", "沙", "亚洲", "Riyadh", 0, ".sa", "+966", "Asia/Riyadh", "SAR", "ar-SA", 153),
            new("SN", "SEN", 686, "塞内加尔共和国", "塞内加尔", "Senegal", "塞", "非洲", "Dakar", 0, ".sn", "+221", "Africa/Dakar", "XOF", "fr-SN", 154),
            new("RS", "SRB", 688, "塞尔维亚共和国", "塞尔维亚", "Serbia", "塞", "欧洲", "Belgrade", 0, ".rs", "+381", "Europe/Belgrade", "RSD", "sr", 155),
            new("SC", "SYC", 690, "塞舌尔共和国", "塞舌尔", "Seychelles", "塞", "非洲", "Victoria", 0, ".sc", "+248", "Indian/Mahe", "SCR", "en-SC", 156),
            new("SL", "SLE", 694, "塞拉利昂共和国", "塞拉利昂", "Sierra Leone", "塞", "非洲", "Freetown", 0, ".sl", "+232", "Africa/Freetown", "SLE", "en-SL", 157),
            new("SG", "SGP", 702, "新加坡共和国", "新加坡", "Singapore", "新", "亚洲", "Singapore", 0, ".sg", "+65", "Asia/Singapore", "SGD", "en-SG", 158),
            new("SK", "SVK", 703, "斯洛伐克共和国", "斯洛伐克", "Slovakia", "斯", "欧洲", "Bratislava", 0, ".sk", "+421", "Europe/Bratislava", "EUR", "sk", 159),
            new("SI", "SVN", 705, "斯洛文尼亚共和国", "斯洛文尼亚", "Slovenia", "斯", "欧洲", "Ljubljana", 0, ".si", "+386", "Europe/Ljubljana", "EUR", "sl", 160),
            new("SB", "SLB", 90, "所罗门群岛", "所罗门群岛", "Solomon Islands", "所", "大洋洲", "Honiara", 0, ".sb", "+677", "Pacific/Guadalcanal", "SBD", "en-SB", 161),
            new("SO", "SOM", 706, "索马里联邦共和国", "索马里", "Somalia", "索", "非洲", "Mogadishu", 0, ".so", "+252", "Africa/Mogadishu", "SOS", "so", 162),
            new("ZA", "ZAF", 710, "南非共和国", "南非", "South Africa", "南", "非洲", "Pretoria", 0, ".za", "+27", "Africa/Johannesburg", "ZAR", "en-ZA", 163),
            new("SS", "SSD", 728, "南苏丹共和国", "南苏丹", "South Sudan", "南", "非洲", "Juba", 0, ".ss", "+211", "Africa/Juba", "SSP", "en-SS", 164),
            new("ES", "ESP", 724, "西班牙王国", "西班牙", "Spain", "西", "欧洲", "Madrid", 0, ".es", "+34", "Europe/Madrid", "EUR", "es-ES", 165),
            new("LK", "LKA", 144, "斯里兰卡民主社会主义共和国", "斯里兰卡", "Sri Lanka", "斯", "亚洲", "Colombo", 0, ".lk", "+94", "Asia/Colombo", "LKR", "si", 166),
            new("SD", "SDN", 729, "苏丹共和国", "苏丹", "Sudan", "苏", "非洲", "Khartoum", 0, ".sd", "+249", "Africa/Khartoum", "SDG", "ar-SD", 167),
            new("SR", "SUR", 740, "苏里南共和国", "苏里南", "Suriname", "苏", "南美洲", "Paramaribo", 0, ".sr", "+597", "America/Paramaribo", "SRD", "nl-SR", 168),
            new("SE", "SWE", 752, "瑞典王国", "瑞典", "Sweden", "瑞", "欧洲", "Stockholm", 0, ".se", "+46", "Europe/Stockholm", "SEK", "sv", 169),
            new("CH", "CHE", 756, "瑞士联邦", "瑞士", "Switzerland", "瑞", "欧洲", "Bern", 0, ".ch", "+41", "Europe/Zurich", "CHF", "de-CH", 170),
            new("SY", "SYR", 760, "阿拉伯叙利亚共和国", "叙利亚", "Syria", "叙", "亚洲", "Damascus", 0, ".sy", "+963", "Asia/Damascus", "SYP", "ar-SY", 171),
            new("TZ", "TZA", 834, "坦桑尼亚联合共和国", "坦桑尼亚", "Tanzania", "坦", "非洲", "Dodoma", 0, ".tz", "+255", "Africa/Dar_es_Salaam", "TZS", "sw-TZ", 172),
            new("TH", "THA", 764, "泰王国", "泰国", "Thailand", "泰", "亚洲", "Bangkok", 0, ".th", "+66", "Asia/Bangkok", "THB", "th", 173),
            new("TL", "TLS", 626, "东帝汶民主共和国", "东帝汶", "Timor-Leste", "东", "亚洲", "Dili", 0, ".tl", "+670", "Asia/Dili", "USD", "tet", 174),
            new("TG", "TGO", 768, "多哥共和国", "多哥", "Togo", "多", "非洲", "Lomé", 0, ".tg", "+228", "Africa/Lome", "XOF", "fr-TG", 175),
            new("TO", "TON", 776, "汤加王国", "汤加", "Tonga", "汤", "大洋洲", "Nuku'alofa", 0, ".to", "+676", "Pacific/Tongatapu", "TOP", "to", 176),
            new("TT", "TTO", 780, "特立尼达和多巴哥共和国", "特立尼达和多巴哥", "Trinidad and Tobago", "特", "北美洲", "Port of Spain", 0, ".tt", "+1868", "America/Port_of_Spain", "TTD", "en-TT", 177),
            new("TN", "TUN", 788, "突尼斯共和国", "突尼斯", "Tunisia", "突", "非洲", "Tunis", 0, ".tn", "+216", "Africa/Tunis", "TND", "ar-TN", 178),
            new("TR", "TUR", 792, "土耳其共和国", "土耳其", "Türkiye", "土", "亚洲", "Ankara", 0, ".tr", "+90", "Europe/Istanbul", "TRY", "tr", 179),
            new("TM", "TKM", 795, "土库曼斯坦", "土库曼斯坦", "Turkmenistan", "土", "亚洲", "Ashgabat", 0, ".tm", "+993", "Asia/Ashgabat", "TMT", "tk", 180),
            new("TV", "TUV", 798, "图瓦卢", "图瓦卢", "Tuvalu", "图", "大洋洲", "Funafuti", 0, ".tv", "+688", "Pacific/Funafuti", "AUD", "en-TV", 181),
            new("UG", "UGA", 800, "乌干达共和国", "乌干达", "Uganda", "乌", "非洲", "Kampala", 0, ".ug", "+256", "Africa/Kampala", "UGX", "en-UG", 182),
            new("UA", "UKR", 804, "乌克兰", "乌克兰", "Ukraine", "乌", "欧洲", "Kyiv", 0, ".ua", "+380", "Europe/Kyiv", "UAH", "uk", 183),
            new("AE", "ARE", 784, "阿拉伯联合酋长国", "阿联酋", "United Arab Emirates", "阿", "亚洲", "Abu Dhabi", 0, ".ae", "+971", "Asia/Dubai", "AED", "ar-AE", 184),
            new("GB", "GBR", 826, "大不列颠及北爱尔兰联合王国", "英国", "United Kingdom", "英", "欧洲", "London", 0, ".uk", "+44", "Europe/London", "GBP", "en-GB", 185),
            new("US", "USA", 840, "美利坚合众国", "美国", "United States", "美", "北美洲", "Washington D.C.", 0, ".us", "+1", "America/New_York", "USD", "en-US", 186),
            new("UY", "URY", 858, "乌拉圭东岸共和国", "乌拉圭", "Uruguay", "乌", "南美洲", "Montevideo", 0, ".uy", "+598", "America/Montevideo", "UYU", "es-UY", 187),
            new("UZ", "UZB", 860, "乌兹别克斯坦共和国", "乌兹别克斯坦", "Uzbekistan", "乌", "亚洲", "Tashkent", 0, ".uz", "+998", "Asia/Tashkent", "UZS", "uz", 188),
            new("VU", "VUT", 548, "瓦努阿图共和国", "瓦努阿图", "Vanuatu", "瓦", "大洋洲", "Port Vila", 0, ".vu", "+678", "Pacific/Efate", "VUV", "en-VU", 189),
            new("VE", "VEN", 862, "委内瑞拉玻利瓦尔共和国", "委内瑞拉", "Venezuela", "委", "南美洲", "Caracas", 0, ".ve", "+58", "America/Caracas", "VES", "es-VE", 190),
            new("VN", "VNM", 704, "越南社会主义共和国", "越南", "Viet Nam", "越", "亚洲", "Hanoi", 0, ".vn", "+84", "Asia/Ho_Chi_Minh", "VND", "vi", 191),
            new("YE", "YEM", 887, "也门共和国", "也门", "Yemen", "也", "亚洲", "Sana'a", 0, ".ye", "+967", "Asia/Aden", "YER", "ar-YE", 192),
            new("ZM", "ZMB", 894, "赞比亚共和国", "赞比亚", "Zambia", "赞", "非洲", "Lusaka", 0, ".zm", "+260", "Africa/Lusaka", "ZMW", "en-ZM", 193),
            new("ZW", "ZWE", 716, "津巴布韦共和国", "津巴布韦", "Zimbabwe", "津", "非洲", "Harare", 0, ".zw", "+263", "Africa/Harare", "ZWL", "en-ZW", 194),
            new("XK", "XKX", 383, "科索沃共和国", "科索沃", "Kosovo", "科", "欧洲", "Pristina", 0, null, "+383", "Europe/Belgrade", "EUR", "sq", 195),
        };

    }

    /// <summary>
    /// 国家地区种子项。参数顺序与实体一致：FullName→RegionName→EnglishName→ShortName，Continent→Capital。
    /// FullName 为官方全称（如中华人民共和国、United States of America），必填，不可为 null。
    /// </summary>
    private sealed record CountrySeedItem(
        string RegionCode,
        string Alpha3Code,
        int NumericCode,
        string FullName,
        string RegionName,
        string? EnglishName,
        string? ShortName,
        string? Continent,
        string? Capital,
        int RegionType,
        string? CountryDomain,
        string? AreaCode,
        string? TimeZone,
        string? CurrencyCode,
        string? LanguageCode,
        int OrderNum
    );
}
