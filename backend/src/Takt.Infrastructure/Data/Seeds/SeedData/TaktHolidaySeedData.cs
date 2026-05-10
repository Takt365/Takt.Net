// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Infrastructure.Data.Seeds
// 文件名称：TaktHolidaySeedData.cs
// 功能描述：假日种子数据，2026年法定节假日放假安排
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Domain.Entities.HumanResource.AttendanceLeave;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.SeedData;

/// <summary>
/// 假日种子数据
/// </summary>
public class TaktHolidaySeedData : ITaktSeedData
{
    /// <summary>
    /// 执行顺序
    /// </summary>
    public int Order => 50;

    /// <summary>
    /// 初始化假日种子数据（2026年法定节假日）
    /// </summary>
    public async Task<(int InsertCount, int UpdateCount)> SeedAsync(IServiceProvider serviceProvider, string configId)
    {
        var repo = serviceProvider.GetRequiredService<ITaktRepository<TaktHoliday>>();
        int insertCount = 0;
        int updateCount = 0;
        const int year = 2026;

        // 地区 | 节假日 | 开始月日 | 结束月日 | 问候语（简短）| 引用/诗句（quote）| 主题色 | 调休补班（Remark）| IsWorkingDay
        var holidays = new[]
        {
            // CN：中国红喜庆、清明灰肃穆、中秋黄月、端午绿艾、劳动蓝、妇女/儿童粉、五四红等
            ("CN", "元旦", (1, 1), (1, 3), "元旦快乐", "爆竹声中一岁除，春风送暖入屠苏。", "red", "1月4日", 0),
            ("CN", "春节", (2, 15), (2, 23), "春节快乐", "千门万户曈曈日，总把新桃换旧符。", "red", "2月14日、2月28日", 0),
            ("CN", "元宵节", (3, 3), (3, 3), "元宵快乐", "众里寻他千百度，蓦然回首，那人却在，灯火阑珊处。", "yellow", null, 1),
            ("CN", "妇女节", (3, 8), (3, 8), "妇女节快乐", "休言女子非英物，夜夜龙泉壁上鸣。", "pink", null, 2),
            ("CN", "植树节", (3, 12), (3, 12), "植树节快乐", "东风二月苏堤路，树树桃花间柳花。", "green", null, 1),
            ("CN", "清明节", (4, 4), (4, 6), "清明安康", "借问酒家何处有？牧童遥指杏花村。", "gray", null, 0),
            ("CN", "劳动节", (5, 1), (5, 5), "劳动节快乐", "力尽不知热，但惜夏日长。", "blue", "5月9日", 0),
            ("CN", "青年节", (5, 4), (5, 4), "青年节快乐", "青春须早为，岂能长少年。", "red", null, 2),
            ("CN", "护士节", (5, 12), (5, 12), "护士节快乐", "采得百花成蜜后，为谁辛苦为谁甜？", "pink", null, 1),
            ("CN", "儿童节", (6, 1), (6, 1), "儿童节快乐", "儿童散学归来早，忙趁东风放纸鸢。", "pink", null, 0),
            ("CN", "端午节", (6, 19), (6, 21), "端午安康", "路漫漫其修远兮，吾将上下而求索。", "green", null, 0),
            ("CN", "建军节", (8, 1), (8, 1), "建军节快乐", "黄沙百战穿金甲，不破楼兰终不还。", "red", null, 2),
            ("CN", "教师节", (9, 10), (9, 10), "教师节快乐", "新竹高于旧竹枝，全凭老干为扶持。", "blue", null, 1),
            ("CN", "中秋节", (9, 25), (9, 27), "中秋快乐", "但愿人长久，千里共婵娟。", "yellow", null, 0),
            ("CN", "国庆节", (10, 1), (10, 7), "国庆快乐", "俱往矣，数风流人物，还看今朝。", "red", "9月20日、10月10日", 0),
            ("CN", "重阳节", (10, 18), (10, 18), "重阳安康", "遥知兄弟登高处，遍插茱萸少一人。", "orange", null, 1),
            // JP：问候语简短，引用为和歌/俳句或当地名句，绝不与问候语相同
            ("JP", "元旦", (1, 1), (1, 1), "あけましておめでとう。", "初春の令月にして、気淑く風和らぐ。（万葉集・元日）", "red", null, 0),
            ("JP", "成人日", (1, 12), (1, 12), "成人の日、おめでとう。", "二十歳の門出に、社会の一員としての自覚を新たに。", "purple", null, 0),
            ("JP", "建国纪念日", (2, 11), (2, 11), "建国記念の日。", "肇国は、悠久な歴史と伝統に根ざす日本の礎。", "red", null, 0),
            ("JP", "天皇诞生日", (2, 23), (2, 23), "天皇誕生日、おめでとう。", "象徴として国民の統合に寄与される日を祝う。", "red", null, 0),
            ("JP", "春分日", (3, 20), (3, 20), "春分の日。", "暑さ寒さも彼岸まで。自然の恵みに感謝する日。", "green", null, 0),
            ("JP", "昭和之日", (4, 29), (4, 29), "昭和の日。", "激動の日々を経て、復興を遂げた昭和の精神を偲ぶ。", "indigo", null, 0),
            ("JP", "宪法纪念日", (5, 3), (5, 3), "憲法記念日。", "国民主権・基本的人権・平和主義を謳う日本国憲法の精神。", "blue", null, 0),
            ("JP", "绿之日", (5, 4), (5, 4), "みどりの日。", "自然に親しみ、その恩恵に感謝し、豊かな心を育む。", "green", null, 0),
            ("JP", "儿童节", (5, 5), (5, 5), "こどもの日、おめでとう。", "鯉のぼり、空を泳ぐ。子供の健やかな成長を願う端午の節句。", "blue", null, 0),
            ("JP", "海之日", (7, 20), (7, 20), "海の日。", "海の恩恵に感謝するとともに、海洋国家日本の繁栄を願う。", "cyan", null, 0),
            ("JP", "山之日", (8, 11), (8, 11), "山の日。", "山に親しむ機会を得て、山の恩恵に感謝する。（富士は日本一の山）", "green", null, 0),
            ("JP", "敬老日", (9, 21), (9, 21), "敬老の日。", "多年にわたり社会につくしてきた老人を敬い、長寿を祝う。", "orange", null, 0),
            ("JP", "秋分日", (9, 23), (9, 23), "秋分の日。", "祖先をうやまい、なくなった人々をしのぶ。秋の彼岸。", "orange", null, 0),
            ("JP", "体育之日", (10, 12), (10, 12), "スポーツの日。", "スポーツに親しみ、健康な心身をつちかう。", "blue", null, 0),
            ("JP", "文化日", (11, 3), (11, 3), "文化の日。", "自由と平和を愛し、文化をすすめる。文化勲章授与の日。", "purple", null, 0),
            ("JP", "勤劳感谢日", (11, 23), (11, 23), "勤労感謝の日。", "勤労をたっとび、生産を祝い、国民たがいに感謝しあう。", "yellow", null, 0),
            // US：问候语简短，引用为美国文学/名句或纪念寓意，绝不与问候语相同
            ("US", "元旦", (1, 1), (1, 1), "Happy New Year!", "Ring out the old, ring in the new. — Tennyson", "red", null, 0),
            ("US", "马丁·路德·金纪念日", (1, 19), (1, 19), "Honoring Dr. King.", "I have a dream that one day this nation will rise up. — Martin Luther King Jr.", "indigo", null, 0),
            ("US", "总统日", (2, 16), (2, 16), "Presidents' Day.", "Government of the people, by the people, for the people. — Lincoln", "blue", null, 0),
            ("US", "阵亡将士纪念日", (5, 25), (5, 25), "Remember the fallen.", "In Flanders fields the poppies blow / Between the crosses, row on row. — John McCrae", "gray", null, 0),
            ("US", "六月节", (6, 19), (6, 19), "Happy Juneteenth!", "Until justice is blind to color, until education is unaware of race. — Maya Angelou", "red", null, 0),
            ("US", "独立日", (7, 4), (7, 4), "Happy Fourth of July!", "We hold these truths to be self-evident, that all men are created equal. — Declaration of Independence", "red", null, 0),
            ("US", "劳动节", (9, 7), (9, 7), "Happy Labor Day!", "Labor is the great producer of wealth; it moves all other causes. — Daniel Webster", "blue", null, 0),
            ("US", "哥伦布日", (10, 12), (10, 12), "Columbus Day.", "Following the light of the sun, we left the Old World. — Columbus", "blue", null, 0),
            ("US", "退伍军人节", (11, 11), (11, 11), "Thank you, veterans.", "The brave men, living and dead, who struggled here, have consecrated it. — Lincoln", "red", null, 0),
            ("US", "感恩节", (11, 26), (11, 26), "Happy Thanksgiving!", "Give thanks for unknown blessings already on their way. — Native American saying", "orange", null, 0),
            ("US", "圣诞节", (12, 25), (12, 25), "Merry Christmas!", "Peace on earth, goodwill to men. — Longfellow", "red", null, 0),
            // KR：问候语简短，引用为韩国诗词/名句或纪念寓意，绝不与问候语相同
            ("KR", "元旦", (1, 1), (1, 1), "새해 복 많이 받으세요.", "새 봄이 오면 만물이 소생하나니，새해에도 희망이 피어나리라.", "red", null, 0),
            ("KR", "春节", (2, 16), (2, 18), "설날 복 많이 받으세요.", "설날에는 차례를 모시고，세배로 어른께 인사드리며 한 해를 시작한다.", "red", null, 0),
            ("KR", "三一节", (3, 1), (3, 1), "삼일절.", "오천년 역사의 빛，이 조국에 길이 빛내세. — 애국가", "red", null, 0),
            ("KR", "儿童节", (5, 5), (5, 5), "어린이날 축하해요.", "어린이는 나라의 미래요，꿈과 희망의 씨앗이자리.", "pink", null, 0),
            ("KR", "佛诞日", (5, 24), (5, 24), "부처님 오신 날.", "등 밝히는 날，마음의 등불을 밝혀 중생을 이끄시니.", "yellow", null, 0),
            ("KR", "显忠日", (6, 6), (6, 6), "호국영령에 감사합니다.", "나라를 지키다 돌아가신 영령들，그 희생을 기억하며 감사한다.", "gray", null, 0),
            ("KR", "光复节", (8, 15), (8, 15), "광복절 축하합니다.", "광복의 기쁨，자유와 독립의 소중함을 되새기는 날.", "red", null, 0),
            ("KR", "中秋节", (9, 24), (9, 26), "추석 잘 보내세요.", "달 밝은 추석，하늘의 보름달처럼 가족이 둥글게 모여 함께한다.", "yellow", null, 0),
            ("KR", "开天节", (10, 3), (10, 3), "개천절.", "단군이 나라를 열어，오랜 역사와 문화의 뿌리를 세우신 날.", "red", null, 0),
            ("KR", "韩文日", (10, 9), (10, 9), "한글날.", "나라의 말과 글을 세종대왕이 창제하시어，오늘 우리말의 빛이 되셨다.", "blue", null, 0),
            ("KR", "圣诞节", (12, 25), (12, 25), "메리 크리스마스!", "평화의 밤，사랑과 나눔이 가득한 성탄을 축하합니다.", "red", null, 0),
            // HK：问候语简短，引用为粤/华诗词或纪念寓意，绝不与问候语相同
            ("HK", "元旦", (1, 1), (1, 1), "新年快樂", "爆竹一聲除舊，桃符萬戶更新。", "red", null, 0),
            ("HK", "农历新年", (2, 17), (2, 19), "恭喜發財", "千門萬戶曈曈日，總把新桃換舊符。", "red", null, 0),
            ("HK", "耶稣受难节", (4, 3), (4, 4), "受難節平安。", "十架大愛，捨身贖罪；信者得生，永誌不忘。", "gray", null, 0),
            ("HK", "清明节翌日", (4, 6), (4, 6), "清明安康", "清明時節雨紛紛，路上行人欲斷魂。借問酒家何處有？牧童遙指杏花村。", "gray", null, 0),
            ("HK", "复活节翌日", (4, 7), (4, 7), "復活節快樂", "死裡復活，戰勝死亡；盼望與新生，與信者同在。", "yellow", null, 0),
            ("HK", "劳动节", (5, 1), (5, 1), "勞動光榮", "鋤禾日當午，汗滴禾下土。誰知盤中飧，粒粒皆辛苦。", "blue", null, 0),
            ("HK", "佛诞翌日", (5, 25), (5, 25), "佛誕吉祥", "佛誕浴佛，淨心祈福；慈悲智慧，普度眾生。", "yellow", null, 0),
            ("HK", "端午节", (6, 19), (6, 19), "端午安康", "路漫漫其修遠兮，吾將上下而求索。", "green", null, 0),
            ("HK", "香港特别行政区成立纪念日", (7, 1), (7, 1), "回歸紀念日", "一國兩制，港人治港；東方之珠，繁榮安定。", "red", null, 0),
            ("HK", "中秋节翌日", (9, 26), (9, 26), "中秋快樂", "但願人長久，千里共嬋娟。", "yellow", null, 0),
            ("HK", "国庆日", (10, 1), (10, 1), "國慶快樂", "祝祖國繁榮昌盛，國泰民安。", "red", null, 0),
            ("HK", "重阳节翌日", (10, 19), (10, 19), "重陽安康", "遙知兄弟登高處，遍插茱萸少一人。", "orange", null, 0),
            ("HK", "圣诞节", (12, 25), (12, 26), "聖誕快樂", "平安夜，聖善夜；萬暗中，光華射。", "red", null, 0),
            // TW：2026 年台湾公众假期（按用户提供清单）
            ("TW", "元旦", (1, 1), (1, 1), "元旦快樂", "新年第一天，萬象更新。", "red", null, 0),
            ("TW", "农历春节", (2, 14), (2, 22), "新春快樂", "含小年夜、除夕、初一至初五及补假。", "red", null, 0),
            ("TW", "和平纪念日(228)", (2, 27), (3, 1), "和平纪念日", "2/28 逢周六，2/27（五）补假。", "gray", null, 0),
            ("TW", "儿童节/清明节", (4, 3), (4, 6), "儿童节与清明节安康", "4/4（六）儿童节、4/5（日）清明，4/6（一）补假。", "gray", null, 0),
            ("TW", "劳动节", (5, 1), (5, 3), "劳动节快樂", "仅适用劳基法适用人员（劳工）。", "blue", null, 0),
            ("TW", "端午节", (6, 19), (6, 21), "端午安康", "农历五月初五。", "green", null, 0),
            ("TW", "中秋节/教师节", (9, 25), (9, 28), "中秋与教师节快樂", "9/27 中秋、9/28 教师节（周一补假）。", "yellow", null, 0),
            ("TW", "国庆日(双十节)", (10, 9), (10, 11), "国庆快樂", "10/10（六）逢周末，10/9（五）补假。", "red", null, 0),
            ("TW", "台湾光复节", (10, 24), (10, 26), "台湾光复节", "10/25（日）逢周末，10/26（一）补假。", "orange", null, 0),
            ("TW", "行宪纪念日", (12, 25), (12, 27), "行宪纪念日", "12/25 放假，与周末连休。", "indigo", null, 0),
            // RU：问候语简短，引用为俄文学/名句或纪念寓意，绝不与问候语相同
            ("RU", "元旦", (1, 1), (1, 1), "С Новым годом!", "Мороз и солнце; день чудесный! — Пушкин", "red", null, 0),
            ("RU", "东正教圣诞节", (1, 7), (1, 7), "С Рождеством!", "В ночь под Рождество небо полно звёзд и тишины.", "yellow", null, 0),
            ("RU", "祖国保卫者日", (2, 23), (2, 23), "С Днём защитника Отечества!", "Кто с мечом к нам придёт, от меча и погибнет. — Александр Невский", "red", null, 0),
            ("RU", "国际妇女节", (3, 8), (3, 8), "С 8 Марта!", "Женщина — весна земли, красота и вдохновение. — Максим Горький", "pink", null, 0),
            ("RU", "春天与劳动节", (5, 1), (5, 1), "С Праздником труда!", "Труд облагораживает человека. — В. Г. Белинский", "blue", null, 0),
            ("RU", "胜利日", (5, 9), (5, 9), "С Днём Победы!", "Вечная память павшим. Никто не забыт, ничто не забыто. — Ольга Берггольц", "gray", null, 0),
            ("RU", "俄罗斯日", (6, 12), (6, 12), "С Днём России!", "Россия — священная наша держава. — гимн", "red", null, 0),
            ("RU", "人民团结日", (11, 4), (11, 4), "День единства.", "В единстве — сила. Народное ополчение 1612 года.", "red", null, 0),
            // FR：问候语简短，引用为法国文学/名句或纪念寓意，绝不与问候语相同
            ("FR", "元旦", (1, 1), (1, 1), "Bonne année !", "Chaque instant de l'année a son charme. — Lamartine", "red", null, 0),
            ("FR", "复活节星期一", (4, 6), (4, 6), "Joyeuses Pâques !", "Le printemps revient, la nature renaît; Pâques est la fête de la résurrection.", "yellow", null, 0),
            ("FR", "劳动节", (5, 1), (5, 1), "Bonne fête du Travail !", "Le travail éloigne de nous trois grands maux：l'ennui，le vice et le besoin. — Voltaire", "blue", null, 0),
            ("FR", "二战胜利纪念日", (5, 8), (5, 8), "8 mai 1945.", "Souvenons-nous. La liberté reconquise，la paix retrouvée.", "gray", null, 0),
            ("FR", "耶稣升天节", (5, 14), (5, 14), "Ascension.", "Élevé dans la gloire, le Christ envoie ses disciples porter la bonne nouvelle.", "yellow", null, 0),
            ("FR", "圣灵降临节", (5, 25), (5, 25), "Pentecôte.", "L'Esprit descend comme le vent; les langues de feu, les cœurs enflammés.", "yellow", null, 0),
            ("FR", "国庆日", (7, 14), (7, 14), "14 Juillet !", "Liberté, égalité, fraternité. La prise de la Bastille, 1789.", "red", null, 0),
            ("FR", "圣母升天节", (8, 15), (8, 15), "Assomption.", "Marie, mère de l'Église, élevée au ciel en corps et en âme.", "purple", null, 0),
            ("FR", "万圣节", (11, 1), (11, 1), "Toussaint.", "Tous les saints, connus et inconnus; prions pour ceux qui nous ont précédés.", "gray", null, 0),
            ("FR", "一战停战日", (11, 11), (11, 11), "11 Novembre.", "Hommage aux soldats. Plus jamais ça. Le coquelicot et le bleuet.", "gray", null, 0),
            ("FR", "圣诞节", (12, 25), (12, 25), "Joyeux Noël !", "Minuit，chrétiens，c'est l'heure solennelle. — Cantique de Noël", "red", null, 0),
            // ES：问候语简短，引用为西语文学/名句或纪念寓意，绝不与问候语相同
            ("ES", "元旦", (1, 1), (1, 1), "¡Feliz Año Nuevo!", "Año nuevo，vida nueva. Que cada día sea una página en blanco. — Refrán", "red", null, 0),
            ("ES", "主显节", (1, 6), (1, 6), "¡Feliz Día de Reyes!", "Los Reyes Magos de Oriente siguen la estrella；los niños esperan sus regalos.", "yellow", null, 0),
            ("ES", "耶稣受难日", (4, 3), (4, 3), "Viernes Santo.", "Via Crucis. El sacrificio y la redención；el silencio del Viernes，la espera del Domingo.", "gray", null, 0),
            ("ES", "劳动节", (5, 1), (5, 1), "¡Feliz Día del Trabajo!", "El trabajo dignifica al hombre. La lucha por los derechos laborales.", "blue", null, 0),
            ("ES", "圣母升天节", (8, 15), (8, 15), "Asunción.", "María，llena de gracia，asunta al cielo en cuerpo y alma.", "purple", null, 0),
            ("ES", "国庆日", (10, 12), (10, 12), "¡Fiesta Nacional!", "España，una grande y libre. Descubrimiento de América，encuentro de dos mundos.", "red", null, 0),
            ("ES", "万圣节", (11, 1), (11, 1), "Todos los Santos.", "Recordamos a los que partieron；la muerte no es el final. — Unamuno", "orange", null, 0),
            ("ES", "宪法日", (12, 6), (12, 6), "Día de la Constitución.", "La Constitución de 1978：democracia，libertades y Estado de derecho.", "blue", null, 0),
            ("ES", "圣母无染原罪节", (12, 8), (12, 8), "Inmaculada.", "Sin mancha concebida，patrona de España.", "purple", null, 0),
            ("ES", "圣诞节", (12, 25), (12, 25), "¡Feliz Navidad!", "Noche de paz，noche de amor. Todo duerme en derredor. — Villancico", "red", null, 0),
            // SA：问候语简短，引用为古兰经/阿拉伯名句或纪念寓意，绝不与问候语相同
            ("SA", "元旦", (1, 1), (1, 1), "سنة جديدة سعيدة!", "إِنَّ مَعَ الْعُسْرِ يُسْرًا. — القرآن الكريم", "green", null, 0),
            ("SA", "斋月开始", (2, 18), (2, 18), "رمضان كريم.", "شَهْرُ رَمَضَانَ الَّذِي أُنزِلَ فِيهِ الْقُرْآنُ. — القرآن", "indigo", null, 0),
            ("SA", "开斋节", (3, 20), (3, 24), "عيد مبارك!", "فَصُمْتُمْ فَأَفْطِرُوا وَاشْكُرُوا. عيد الفطر، فرحة الصائمين.", "green", null, 0),
            ("SA", "劳动节", (5, 1), (5, 1), "عيد العمال.", "وَقُلِ اعْمَلُوا فَسَيَرَى اللَّهُ عَمَلَكُمْ. — القرآن", "green", null, 0),
            ("SA", "阿拉法特日", (5, 26), (5, 26), "يوم عرفة.", "خير الدعاء دعاء يوم عرفة. الوقوف بعرفة، ركن الحج الأعظم.", "gray", null, 0),
            ("SA", "宰牲节", (5, 27), (5, 31), "عيد الأضحى مبارك!", "وَاذْكُرُوا اللَّهَ فِي أَيَّامٍ مَعْدُودَاتٍ. إبراهيم والذبيحة.", "green", null, 0),
            ("SA", "伊斯兰新年", (6, 16), (6, 16), "رأس السنة الهجرية.", "الهجرة النبوية：من مكة إلى المدينة، فجر الدولة الإسلامية.", "green", null, 0),
            ("SA", "阿舒拉节", (6, 26), (6, 26), "يوم عاشوراء.", "يوم فلكي مبارك؛ يُصام تقرباً إلى الله.", "gray", null, 0),
            ("SA", "先知诞辰", (8, 25), (8, 25), "المولد النبوي.", "وَإِنَّكَ لَعَلَى خُلُقٍ عَظِيمٍ. — القرآن، سورة القلم", "green", null, 0),
            ("SA", "国庆日", (9, 23), (9, 23), "اليوم الوطني.", "توحيد المملكة على يدي الملك عبد العزيز؛ وطن واحد، شعب واحد.", "green", null, 0)
        };

        foreach (var (country, name, startMd, endMd, greeting, quote, theme, remark, isWorkingDay) in holidays)
        {
            var start = new DateTime(year, startMd.Item1, startMd.Item2);
            var end = new DateTime(year, endMd.Item1, endMd.Item2);
            var remarkValue = string.IsNullOrEmpty(remark) ? null : $"调休补班：{remark}";

            var existing = await repo.GetAsync(x => x.StartDate == start && x.HolidayName == name && x.Region == country);
            if (existing == null)
            {
                await repo.CreateAsync(new TaktHoliday
                {
                    Region = country,
                    HolidayName = name,
                    HolidayType = 0,
                    StartDate = start,
                    EndDate = end,
                    IsWorkingDay = isWorkingDay,
                    HolidayGreeting = greeting,
                    HolidayQuote = quote,
                    HolidayTheme = theme,
                    Remark = remarkValue,
                    ConfigId = configId
                });
                insertCount++;
            }
            else
            {
                existing.EndDate = end;
                existing.IsWorkingDay = isWorkingDay;
                existing.HolidayGreeting = greeting;
                existing.HolidayQuote = quote;
                existing.HolidayTheme = theme;
                existing.Remark = remarkValue;
                existing.HolidayType = 0;
                await repo.UpdateAsync(existing);
                updateCount++;
            }
        }

        return (insertCount, updateCount);
    }
}