﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisAIO.VLib
{
    class Translator
    {
        public static string Name(string name)
        {
            switch (name)
            {
                case "Aatrox": return "아트록스";
                case "Ahri": return "아리";
                case "Akali": return "아칼리";
                case "Akshan": return "아크샨";
                case "Alistar": return "알리스타";
                case "Amumu": return "아무무";
                case "Anivia": return "애니비아";
                case "Annie": return "애니";
                case "Aphelios": return "아펠";
                case "Ashe": return "애쉬";
                case "AurelionSol": return "아우솔";
                case "Azir": return "아지르";
                case "Bard": return "바드";
                case "Blitzcrank": return "블츠";
                case "Brand": return "브랜드";
                case "Braum": return "브라움";
                case "Caitlyn": return "케틀";
                case "Camille": return "카밀";
                case "Cassiopeia": return "카시오페아";
                case "Chogath": return "초가스";
                case "Corki": return "코르키";
                case "Darius": return "다리우스";
                case "Diana": return "다이애나";
                case "DrMundo": return "문도";
                case "Draven": return "드레이븐";
                case "Ekko": return "에코";
                case "Elise": return "엘리스";
                case "Evelynn": return "이블린";
                case "Ezreal": return "이즈리얼";
                case "Fiddlesticks": return "피들스틱";
                case "Fiora": return "피오라";
                case "Fizz": return "피즈";
                case "Galio": return "갈리오";
                case "Gangplank": return "갱플랭크";
                case "Garen": return "가렌";
                case "Gnar": return "나르";
                case "Gragas": return "그라가스";
                case "Graves": return "그브";
                case "Gwen": return "그웬";
                case "Hecarim": return "헤카림";
                case "Heimerdinger": return "딩거";
                case "Illaoi": return "일라오이";
                case "Irelia": return "이렐리아";
                case "Ivern": return "아이번";
                case "Janna": return "잔나";
                case "JarvanIV": return "자르반";
                case "Jax": return "잭스";
                case "Jayce": return "제이스";
                case "Jhin": return "진";
                case "Jinx": return "징크스";
                case "Kaisa": return "카이사";
                case "Kalista": return "칼리스타";
                case "Karma": return "카르마";
                case "Karthus": return "카서스";
                case "Kassadin": return "카사딘";
                case "Katarina": return "카타리나";
                case "Kayle": return "케일";
                case "Kayn": return "케인";
                case "Kennen": return "케넨";
                case "Khazix": return "카직스";
                case "Kindred": return "킨드레드";
                case "Kled": return "클레드";
                case "Kogmaw": return "코그모";
                case "LeBlanc": return "르블랑";
                case "Leesin": return "리신";
                case "Leona": return "레오나";
                case "Lillia": return "릴리아";
                case "Lissandra": return "리산드라";
                case "Lucian": return "루시안";
                case "Lulu": return "룰루";
                case "Lux": return "럭스";
                case "Malphite": return "말파이트";
                case "Malzahar": return "말자하";
                case "Maokai": return "마오카이";
                case "MasterYi": return "마이";
                case "MissFortune": return "미포";
                case "Mordekaiser": return "모데";
                case "Morgana": return "모르가나";
                case "Nami": return "나미";
                case "Nasus": return "나서스";
                case "Nautilus": return "노틸";
                case "Neeko": return "니코";
                case "Nidalee": return "니달리";
                case "Nocturne": return "녹턴";
                case "Nunu": return "누누";
                case "Olaf": return "올라프";
                case "Orianna": return "오리아나";
                case "Ornn": return "오른";
                case "Pantheon": return "판테온";
                case "Poppy": return "뽀삐";
                case "Pyke": return "파이크";
                case "Qiyana": return "키아나";
                case "Quinn": return "퀸";
                case "Rakan": return "라칸";
                case "Rammus": return "람머스";
                case "Reksai": return "렉사이";
                case "Rell": return "렐";
                case "Renekton": return "레넥톤";
                case "Rengar": return "렝가";
                case "Riven": return "리븐";
                case "Rumble": return "럼블";
                case "Ryze": return "라이즈";
                case "Samira": return "사미라";
                case "Sejuani": return "세주아니";
                case "Senna": return "세나";
                case "Seraphine": return "세라핀";
                case "Sett": return "세트";
                case "Shaco": return "샤코";
                case "Shen": return "쉔";
                case "Shyvana": return "쉬바나";
                case "Singed": return "신지드";
                case "Sion": return "사이온";
                case "Sivir": return "시비르";
                case "Skarner": return "스카너";
                case "Sona": return "소나";
                case "Soraka": return "소라카";
                case "Swain": return "스웨인";
                case "Sylas": return "사일러스";
                case "Syndra": return "신드라";
                case "TahmKench": return "탐켄치";
                case "Taliyah": return "탈리야";
                case "Talon": return "탈론";
                case "Taric": return "타릭";
                case "Teemo": return "티모";
                case "Thresh": return "쓰레쉬";
                case "Tristana": return "트타";
                case "Trundle": return "트런들";
                case "Tryndamere": return "트린";
                case "TwistedFate": return "트페";
                case "Twitch": return "트위치";
                case "Udyr": return "우디르";
                case "Urgot": return "우르곳";
                case "Varus": return "바루스";
                case "Vayne": return "베인";
                case "Veigar": return "베이가";
                case "Velkoz": return "벨코즈";
                case "Vex": return "벡스";
                case "Vi": return "바이";
                case "Viego": return "비에고";
                case "Viktor": return "빅토르";
                case "Vladimir": return "블라디";
                case "Volibear": return "볼베";
                case "Warwick": return "워윅";
                case "Wukong": return "오공";
                case "Xayah": return "자야";
                case "Xerath": return "제라스";
                case "Xinzhao": return "신짜오";
                case "Yasuo": return "야스오";
                case "Yone": return "요네";
                case "Yorick": return "요릭";
                case "Yuumi": return "유미";
                case "Zac": return "자크";
                case "Zed": return "제드";
                case "Zeri": return "제리";
                case "Ziggs": return "직스";
                case "Zilean": return "질리언";
                case "Zoe": return "조이";
                case "Zyra": return "자이라";
                default: return name;
            }
        }
    }
}
