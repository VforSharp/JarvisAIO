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

    public class Program
    {
        public static Menu Config;

        public static AIHeroClient Player { get { return ObjectManager.Player; } }
        public static Spell Q, W, E, R, Q1, W1, E1, R1;
        public static bool Combo = false, Harass = false, LaneClear = false, None = false;
        public static int tickIndex = 0;

        static void Main(string[] args)
        {
            GameEvent.OnGameLoad += GameEvent_OnGameLoad;
        }

        private static void GameEvent_OnGameLoad()
        {
            Config = new Menu("Jarvis_AIO" + Player.CharacterName, "Jarvis AIO", true);

            switch (Player.CharacterName)
            {
                case "Ezreal":
                    new Champions.Ezreal.Ezreal();
                    break;
            }

            Game.OnUpdate += Game_OnUpdate;
            Config.Attach();
            //Game.Print("<font size='30'>Jarvis</font> <font color='#b756c5'>by V</font>");
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            Combo = Orbwalker.ActiveMode == OrbwalkerMode.Combo;
            Harass = Orbwalker.ActiveMode == OrbwalkerMode.Harass;
            LaneClear = Orbwalker.ActiveMode == OrbwalkerMode.LaneClear;
            None = Orbwalker.ActiveMode == OrbwalkerMode.None;

            tickIndex++;

            if (tickIndex > 4) tickIndex = 0;
        }

        public static void CastSpell(Spell qwer, AIBaseClient target, HitChance hitChance = HitChance.VeryHigh)
        {
            qwer.CastIfHitchanceMinimum(target, hitChance);
        }

        public static bool LagFree(int index)
        {
            return tickIndex == index;
        }
    }
}
