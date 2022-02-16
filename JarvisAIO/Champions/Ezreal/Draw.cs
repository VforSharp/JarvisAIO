namespace JarvisAIO.Champions.Ezreal
{
    using EnsoulSharp;
    using EnsoulSharp.SDK;
    using EnsoulSharp.SDK.MenuUI;

    class Draw : Base
    {
        public static readonly MenuBool noti = new MenuBool("noti", "Show notification", false);
        public static readonly MenuBool qRange = new MenuBool("qRange", "Q 사거리 표시", false);
        public static readonly MenuBool wRange = new MenuBool("wRange", "W 사거리 표시", false);
        public static readonly MenuBool eRange = new MenuBool("eRange", "E 사거리 표시", false);

        public static void On()
        {
            if (qRange.Enabled)
            {
                if (Q.IsReady())
                    Render.Circle.DrawCircle(Player.Position, Q.Range, System.Drawing.Color.Cyan, 1);

            }
            if (wRange.Enabled)
            {
                if (W.IsReady())
                    Render.Circle.DrawCircle(Player.Position, W.Range, System.Drawing.Color.Orange, 1);
            }
            if (eRange.Enabled)
            {
                if (E.IsReady())
                    Render.Circle.DrawCircle(Player.Position, E.Range, System.Drawing.Color.Yellow, 1);
            }


            if (noti.Enabled)
            {

                var target = TargetSelector.GetTarget(1500, DamageType.Physical);
                if (target.IsValidTarget())
                {

                    var poutput = Q.GetPrediction(target);
                    if ((int)poutput.Hitchance == 5)
                        Render.Circle.DrawCircle(poutput.CastPosition, 50, System.Drawing.Color.YellowGreen);
                    if (Q.GetDamage(target) > target.Health)
                    {
                        Render.Circle.DrawCircle(target.Position, 200, System.Drawing.Color.Red);
                        Drawing.DrawText(Drawing.Width * 0.1f, Drawing.Height * 0.4f, System.Drawing.Color.Red, "Q kill: " + target.CharacterName + " have: " + target.Health + "hp");
                    }
                    else if (Q.GetDamage(target) + W.GetDamage(target) > target.Health)
                    {
                        Render.Circle.DrawCircle(target.Position, 200, System.Drawing.Color.Red);
                        Drawing.DrawText(Drawing.Width * 0.1f, Drawing.Height * 0.4f, System.Drawing.Color.Red, "Q + W kill: " + target.CharacterName + " have: " + target.Health + "hp");
                    }
                    else if (Q.GetDamage(target) + W.GetDamage(target) + E.GetDamage(target) > target.Health)
                    {
                        Render.Circle.DrawCircle(target.Position, 200, System.Drawing.Color.Red);
                        Drawing.DrawText(Drawing.Width * 0.1f, Drawing.Height * 0.4f, System.Drawing.Color.Red, "Q + W + E kill: " + target.CharacterName + " have: " + target.Health + "hp");
                    }
                }
            }
        }


    }
}
