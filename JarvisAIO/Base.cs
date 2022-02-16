using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisAIO
{
    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.MenuUI;
    using JarvisAIO.VLib;

    class Base : Program
    {
        public static Menu Local, ComboMenu, HarassMenu, LaneClearMenu;

        public static List<MenuBool> HarassList = new List<MenuBool>();

        //public static MenuBool manaDisable = new MenuBool("manaDisable", "Disable mana manager in combo", false);

        public static MenuSlider LCmana = new MenuSlider("LCmana", "라인클리어 마나 관리", 50, 0, 100);

        public static float QMANA = 0, WMANA = 0, EMANA = 0, RMANA = 0;

        public static bool FarmSpells
        {
            get
            {
                return Orbwalker.ActiveMode == OrbwalkerMode.LaneClear
                    && Player.ManaPercent > LCmana.Value;
            }
        }

        static Base()
        {
            Local = new Menu(Player.CharacterName, Translator.Name(Player.CharacterName));

            HarassMenu = new Menu("harass", "견제");
            foreach (var enemy in GameObjects.EnemyHeroes)
            {
                var harass = new MenuBool("harass" + enemy.CharacterName, Translator.Name(enemy.CharacterName));
                HarassList.Add(harass);
                HarassMenu.Add(harass);
            }

            LaneClearMenu = new Menu("laneclear", "라인클리어");
            LaneClearMenu.Add(LCmana);

            Local.Add(HarassMenu);
            Local.Add(LaneClearMenu);

            //Config.Add(new Menu("extra", "Extra settings V")
            //{
            //    manaDisable
            //});
            Config.Add(Local);

            //spellFarm.Permashow(); //상단에 표시되는 메뉴
        }

        public static bool InHarassList(AIHeroClient t)
        {
            return HarassList.Any(e => e.Enabled && e.Name == "harass" + t.CharacterName);
        }
    }
}
