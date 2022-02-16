using EnsoulSharp;
using EnsoulSharp.SDK;
using EnsoulSharp.SDK.MenuUI;
using JarvisAIO.VLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisAIO.Champions.Ezreal
{
    class Ezreal : Base
    {
        public Ezreal()
        {
            Q = new Spell(SpellSlot.Q, 1150);
            W = new Spell(SpellSlot.W, 1150);
            E = new Spell(SpellSlot.E, 475);
            R = new Spell(SpellSlot.R, 3000f);

            Q.SetSkillshot(0.25f, 60f, 2000f, true, SpellType.Line);
            W.SetSkillshot(0.25f, 60f, 1700f, false, SpellType.Line);
            R.SetSkillshot(1.1f, 160f, 2000f, false, SpellType.Line);

            Local.Add(new Menu("draw", "사거리 표시")
            {
                Draw.qRange,
                Draw.wRange,
                Draw.eRange,
            });

            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            Draw.On();
        }

        private void Game_OnUpdate(EventArgs args)
        {
            if(Program.Combo)
            {
                Modes.Combo.CastW();
                Modes.Combo.CastQ();
            }

            if(Program.Harass)
            {
                Modes.Harass.CastW();
                Modes.Harass.CastQ();
            }
        }
    }
}
